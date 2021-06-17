const ws = require('ws');

const wsc = new ws.Server({
    'port': 9800
}, function() {
    console.log(`9800 포트에서 서버가 실행중입니다`);
});

wsc.on("connection", function(socket) {
    console.log(socket);
    socket.send("Hello Unity");
});