const { DataVO } = require("../VO/DataVO");
const { User } = require("../Types/Type");
const { UserUtil } = require("../Utils/UserUtil.js");
const { LoginHandler } = require("../Handlers/LoginHandler.js");
//const { DBUtil } = require("../Utils/DBUtil");

function loginCheck(id, password) {
    // DB에서 로그인 시도
    // DB에서 아이디가 존재하는지 확인
    // 아이디가 존재하면 비밀번호가 맞는지 확인
    // 비밀번호가 맞으면 로그인 성공
    // 비밀번호가 틀리면 로그인 실패

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
    async Register(socket, payload) {
        let { id, password } = JSON.parse(payload);

        if (await DBUtil.Register(id, password, socket)) // DB에서 회원가입
        {
            LoginHandler.Login(socket, payload);
            socket.send(JSON.stringify(new DataVO("msg", "회원가입 성공!")));
        }
        else // 실패 반환시
        {
            console.log(`${socket.sessionId} 의 요c청: 회원가입\r\n이미 존재하는 계정입니다.`);
        }
    }
}

exports.RegisterHandler = new RegisterHandler();