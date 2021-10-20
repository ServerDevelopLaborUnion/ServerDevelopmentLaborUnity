const { UserRecode, User } = require("../Types/Type");
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
            return false;
        }
        promisePool.query(`INSERT INTO Test (id, password) VALUES (?, password(?))`, [id, password]);
        Login(id, password, socket);
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
        let sql = `SELECT * FROM Test WHERE id = ? AND password = password(?)`;
        let [user] = await promisePool.query(sql, [id, password]);
        console.log(user);
        if (user[0].id != id) {
            socket.send(JSON.stringify(new DataVO("errmsg", "없는 아이디 입니다.")));
            return false;
        }
        socket.user.uuid = user[0].code;

        return true;
    }

    /**
     * 유저 정보 얻기
     * @param {string} uuid
     * @returns {User} 유저 객체
     */
    async GetUser(uuid) {
        var user = new User();
        if (true) // 유저가 있다면
        {
            user.nickname = "닉!넴!";
        }
        else // 유저가 없으면
        {
            user = null;
        }

        return user;
    }

    /**
     * 유저 전적 얻기
     * @param {string} uuid
     * @returns {UserRecode} 유저 객체
     */
    async GetUserRecode(uuid) {
        var user = new User();
        if (true) // 유저가 있다면
        {
            // 전적 불러올 예정
        }
        else // 유저가 없으면
        {
            user = null;
        }

        return user;
    }
}

exports.DBUtil = new DBUtil();