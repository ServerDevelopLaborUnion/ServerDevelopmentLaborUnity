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

const games = {};

const handlerFiles = fs.readdirSync('game').filter(file => file.endsWith('.js'));
handlerFiles.forEach(file => {
    const game = require(path.join('../game', file));
    games[game.id] = game;
    Logger.Debug('Game loaded: ' + new game(null).name);
});

module.exports = {
    type: "Game",
    async handle(socket, payload) {
        const data = new DataVO(payload);
        const gameData = new GameVO(data);
        const game = games[gameData.payload.type];
        if (!game) {
            Logger.Error(`Game ${gameData.payload.type} not found`);
            return;
        }
        game.handle(socket, gameData.payload);
    }
}