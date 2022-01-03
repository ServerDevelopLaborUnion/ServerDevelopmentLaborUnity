function player2json(players) { // Player list 를 json 으로 바꿔줌
    let vo = [];

    for (let i = 0; i < players.length; ++i) {
        vo.push(players.id, players.position);
    }

    return { playerList: JSON.stringify(vo) };
}

module.exports = {
    player2json: player2json
}