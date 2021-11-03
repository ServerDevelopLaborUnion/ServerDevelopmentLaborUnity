const { DataVO } = require("../VO/DataVO.js");
const { spawnPositions } = require("../Vars/SpawnPosition.js");
const { DefaultValue } = require("../Vars/DefaultPlayerValue.js");
const { UserUtil } = require("../Utils/UserUtil.js");

// 접속한 플레이어에게 보내 줌


function connectionHandler(socket)
{
    const payload = JSON.stringify({
        id: socket.sessionId,
        pos: spawnPositions[Math.round(Math.random() * spawnPositions.length) % spawnPositions.length],
        hp: DefaultValue.HP,
        history: ""
    });

    console.log("ConnectionHandler: " + payload);

    socket.send(JSON.stringify(new DataVO("init", payload))); // 문자열로 바꿔줌
}



function toHistoryVO()
{
    let id = [];
    let pos = [];
    let hp = [];

    UserUtil.getUsers().forEach(s => {
        id.push(s.sessionId);
        pos.push(s.position);
        hp.push(s.hp);
    });

    return JSON.stringify({ id, pos, hp }); // TODO : HISTORY
}

exports.connectionHandler = connectionHandler;