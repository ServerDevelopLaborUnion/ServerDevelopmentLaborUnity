const { DataVO } = require("../VO/DataVO");
const { User } = require("../Types/Type");
const { UserUtil } = require("../Utils/UserUtil");
const { DBUtil } = require("../Utils/DBUtil")

class LoginHandler
{
    /**
     * 로그인 시도
     * @param {WebSocket} socket
     * @param {string} payload
     */
    async Login(socket, payload, game = null)
    {
        const { id, password } = JSON.parse(payload);
        if (await DBUtil.Login(id, password, socket))
        {
            var user = UserUtil.getUserBySocket(socket);
            console.log(`로그인 성공: ${user.nickname}`);
            socket.send(JSON.stringify(new DataVO("loginSuccess", "로그인 성공!")));
            if (game)
            {
                game.addUser(user);
            }
        }
        else
        {
            socket.send(JSON.stringify(new DataVO("errmsg", "아이디나 비밀번호가 다릅니다.")));
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