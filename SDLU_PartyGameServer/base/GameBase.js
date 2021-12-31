module.exports = class GameBase {
    constructor(id, name, room) {
        this.handlers = {};
        this.id = id;
        this.name = name;
        this.room = room;
        this.Logger = require('../util/logger').Get(this.name);
        this.Logger.Debug(`Game loaded: ${this.name}`);
    }

    start() {
        throw new Error("Method not implemented.");
    }

    stop() {
        throw new Error("Method not implemented.");
    }

    async handle(socket, payload) {
        const handlers = socket.globalObj.games[this.id].handlers;
        if (handlers[payload.type]) {
            handlers[payload.type](socket, payload);
        } else {
            this.Logger.Error(`No handler for type: ${payload.type}`);
        }
    }
}
