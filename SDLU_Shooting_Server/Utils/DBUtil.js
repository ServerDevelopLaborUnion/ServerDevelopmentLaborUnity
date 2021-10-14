const { UserRecode, User } = require("../Types/Type");

class DBUtil {
    /**
     * 회원가입 요청
     * @param {string} id
     * @param {string} password
     * @returns {boolean} 성공 여부
     */
    async Register(id, password) 
    {


        return true;
    }

    /**
     * 로그인 요청
     * @param {string} id
     * @param {string} password
     * @param {WebSocket} socket
     * @returns {boolean} 성공 여부
     */
    async Login(id, password, socket)
    {
        try
        {
            socket.user = new User({
                socket: this.socket,
                sessionID: socket.id,
                uuid: "testUUID",
                nickname: "testNickname",
                level: 0,
                exp: 0,
                gameUser: null
            });
        }
        catch (e)
        {
            console.log(e.name);
        }
        return true;
    }

    /**
     * 유저 정보 얻기
     * @param {string} uuid
     * @returns {User} 유저 객체
     */
    async GetUser(uuid) 
    {
        var user = new User();
        if (true) // 유저가 있다면
        {
            user.nickname = "닉!넴!";
        }
        else // 유저가 없으면
        {
            user = null;
        }

        return user;
    }

    /**
     * 유저 전적 얻기
     * @param {string} uuid
     * @returns {UserRecode} 유저 객체
     */
    async GetUserRecode(uuid)
    {
        var user = new User();
        if (true) // 유저가 있다면
        {
            // 전적 불러올 예정
        }
        else // 유저가 없으면
        {
            user = null;
        }

        return user;
    }
}

exports.DBUtil = new DBUtil();