const { WebSocketServer } = require("ws");
const { parseBuffer } = require("./Utils/ParseBuffer.js");
const { broadcast } = require("./Utils/Broadcast.js");
const { DataVO } = require("./VO/DataVO.js");
const { LoginHandler } = require("./Handlers/LoginHandler.js");
const { UserUtil } = require("./Utils/UserUtil.js");

const port = 32000;

const wsServer = new WebSocketServer({ port }, () => {
    console.log(`Server is running at port: ${port}`);
});

let id = 0;

let bufferHandlerDict = []; // TODO : 잠만 이건 좀 고민을

wsServer.on("connection", socket => {

    socket.sessionId = ++id;
    console.log(`클라이언트 접속. id: ${id}`);

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
                    console.log(User.id);
                }
                else
                    throw `${id} 의 요청: ${type}\r\n로그인이 필요합니다.`;
            }
            else // 로그인이 되어있다면..
            {
                switch (type) {
                    case "msg":
                        broadcast(wsServer, socket.sessionId, JSON.stringify(new DataVO("msg", payload)));
                        break;
                    // dictionary 에 저장한 다음 불러오는 것도 나쁘지 않을수도 << (UUID, User) 사전으로 만들어 주세요 ^^7
                    // 일단 뭐 socket 안에 넣어둘게요

                    default:
                        throw `${id} 의 요청: ${type}\r\n그런 타입이 없습니다.`;
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
        console.log(`${id}: 접속 종료`);
    });

});