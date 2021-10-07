const { DataVO } = require("../VO/DataVO");
const { User } = require("../Types/Type");
const { UserUtil } = require("../Utils/UserUtil.js");

class LoginHandler
{
    /**
     * 로그인 시도
     * @param {WebSocket} socket
     * @param {string} payload
     */
    Login(socket, payload)
    {
        socket.user = null;
        const { id, password } = JSON.parse(payload);
        if (true)
        {
            // 어찌저찌 DB에서 유저 데이터 가져오기 ( 일단은 아무 정보나 넣을거임 )
            socket.user = new User(socket, 0, "testUser", 0);

            var user = UserUtil.getUserBySocket(socket);
            console.log(`로그인 성공: ${user.nickname}`);
        }
        else
        {
            socket.send(new DataVO("errmsg", "아이디나 비밀번호가 다릅니다."));
        }
    }

    /**
     * 디버그 로그인
     * @param {WebSocket} socket
     */
    debugLogin(socket)
    {
        socket.user = new User(socket, "ThisIsUUID", "debugUser", 123456);
        console.log(`디버그 로그인: ${socket.user.nickname}`);
    }
}

exports.LoginHandler = new LoginHandler();