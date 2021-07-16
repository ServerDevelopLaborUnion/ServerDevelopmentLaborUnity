const ws = require('ws');
const port = 32000;

const wsService = new ws.Server({ port }, () => {
    console.log(`서버가 ${port} 에서 실행중입니다.`);

});



wsService.on("connection", socket => {

    console.log("클라이언트 접속");

    socket.on("close", () => {
        console.log("클라이언트 접속 종료");
    });

    socket.on("meessage", data => {

        console.log(data);
    });

});