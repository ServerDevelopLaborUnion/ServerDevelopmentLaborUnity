const path = require('path');
const fs = require('fs');

const GameBase = require('../../base/GameBase');
const Vector2 = require('../../type/Vector2.js');

function getRandomPos(pos, radius) {
    let x = pos.x + Math.random() * radius * 2 - radius;
    let y = pos.y + Math.random() * radius * 2 - radius;
    return new Vector2(x, y);
}

function getRandomStartPos() {
    let x = Math.random() * 100 - 50;
    let y = Math.random() * 100 - 50;
    return new Vector2(x, y);
}
class Entity {
    /**
     * 
     * @param {number} id 
     * @param {Vector2} position 
     */
    constructor(id, position) {
        this.id = id;
        this.position = position;
        this.movePos = position;
        this.movePerSec = 1;
    }

    update() { // this is called every 1/30 second
        // move to movePos with movePerSec
        // if entity moved return true
        let moved = false;
        if (this.movePos.x != this.position.x) {
            if (this.movePos.x > this.position.x) {
                this.position.x += this.movePerSec;
                if (this.position.x >= this.movePos.x) {
                    this.position.x = this.movePos.x;
                }
            } else {
                this.position.x -= this.movePerSec;
                if (this.position.x <= this.movePos.x) {
                    this.position.x = this.movePos.x;
                }
            }
            moved = true;
        }
        if (this.movePos.y != this.position.y) {
            if (this.movePos.y > this.position.y) {
                this.position.y += this.movePerSec;
                if (this.position.y >= this.movePos.y) {
                    this.position.y = this.movePos.y;
                }
            } else {
                this.position.y -= this.movePerSec;
                if (this.position.y <= this.movePos.y) {
                    this.position.y = this.movePos.y;
                }
            }
            moved = true;
        }
        return moved;
    }
}

class Player extends Entity {
    constructor(socket, id, position, name) {
        super(id, position);
        this.socket = socket;
        this.name = name;
    }
}

class Bot extends Entity {
    constructor(id, position) {
        super(id, position);
    }
}

class Game {
    /**
     * 
     * @param {number} id
     * @param {Array<Entity>} entities 
     */
    constructor(id, players, entities) {
        this.id = id;
        this.players = players;
        this.entities = entities;
        this.loopInterval = setInterval(() => {
            this.update();
        }, 1000 / 30);
    }

    /**
     * 
     * @param {Entity} entity 
     */
    broadcastPlayerPos(entity) {
        this.entities.forEach(p => {
            if (p.id !== entity.id) {
                p.socket.send(JSON.stringify({
                    type: "Game",
                    payload: {
                        id: this.id,
                        payload: {    
                            type: "playerPos",
                            payload: {
                                id: entity.id,
                                x: entity.position.x,
                                y: entity.position.y
                            }
                        }
                    }
                }));
            }
        });
    }

    update() {
        this.entities.forEach(entity => {
            if (entity.update()) {
                this.broadcastPlayerPos(entity);
            }
        });
    }

    stop() {
        clearInterval(this.loopInterval);
    }
}

module.exports = class LOS extends GameBase {
    constructor(room) {
        super(1, "Last One Standing");
        this.room = room;
        this.game = null;
    }

    async start() {
        this.Logger.Debug('Game started');
        const entities = [];
        for (let i = 0; i < this.room.playerCount; i++) {
            entities.push(new Player(this.room.players[i].socket, i, getRandomStartPos(), this.room.players[i].name));
        }
    }

    end() {
        this.Logger.Debug('Game ended');
        this.game.stop();
    }
}