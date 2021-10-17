const { broadcast } = require("../Utils/Broadcast.js");
const { spawnPositions } = require("../Vars/SpawnPosition.js");
const { DefaultValue } = require("../Vars/DefaultPlayerValue.js");
const { DataVO } = require("../VO/DataVO.js");

// 유저가 접속했을 때 호출

function userConnectedHandler(wsServer, socket)
{
    broadcast(wsServer, socket, JSON.stringify({
                                id: socket.sessionId,
                                pos: spawnPositions[Math.random() * (spawnPositions.length - 1)],
                                hp: DefaultValue.hp
                                }
    )); // 문자열로 바꿔줌

}

exports.userConnectedHandler = userConnectedHandler;