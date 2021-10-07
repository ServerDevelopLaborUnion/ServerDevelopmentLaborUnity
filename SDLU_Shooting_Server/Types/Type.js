class User {
    /**
     * @param {WebSocket} socket
     * @param {string} uuid
     * @param {string} nickname
     * @param {number} score
     */
    constructor(socket, uuid, nickname, score) {
        this.socket = socket;
        this.uuid = uuid;
        this.nickname = nickname;
        this.score = score;
    }
}

class GameUser {
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