import { WebSocketServer } from "ws";

const port = 32000;

const wsServer = new WebSocketServer({ port }, () => {
    console.log("서버가 잘 돌아가고 있습니다.");
});

wsServer.on("connection", socket => {

    console.log("누군가가 접속했어요.");

    socket.on("message", data => {
        try {
            const { type, payload } = JSON.parse(data);

            console.log(type);
            console.log(payload);

            switch (type) {
                case "nickname":
                    socket.nickname = JSON.parse(payload).nickname;
                    break;
                case "message":
                    broadcast(JSON.parse(payload).data);
                    break;
            }
        }
        catch (e)
        {
            console.log(e);
        }
        


    });

});

function broadcast(msg)
{
    wsServer.clients.forEach(socket => {
        socket.send(msg);
    });    
}


//on // 메세지 받을때
//send // 메세지 보낼때