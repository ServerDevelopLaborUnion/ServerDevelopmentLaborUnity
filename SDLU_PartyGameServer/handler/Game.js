const { DataVO } = require('../type/VO');
const fs = require('fs');
const path = require('path');
const Logger = require('../util/logger').Get("MatchMaking");

class GameVO {
    constructor(data) {
        this.id = 0;
        this.payload = data.payload;
    }
}

module.exports = {
    type: "Game",
    async handle(socket, payload) {
        const data = new DataVO(payload);
        const game = socket.user.room.game;
        if (!game) {
            Logger.Error("User is not in a game");
            return;
        }
        const gameVO = new GameVO(data);
        game.handle(socket, gameVO.payload);
    }
}