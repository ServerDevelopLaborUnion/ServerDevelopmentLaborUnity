export default function BroadCast(ws, msg) {

    ws.clients.forEach(socket => {
        socket.send(msg);
    });

}