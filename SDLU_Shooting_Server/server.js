const { WebSocketServer } = require("ws");
const { parseBuffer } = require("./Utils/ParseBuffer.js");
const { broadcast } = require("./Utils/Broadcast.js");
const { DataVO } = require("./VO/DataVO.js");
const { LoginHandler } = require("./Handlers/LoginHandler.js");
const { UserUtil } = require("./Utils/UserUtil.js");
const { RegisterHandler } = require("./Handlers/RegisterHandler.js");
const { connectionHandler } = require("./Handlers/ConnectionHandler.js");
const { userConnectedHandler } = require("./Handlers/UserConnectionHandler.js");
const { User } = require("./Types/Type");

const port = 32000;

const wsServer = new WebSocketServer({ port }, () => {
    console.log(`Server is running at port: ${port}`);
});

let id = 0;

wsServer.on("connection", socket => {
    socket.id = ++id;
    console.log(`클라이언트 접속. id: ${id}`);
    connectionHandler(socket);
    userConnectedHandler(wsServer, socket);

    UserUtil.addUser(null, new User(socket, id, null, null, null, null, null));

    // 임시로 로그인 시킴..
    LoginHandler.debugLogin(socket);

    socket.on("message", data => {
        try
        {
            const { type, payload } = parseBuffer(data);

            if (socket.user.uuid == null) // 로그인이 되어있지 않다면..
            {
                if (type == "login") {
                    LoginHandler.Login(socket, payload);
                    socket.send(JSON.stringify(new DataVO("msg", socket.id, "로그인 성공!")));
                }
                else if (type == "register")
                {
                    RegisterHandler.Register(socket, payload);
                    socket.send(JSON.stringify(new DataVO("msg", socket.id, "회원가입 성공!")));
                }
                else
                    socket.send(new DataVO("errmsg", "로그인이 필요합니다."));
            }
            else // 로그인이 되어있다면..
            {
                switch (type) {
                    case "msg":
                        broadcast(wsServer, socket, JSON.stringify(new DataVO("msg", socket.id, payload)));
                        break;
                    case "damage":
                        broadcast(wsServer, socket, JSON.stringify(new DataVO("damage", payload)));
                        break;

                    default:
                        socket.send(new DataVO("errmsg", "그런 타입이 없습니다."));
                }
            }
        }
        catch (e)
        {
            console.log(e);
            console.log(`${id} : ${data}`);
        }
    });

    socket.on("close", () => {
        UserUtil.removeUser(socket);
        console.log(`${socket.id}: 접속 종료`);
    });

});