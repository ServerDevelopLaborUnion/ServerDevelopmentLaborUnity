const { DataVO } = require("../VO/DataVO.js");

function connectionHandler(socket)
{
    socket.send(JSON.stringify(new DataVO("init", { id: socket.id })));
}

exports.connectionHandler = connectionHandler;