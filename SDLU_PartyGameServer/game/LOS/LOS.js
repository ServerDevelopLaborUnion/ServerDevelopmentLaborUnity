const path = require('path');
const fs = require('fs');

const GameBase = require('../../base/GameBase');
const Vector2 = require('../../type/Vector2.js');

function getRandomPos(pos, radius) { // random direction and radius
    const angle = Math.random() * Math.PI * 2;
    const r = Math.random() * radius;
    const x = pos.x + Math.cos(angle) * r;
    const y = pos.y + Math.sin(angle) * r;
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
        this.movePerSec = 10;
    }

    update() {
        if (this.position.distance(this.movePos) > this.movePerSec) {
            let moveDir = this.movePos.subtract(this.position);
            moveDir.normalize();
            this.position.add(moveDir.multiply(this.movePerSec));
            return true;
        }
        else {
            return false;
        }
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
        this.randomTime = Math.random() * 1;
        do {
            this.movePos = getRandomPos(this.position, 10);
        } while (this.checkMap() == false);
    }

    checkMap() {
        if (this.position.x < -50) {
            return false;
        }
        if (this.position.x > 50) {
            return false;
        }
        if (this.position.y < -50) {
            return false;
        }
        if (this.position.y > 50) {
            return false;
        }
    }

    update() {
        this.randomTime -= 1 / 30;
        if (this.randomTime <= 0 || !super.update()) {
            this.randomTime = Math.random() * 1;
            do {
                this.movePos = getRandomPos(this.position, 10);
            } while (this.checkMap() == false);
            return false;
        } else {
            return true;
        }
    }
}

class Game {
    /**
     * 
     * @param {Array<Entity>} entities 
     */
    constructor(players, entities) {
        this.players = players;
        this.entities = entities;
        let loopCount = 0;
        try {
            this.loopInterval = setInterval(() => {
                this.update();
                loopCount++;
                console.log(loopCount);
            }, 1000 / 30);
        }
        catch (err) {
            console.log(err);
        }
    }

    /**
     * 
     * @param {Entity} entity 
     */
    broadcastEntityPos(entity) {
        this.entities.forEach(p => {
            if (p.id !== entity.id) {
                if (typeof p.socket !== 'undefined') {
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
            }
        });
    }

    update() {
        this.entities.forEach(entity => {
            if (entity.update()) {
                this.broadcastEntityPos(entity);
            }
        });
    }

    stop() {
        console.log("stop");
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
        for (let i = 0; i < this.room.roomUsers.length; i++) {
            entities.push(new Player(this.room.roomUsers[i], i, getRandomStartPos(), this.room.roomUsers[i].name));
        }
        entities.push(new Bot(entities.length, getRandomStartPos()));
        this.game = new Game(this.room.roomUsers, entities);
    }

    end() {
        this.Logger.Debug('Game ended');
        this.game.stop();
    }
}
