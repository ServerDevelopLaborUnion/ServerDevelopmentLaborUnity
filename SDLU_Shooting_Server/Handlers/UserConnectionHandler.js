const { broadcast } = require("../Utils/Broadcast.js");
const { DataVO } = require("../VO/DataVO.js");

function userConnectedHandler(wsServer, socket)
{
    broadcast(wsServer, socket.id, JSON.stringify(new DataVO("connect", socket.id, JSON.stringify({ id: socket.id }))));
}

exports.userConnectedHandler = userConnectedHandler;