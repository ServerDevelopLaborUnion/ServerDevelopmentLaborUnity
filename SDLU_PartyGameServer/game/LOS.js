const path = require('path');
const fs = require('fs');

const GameBase = require('../base/GameBase');

module.exports = class LOS extends GameBase {
    constructor(room) {
        super(1, "Last One Standing");
        this.handlers = {};
        const __dirname = path.resolve();
        const handlerFiles = fs.readdirSync(path.join(__dirname, 'game/LOS/')).filter(file => file.endsWith('.js'));
        handlerFiles.forEach(file => {
            if (file.startsWith('_')) return;
            const handler = require(path.join(__dirname, 'game/LOS/', file));
            this.handlers[handler.type] = handler;
            this.Logger.Debug('Game Handler loaded: ' + handler.type + '.js');
        });
    }

    handle(socket, payload) {
        const handler = this.handlers[payload.type];
        if (!handler) {
            this.Logger.Error(`Game Handler ${payload.type} not found`);
            return;
        }
        handler.handle(socket, payload);
    }
}