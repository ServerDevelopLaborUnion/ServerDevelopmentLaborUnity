const { DataVO } = require("../VO/DataVO.js");

function connectionHandler(socket)
{
    socket.send(JSON.stringify(new DataVO("init", socket.sessionId, JSON.stringify({ id: socket.sessionId }))));
}

exports.connectionHandler = connectionHandler;