class User {
    /**
     * @param {WebSocket} socket
     * @param {string} sessionId
     * @param {string} uuid
     * @param {string} nickname
     * @param {number} level
     * @param {number} exp
     * @param {number} score
     * @param {GameUser} gameUser
     */
    constructor(socket, sessionId, uuid, nickname, level, exp, gameUser) {
        this.socket = socket;
        this.sessionId = sessionId;

        // 아래는 DB에서 불러올 정보
        this.uuid = uuid;
        this.nickname = nickname;
        this.level = level;
        this.exp = exp;

        this.gameUser = gameUser;
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

class GameUser 
{
    /**
     * @param {string} uuid
     * @param {string} nickname
     * @param {number} score
     * @param {string} animation
     */
    constructor(id, nickname, score, pos, force, animation) {
        this.uuid = uuid;
        this.nickname = nickname;
        this.score = score;
        this.pos = pos;
        this.force = force;
        this.animation = animation;
    }
}

exports.User = User;
exports.GameUser = GameUser;