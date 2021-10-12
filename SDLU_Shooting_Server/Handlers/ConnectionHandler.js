const { DataVO } = require("../VO/DataVO.js");

function connectionHandler(socket)
{
    socket.send(JSON.stringify(new DataVO("init", socket.id, JSON.stringify({ id: socket.id }))));
}

exports.connectionHandler = connectionHandler;