const { WebSocketServer } = require("ws");
const { parseBuffer } = require("./Utils/ParseBuffer.js");
const { broadcast } = require("./Utils/Broadcast.js");
const { DataVO } = require("./VO/DataVO.js");
const { LoginHandler } = require("./Handlers/LoginHandler.js");
const { UserUtil } = require("./Utils/UserUtil.js");
const { RegisterHandler } = require("./Handlers/RegisterHandler.js");
const { connectionHandler } = require("./Handlers/ConnectionHandler.js");
const { userConnectedHandler } = require("./Handlers/UserConnectionHandler.js");
const { DamageHandler } = require("./Handlers/DamageHandler.js");
const port = 32000;

const wsServer = new WebSocketServer({ port }, () => {
    console.log(`Server is running at port: ${port}`);
});

let id = 0;

let bufferHandlerDict = []; // TODO : 잠만 이건 좀 고민을

wsServer.on("connection", socket => {

    socket.id = ++id;
    console.log(`클라이언트 접속. id: ${id}`);
    connectionHandler(socket);
    userConnectedHandler(wsServer, socket);

    // 임시로 로그인 시킴..
    LoginHandler.debugLogin(socket);

    socket.on("message", data => {
        try
        {
            const { type, payload } = parseBuffer(data);

            if (socket.user == null) // 로그인이 되어있지 않다면..
            {
                if (type == "login") {
                    LoginHandler.Login(socket, payload);
                    var User = UserUtil.getUser(socket);
                    socket.send(JSON.stringify(new DataVO("msg", socket.id, "로그인 성공!")));
                }
                else if (type == "register")
                {
                    RegisterHandler.Register(socket, payload);
                    socket.send(JSON.stringify(new DataVO("msg", socket.id, "회원가입 성공!")));
                }
                else
                    throw `${id} 의 요청: ${type}\r\n로그인이 필요합니다.`;
            }
            else // 로그인이 되어있다면..
            {
                switch (type) {
                    case "msg":
                        broadcast(wsServer, socket, JSON.stringify(new DataVO("msg", socket.id, payload)));
                        break;

                    case "damage":
                        broadcast(wsServer, socket, JSON.stringify(new DataVO("damage", socket.id, payload)));
                        break;
                    // dictionary 에 저장한 다음 불러오는 것도 나쁘지 않을수도 << (UUID, User) 사전으로 만들어 주세요 ^^7
                    // 일단 뭐 socket 안에 넣어둘게요

                    default:
                        throw `${socket.id} 의 요청: ${type}\r\n그런 타입이 없습니다.`;
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
        console.log(`${socket.id}: 접속 종료`);
    });

});