const { User } = require("../Types/Type");
const { UserUtil } = require("../Utils/UserUtil");

/**
 * 보내는 자를 제외한 모든 접속자에게 보내기
 * @param {User} sender
 * @param {string} msg
 */
function broadcast(msg) {
    try {
        UserUtil.getUsers().forEach(user => {
            user.socket.send(msg);
        });
    }
    catch (e) {
        // console.log(e);
    }
}

exports.broadcast = broadcast;