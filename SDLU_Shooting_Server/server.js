const { WebSocketServer } = require("ws");
const { parseBuffer } = require("./Utils/ParseBuffer.js");
const { broadcast } = require("./Utils/Broadcast.js");
const { DataVO } = require("./VO/DataVO.js");
const { LoginHandler } = require("./Handlers/LoginHandler.js");
const { UserUtil } = require("./Utils/UserUtil.js");
const { RegisterHandler } = require("./Handlers/RegisterHandler.js");
const { connectionHandler } = require("./Handlers/ConnectionHandler.js");
const { userConnectedHandler } = require("./Handlers/UserConnectionHandler.js");
const { User, Game } = require("./Types/Type");
const { DBUtil } = require("./Utils/DBUtil.js");
const { DamageVO } = require("./VO/DamageVO.js");
const { ChatHandler } = require("./Handlers/ChatHandler.js");

const port = 32000;

const wsServer = new WebSocketServer({ port }, () => {
    console.log(`Server is running at port: ${port}`);
});

let id = 0;

let game = new Game();

wsServer.on("connection", socket => {

    //#region Connection
    socket.sessionId = ++id;
    socket.hp = 100;
    console.log(`Client Connected. id: ${socket.sessionId}`);

    
    // user
    let user = new User(socket, id, null, null, null, null, null);
    socket.user = user;
    user.socket = socket;
    UserUtil.addUser(null, user);
    
    // connection packet
    connectionHandler(socket);
    userConnectedHandler(socket);

    //#endregion // Connection end

    // 임시로 로그인 시킴..
    // LoginHandler.debugLogin(socket);

    socket.on("message", data => {
        try {
            const { type, payload } = parseBuffer(data);

            if (socket.user.uuid == null) // 로그인이 되어있지 않다면..
            {
                if (type == "login") {
                    LoginHandler.Login(socket, payload, game);
                }
                else if (type == "register") {
                    RegisterHandler.Register(socket, payload, game);
                }
                else
                    socket.send(JSON.stringify(new DataVO("errmsg", "로그인이 필요합니다.")));
            }
            else // 로그인이 되어있다면..
            {
                // User = 서버에 들어온 유저
                // GameUser = 게임중인 객체
                switch (type) {
                    case "dead":
                    case "msg":
                    case "shoot":
                    case "move":
                        broadcast(JSON.stringify(new DataVO(type, payload)));
                        break;
                    case "damage":
                        console.log(`[${socket.sessionId}] ${type}: ${payload}` + ", HP: " + UserUtil.getUserBySessionId(id).hp);
                        if (isNaN(UserUtil.getUserBySessionId(id).hp))
                            UserUtil.getUserBySessionId(id).hp = 100;
                        var { id, damage } = payload;
                        damage = Number(damage);
                        UserUtil.getUserBySessionId(id).hp -= 20;

                        if (UserUtil.getUserBySessionId(id).hp <= 0)
                        {
                            console.log("dead: " + id);
                            broadcast(JSON.stringify(new DataVO("dead", payload)));
                            socket.disconnect();
                        }
                        else
                            broadcast(JSON.stringify(new DataVO(type, payload)));
                        break;
                    case "record":
                        console.log(`[${socket.sessionId}] ${type}: ${payload}`);
                        DBUtil.RecordUser(payload, socket);
                        break;
                    case "read":
                        console.log(`[${socket.sessionId}] ${type}: ${payload}`);
                        DBUtil.ReadRecord(socket);
                        break;
                    case "chat":
                        console.log(`[${socket.sessionId}] ${type}: ${payload}`);
                        ChatHandler.Chat(socket, payload);
                        break;
                    default:
                        socket.send(JSON.stringify(new DataVO("errmsg", "그런 타입이 없습니다.")));
                }
            }
        }
        catch (e) {
            console.log(e);
            console.log(`${socket.sessionId} : ${data}`);
        }
    });

    socket.on("close", () => {
        UserUtil.removeUser(socket);
        game.removeUser(socket);
        console.log(`${socket.sessionId}: 접속 종료`);
    });
});