const { WebSocketServer } = require("ws");


const port = 32000;


const wsServer = new WebSocketServer({ port }, () => {
    console.log(`Server is running at port: ${port}`);
});

wsServer.on("connection", socket => {
    socket.on("message", data =>{
        
        const { type, payload } = JSON.parse(data);

        console.log(payload);
        socket.send(payload);
        
    });
});