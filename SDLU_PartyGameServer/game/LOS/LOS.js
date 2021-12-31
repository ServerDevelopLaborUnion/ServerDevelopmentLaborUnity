const path = require('path');
const fs = require('fs');

const Logger = require('../../util/logger').Get("LOS");
const GameBase = require('../../base/GameBase');

module.exports = class LOS extends GameBase {
    constructor(room) {
        super(1, "Last One Standing");
    }

    async start() {
        this.Logger.Debug('Game started');
    }
}