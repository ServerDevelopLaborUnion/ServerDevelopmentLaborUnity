const { broadcast } = require("../Utils/Broadcast");
const { Vector3 } = require("../Utils/Vector3");
const { DataVO } = require("../VO/DataVO");
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
        /** @type {number} */
        this.hp = 100;

        /** @type {GameUser} */
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
        //this.GameUsers.push(new GameUser(user.uuid, user.nickname, 100));
        var gameuser = new GameUser(user.uuid, user.nickname, 100);
        this.GameUsers.push(gameuser);
        user.gameUser = gameuser;
        user.hp = 100;
    }

    getUser(uuid)
    {
        for(var i = 0; i < this.GameUsers.length; i++)
        {
            if(this.GameUsers[i].uuid == uuid)
            {
                return this.GameUsers[i];
            }
        }
        return null;
    }

    getUsers()
    {
        var users = [];
        for(var i = 0; i < this.GameUsers.length; i++)
        {
            users.push(this.GameUsers[i].toDataVO());
        }
        return users;
    }

    getUserByNickname(nickname)
    {
        for(var i = 0; i < this.GameUsers.length; i++)
        {
            if(this.GameUsers[i].nickname == nickname)
            {
                return this.GameUsers[i];
            }
        }
        return null;
    }

    removeUser(user)
    {
        for(var i = 0; i < this.GameUsers.length; i++)
        {
            if(this.GameUsers[i].uuid == user.uuid)
            {
                this.GameUsers.splice(i, 1);
                broadcast(new DataVO("disconnect", user.sessionId));
                break;
            }
        }
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
        this.pos = new Vector3(0, 0, 0);
        this.force = new Vector3(0, 0, 0);
        this.animation = "Idle";
    }
}

exports.User = User;
exports.Game = Game;
exports.GameUser = GameUser;
exports.UserRecord = UserRecord;