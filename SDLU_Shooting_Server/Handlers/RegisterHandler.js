const { DataVO } = require("../VO/DataVO");
const { User } = require("../Types/Type");
const { UserUtil } = require("../Utils/UserUtil.js");
const { LoginHandler } = require("../Handlers/LoginHandler.js");
const { DBUtil } = require("../Utils/DBUtil");

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
        let { id, password } = JSON.parse(payload);
        if (DBUtil.Register(id, password)) // DB에서 회원가입
        {
            LoginHandler.Login(socket, payload);
        }
        else // 실패 반환시
        {
            throw `${socket.sessionId} 의 요청: 회원가입\r\n이미 존재하는 계정입니다.`;
        }
    }
}

exports.RegisterHandler = new RegisterHandler();