const { UserRecode, User } = require("../Types/Type");
const { DataVO } = require("../VO/DataVO.js");
const { RecordVO } = require("../VO/RecordVO.js");
const mysql = require('mysql2');
const secret = require('../secret')

const pool = mysql.createPool(secret);
const promisePool = pool.promise();


class DBUtil {
    /**
     * 회원가입 요청
     * @param {string} id
     * @param {string} password
     * @returns {boolean} 성공 여부
     */
    async Register(id, password, socket) {
        console.log(id, password);
        let sql = `SELECT id FROM Test WHERE id = ?`;
        let [user] = await promisePool.query(sql, [id]);
        console.log(user);
        if (user.length > 0) {
            socket.send(JSON.stringify(new DataVO("errmsg", "중복된 아이디 입니다.")));
            console.log("에러로 인한 회원가입 거절!");
            return false;
        }
        promisePool.query(`INSERT INTO Test (id, password) VALUES (?, password(?))`, [id, password]);
        console.log("회원가입 성공함");
        return true;
    }

    /**
     * 로그인 요청
     * @param {string} id
     * @param {string} password
     * @param {WebSocket} socket
     * @returns {boolean} 성공 여부
     */
    async Login(id, password, socket) {
        console.log(id, password);
        let sql = `SELECT code, isUsing FROM Test WHERE id = ? AND password = password(?)`;
        let [user] = await promisePool.query(sql, [id, password]);
        if (user.length <= 0) {
            socket.send(JSON.stringify(new DataVO("errmsg", "없는 아이디 입니다.")));
            return false;
        }
        if(user[0].isUsing == 1){
            socket.send(JSON.stringify(new DataVO("errmsg", "지금 로그인 중인 아이디입니다.")));
            return false;
        }
        console.log(user);
        socket.user.uuid = user[0].code;

        sql = `UPDATE Test SET isUsing = 1 WHERE code = ?`
        await promisePool.query(sql, [user[0].code]);

        return true;
    }

    async ReadRecord(socket){
        let target = await this.GetUser(socket);
        console.log(target)
        socket.send(JSON.stringify(new DataVO("read", JSON.stringify(new RecordVO(target.kill, target.death, target.exp)))));
    }

    /**
     * 유저 정보 얻기
     * @param {string} uuid
     * @returns {User} 유저 객체
     */
    async GetUser(socket) {
        var user = new User();
        let sql = `SELECT * FROM Test WHERE code = ?`;
        let [result] = await promisePool.query(sql, [socket.user.uuid]);
        if (result.length > 0) // 유저가 있다면
        {
            user.nickname = result[0].id;
            user.id = socket.user.uuid;
            user.kill = result[0].kill;
            user.death = result[0].death;
            user.exp = result[0].exp;
        }
        else // 유저가 없으면
        {
            user = null;
        }
        
        return user;
    }

    async RecordUser(payload, socket){
        let {kill, death, exp} = JSON.parse(payload)
        let sql = `UPDATE Test SET \`kill\` = ?, \`death\` = ?, \`exp\` = ? WHERE code = ?`;
        let [result] = await promisePool.query(sql, [kill, death, exp, socket.user.uuid]);
    }

    /**
     * 유저 전적 얻기
     * @param {string} uuid
     * @returns {UserRecode} 유저 객체
     */
    async GetUserRecode(uuid) {
        var user = new User();
        // get user info in mysql database
        //TODO : 나중에 유저 전적 만들기
        let sql = `SELECT * FROM Test WHERE code = ?`;
        let [result] = await promisePool.query(sql, [uuid]);
        if (result.length > 0) // 유저가 있다면
        {
            
        }
        else // 유저가 없으면
        {
            user = null;
        }

        return user;
    }

    async SetOffline(socket){
        let sql = `UPDATE Test SET isUsing = 0 WHERE code = ?`;
        await promisePool.query(sql, [socket.user.uuid])
    }
}

exports.DBUtil = new DBUtil();