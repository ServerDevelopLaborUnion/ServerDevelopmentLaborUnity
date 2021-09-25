const { WebSocketServer } = require("ws");
const { parseBuffer } = require("./Utils/ParseBuffer.js");
const { broadcast } = require("./Utils/Broadcast.js");
const { DataVO } = require("./VO/DataVO.js")

const port = 32000;

const wsServer = new WebSocketServer({ port }, () => {
    console.log(`Server is running at port: ${port}`);
});

let id = 0;

let bufferHandlerDict = []; // TODO : 잠만 이건 좀 고민을

wsServer.on("connection", socket => {

    socket.id = ++id;
    console.log(`클라이언트 접속. id: ${id}`);

    socket.on("message", data => {
        try
        {
            const { type, payload } = parseBuffer(data);
            
            switch (type)
            {
                case "msg":
                    broadcast(wsServer, socket.id, JSON.stringify(new DataVO("msg", payload)));
                    break;
                // dictionary 에 저장한 다음 불러오는 것도 나쁘지 않을수도

                default:
                    throw `${id} 의 요청: ${type}\r\n그런 타입이 없습니다.`;
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