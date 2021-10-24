const { User } = require("../Types/Type");
const { UserUtil } = require("../Utils/UserUtil");

/**
 * 보내는 자를 제외한 모든 접속자에게 보내기
 * @param {User} sender
 * @param {string} msg
 */
function broadcast(msg)
{
    UserUtil.getUsers().forEach(s => {
        s.socket.send(msg);
    });
}

exports.broadcast = broadcast;