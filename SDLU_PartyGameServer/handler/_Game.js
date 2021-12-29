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

const handlerFiles = fs.readdirSync('handler').filter(file => file.endsWith('.js'));
handlerFiles.forEach(file => {
    const handler = require(path.join('../handler', file));
    this.handlers[handler.type] = handler;
    Logger.Debug('Handler loaded: ' + handler.type + '.js');
});

module.exports = {
    type: "Game",
    async handle(socket, payload) {
        const data = new DataVO(payload);
        const gameData = new GameVO(data);

    }
}