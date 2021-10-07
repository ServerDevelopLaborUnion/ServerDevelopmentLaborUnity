const { DataVO } = require("../VO/DataVO");
const { User } = require("../Types/Type");
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
        const { id, password } = JSON.parse(payload);;
        if (UserUtil.getUserBySocket(socket) == null) // 로그인이 되어있지 않다면..
        {
            if (true) // DB에서 회원가입
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

exports.RegisterHandler = new RegisterHandler();