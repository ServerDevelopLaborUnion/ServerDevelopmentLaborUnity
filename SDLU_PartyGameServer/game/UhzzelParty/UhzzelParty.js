const GameBase = require('../../base/GameBase');
const Vector2 = require('../../type/Vector2.js');
const { player2json } = require("../../util/players2json.js");

// r = 15

function getRandomStartPos() {
    let x = Math.random() * 30 - 15;
    let y = Math.random() * 30 - 15;
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
    }
    // update() {
    //     if (this.position.distance(this.movePos) > this.movePerSec) {
    //         let moveDir = this.movePos.subtract(this.position);
    //         moveDir.normalize();
    //         this.position.add(moveDir.multiply(this.movePerSec));
    //         return true;
    //     }
    //     else {
    //         return false;
    //     }
    // }
}
class Player extends Entity {
    constructor(socket, id, position, name) {
        super(id, position);
        this.socket = socket;
        this.name = name;
    }
}

class Game {
    constructor(players) {
        this.players = players;
        try {
            this.loopInterval = setInterval(() => {
                this.update();
            }, 1000 / 30);
        } catch (e) {
            console.log(e);
        }
    }

    update() {
        
    }
}


module.exports = class UhzzelParty extends GameBase {
    constructor(room) {
        super(0, "Uzzel Party");
        this.room = room;
        this.game = null;
    }

    start() {
        this.Logger.Debug("Game Started");
        const players = [];
        
        for (let i = 0; i < this.room.roomUsers.length; ++i) { // 플레이어 생성
            players.push(new Player(this.room.roomUsers[i], this.room.roomUsers[i].id, getRandomStartPos(), this.room.roomUsers.name));
        }
        this.room.broadcast({ type: gameinit, payload: player2json(players) }); // 플레이어 정보 클라이언트로 전달
        this.game = new Game(players);
    }

    stop() {
        
    }

    end() {
        this.Logger.Debug("Game ended");
        this.game.stop();
    }
}