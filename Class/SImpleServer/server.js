import { WebSocketServer } from "ws";

const port = 32000;

const wsServer = new WebSocketServer({ port }, () => {
    console.log("서버가 잘 돌아가고 있습니다.");
});

wsServer.on("connection", socket => {

    console.log("누군가가 접속했어요.");

    socket.on("message", data => {
        console.log(data.toString());

        broadcast(data.toString());
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