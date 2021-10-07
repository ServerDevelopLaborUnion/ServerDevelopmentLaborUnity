class User {
    /**
     * @param {WebSocket} socket
     * @param {string} sessionId
     * @param {string} uuid
     * @param {string} nickname
     * @param {number} level
     * @param {number} exp
     * @param {number} score
     */
    constructor(socket, sessionId, uuid, nickname, level, exp, score) {
        this.socket = socket;
        this.sessionId = sessionId;

        // 아래는 DB에서 불러올 정보
        this.uuid = uuid;
        this.nickname = nickname;
        this.level = level;
        this.exp = exp;

        this.score = score;
    }
}

class UserRecord
{
    /**
     * @param {string} uuid
     */
    constructor(uuid) {
        // 아래는 DB에서 불러올 정보
        this.uuid = uuid;
    }
}

class GameUser 
{
    /**
     * @param {string} uuid
     * @param {string} nickname
     * @param {number} score
     */
    constructor(id, nickname, score) {
        this.uuid = uuid;
        this.nickname = nickname;
        this.score = score;
    }
}

exports.User = User;
exports.GameUser = GameUser;