class User {
    /**
     * @param {WebSocket} socket
     * @param {string} sessionId
     * @param {string} uuid
     * @param {string} nickname
     * @param {number} kill
     * @param {number} death
     * @param {number} exp
     */
    constructor(socket, sessionId, uuid, nickname, kill, death, exp) {
        this.socket = socket;
        this.sessionId = sessionId;

        // 아래는 DB에서 불러올 정보
        this.uuid = uuid;
        this.nickname = nickname;
        this.kill = kill;
        this.death = death;
        this.exp = exp;

        this.gameUser = null;
    }
}

class UserRecord
{
    /**
     * @param {string} uuid
     */
    constructor(uuid) {
        this.uuid = uuid;

        // 아래는 DB에서 불러올 정보
    }
}

class Game
{
    constructor()
    {
        /** @type {number} */
        this.id = 0;
        /** @type {Array<GameUser>} */
        this.GameUsers = [];
        /** @type {number} */
        this.time = 0;
    }


    /**
     * @param {User} user
     */
    addUser(user)
    {
        this.GameUsers.push(new GameUser(user.uuid, user.nickname, 100));
    }

    removeUser(user)
    {
        this.GameUsers.forEach();
    }
}

class GameUser
{
    /**
     * @param {string} uuid
     * @param {string} nickname
     * @param {number} maxHp
     */
    constructor(uuid, nickname, maxHp) {
        this.uuid = uuid;
        this.nickname = nickname;
        this.maxHp = maxHp;
        this.hp = this.maxHp;
        this.score = 0;
        this.pos = [0, 0, 0];
        this.force = [0, 0, 0];
        this.animation = "Idle";
    }
}

exports.User = User;
exports.Game = Game;
exports.GameUser = GameUser;
exports.UserRecord = UserRecord;