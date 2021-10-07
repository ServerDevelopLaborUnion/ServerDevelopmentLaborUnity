const { broadcast } = require("../Utils/Broadcast.js");
const { DataVO } = require("../VO/DataVO.js");

function userConnectedHandler(wsServer, socketid)
{
    broadcast(wsServer, socketid, JSON.stringify(new DataVO("connect", { id: socketid })));
}

exports.userConnectedHandler = userConnectedHandler;