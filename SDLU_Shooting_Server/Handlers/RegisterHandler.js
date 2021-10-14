const { DataVO } = require("../VO/DataVO");
const { User } = require("../Types/Type");
const pool = require("../DB")
const { UserUtil } = require("../Utils/UserUtil.js");
const { LoginHandler } = require("../Handlers/LoginHandler.js");

function loginCheck(id, password) {
    // DB에서 로그인 시도
    if (true) {
        return true;
    }
    else {
        return false;
    }
}

class RegisterHandler {
    /**
     * 회원가입 시도? 디코에서만 가능하게 할 수도..
     * @param {WebSocket} socket
     * @param {string} payload
     */
    Register(socket, payload) {
        socket.user = null;
        if (UserUtil.getUserBySocket(socket) == null) // 로그인이 되어있지 않다면..
        {
            if (register(socket, payload)) // DB에서 회원가입
            {
                LoginHandler.Login(socket, payload);
            }
            else // 실패 반환시
            {
                throw `${socket.sessionId} 의 요청: 회원가입\r\n이미 존제하는 계정입니다.`;
            }
        }
        else
        {
            throw `${socket.sessionId} 의 요청: 회원가입\r\n이미 로그인 되어 있습니다.`;
        }
    }
}

async function register(socket, payload){
    let {id, password} = JSON.parse(payload);
    console.log(id,password);
    let sql = `SELECT id FROM Test WHERE id = ?`;
    let [user] = await pool.query(sql,[id]);
    console.log(user);
    if(user.length > 0){
        socket.send(JSON.stringify(new DataVO("msg", "중복된 아이디 입니다.")));
        return false;
    }
    pool.query(`INSERT INTO Test (id, password) VALUES (?, password(?))`, [id, password]);
    return true;
}
exports.RegisterHandler = new RegisterHandler();