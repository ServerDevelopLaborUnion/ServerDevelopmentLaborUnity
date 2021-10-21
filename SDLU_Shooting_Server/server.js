const { WebSocketServer } = require("ws");
const { parseBuffer } = require("./Utils/ParseBuffer.js");
const { broadcast } = require("./Utils/Broadcast.js");
const { DataVO } = require("./VO/DataVO.js");
const { LoginHandler } = require("./Handlers/LoginHandler.js");
const { UserUtil } = require("./Utils/UserUtil.js");
const { RegisterHandler } = require("./Handlers/RegisterHandler.js");
const { connectionHandler } = require("./Handlers/ConnectionHandler.js");
const { userConnectedHandler } = require("./Handlers/UserConnectionHandler.js");
const { User, Game, GameUser } = require("./Types/Type");

const port = 32000;

const wsServer = new WebSocketServer({ port }, () => {
    console.log(`Server is running at port: ${port}`);
});

let id = 0;

let game = new Game();

wsServer.on("connection", socket => {

    //#region Connection
    socket.sessionId = ++id;
    console.log(`Client Connected. id: ${socket.sessionId}`);

    // connection packet
    userConnectedHandler(wsServer, socket);
    connectionHandler(socket);

    // user
    let user = new User(socket, id, "uuid", null, null, null, null);
    UserUtil.addUser(null, user);
    socket.user = user;

    //#endregion // Connection end

    // 임시로 로그인 시킴..
    LoginHandler.debugLogin(socket);

    socket.on("message", data => {
        try
        {
            const { type, payload } = parseBuffer(data);
            
            console.log(`[${socket.sessionId}] ${type}: ${payload}`);

            if (socket.user.uuid == null) // 로그인이 되어있지 않다면..
            {
                if (type == "login") {
                    LoginHandler.Login(socket, payload);
                }
                else if (type == "register")
                {
                    RegisterHandler.Register(socket, payload);
                }
                else
                    socket.send(JSON.stringify(new DataVO("errmsg", "로그인이 필요합니다.")));
            }
            else // 로그인이 되어있다면..
            {
                // User = 서버에 들어온 유저
                // GameUser = 게임중인 객체
                switch (type) {
                    case "msg":
                        broadcast(wsServer, socket, JSON.stringify(new DataVO("msg", payload)));
                        break;
                    case "damage":
                        broadcast(wsServer, socket, JSON.stringify(new DataVO("damage", payload)));
                        break;
                    case "shoot":
                        broadcast(wsServer, socket, JSON.stringify(new DataVO("shoot", payload)));
                        break;
                    default:
                        socket.send(JSON.stringify(new DataVO("errmsg", "그런 타입이 없습니다.")));
                }
            }
        }
        catch (e)
        {
            console.log(e);
            console.log(`${socket.sessionId} : ${data}`);
        }
    });
    
    socket.on("close", () => {
        UserUtil.removeUser(socket);
        console.log(`${socket.sessionId}: 접속 종료`);
    });

});