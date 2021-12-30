module.exports = class GameBase {
    constructor(id, name, room) {
        this.handlers = {};
        this.id = id;
        this.name = name;
        this.room = room;
        this.Logger = require('../util/logger').Get(this.name);
    }

    start() {
        throw new Error("Method not implemented.");
    }

    stop() {
        throw new Error("Method not implemented.");
    }

    handle(payload) {
        throw new Error("Method not implemented.");
    }
}
