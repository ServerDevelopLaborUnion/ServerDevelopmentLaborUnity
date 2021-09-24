function broadcast(wsServer, senderid, msg)
{
    wsServer.clients.forEach(s => {
        if (senderid != s.id) {
            s.send(msg);    
        }
    });
}

// 자신의 데이터는 받을 필요가 없으니.

exports.broadcast = broadcast;