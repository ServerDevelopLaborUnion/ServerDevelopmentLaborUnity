const { DataVO } = require("../VO/DataVO.js");
const { spawnPositions } = require("../Vars/SpawnPosition.js");
const { DefaultValue } = require("../Vars/DefaultPlayerValue.js");

// 접속한 플레이어에게 보내 줌

function connectionHandler(socket)
{
    socket.send(JSON.stringify(new DataVO("init",
                JSON.stringify({
                    id: socket.sessionId,
                    pos: spawnPositions[Math.round(Math.random() * spawnPositions.length)],
                    hp: DefaultValue.HP
                }
                ))
    )); // 문자열로 바꿔줌
}

exports.connectionHandler = connectionHandler;