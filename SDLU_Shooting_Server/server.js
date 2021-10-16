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
    socket.sessionId = ++id;
    console.log(`클라이언트 접속. id: ${id}`);
    userConnectedHandler(wsServer, socket);
<<<<<<< Updated upstream
    
    UserUtil.addUser(null, new User(socket, ++id, null, null, null, null, null));
    
=======

    let user = new User(socket, id, null, null, null, null, null);
    UserUtil.addUser(null, user);

    socket.user = user;

>>>>>>> Stashed changes
    // 임시로 로그인 시킴..
    //LoginHandler.debugLogin(socket);

    socket.on("message", data => {
        try
        {
            const { type, payload } = parseBuffer(data);
<<<<<<< Updated upstream
            
=======
            console.log(payload);

>>>>>>> Stashed changes
            if (socket.user.uuid == null) // 로그인이 되어있지 않다면..
            {
                if (type == "login") {
                    LoginHandler.Login(socket, payload);
                    socket.send(JSON.stringify(new DataVO("msg", "로그인 성공!")));
                }
                else if (type == "register")
                {
                    RegisterHandler.Register(socket, payload);
                    socket.send(JSON.stringify(new DataVO("msg", "회원가입 성공!")));
                }
                else
<<<<<<< Updated upstream
                socket.send(new DataVO("errmsg", "로그인이 필요합니다."));
=======
                    socket.send(JSON.stringify(new DataVO("errmsg", "로그인이 필요합니다.")));
>>>>>>> Stashed changes
            }
            else // 로그인이 되어있다면..
            {
                // 설명충 원석이의 설명
                // User = 서버에 들어온 유저
                // GameUser = 게임중인 객체
                switch (type) {
                    case "msg":
                        broadcast(wsServer, socket, JSON.stringify(new DataVO("msg", payload)));
                        break;
                    case "damage":
                        broadcast(wsServer, socket, JSON.stringify(new DataVO("damage", payload)));
                        break;
<<<<<<< Updated upstream
                    case "init":
                        // 생성되면 실시간 통신을 시작한다
                        // Game.Broadcast(socket, data); 이용해서 데이터 뿌려주기
                        connectionHandler(socket);
                        break;
                        
                        default:
                            socket.send(new DataVO("errmsg", "그런 타입이 없습니다."));
                        }
=======

                    default:
                        socket.send(JSON.stringify(new DataVO("errmsg", "그런 타입이 없습니다.")));
                }
>>>>>>> Stashed changes
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