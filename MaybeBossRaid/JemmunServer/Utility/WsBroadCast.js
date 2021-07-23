export default function broadCast(ws, msg, matchmaking = false) {

    if (matchmaking) {
        ws.clients.forEach(socket => {
            if (socket.matchmaking == true) {
                socket.send(msg);
            }
        });
        return;
    }

    ws.clients.forEach(socket => {
        socket.send(msg);
    });

}

// 무언가 확실히 잘못된 코드
// 2학기 성과물에서 수정해야할