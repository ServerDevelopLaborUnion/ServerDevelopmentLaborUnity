const { User } = require("../Types/Type");

/**
 * 보내는 자를 제외한 모든 접속자에게 보내기
 * @param {User} sender
 * @param {string} msg
 */
function broadcast(wsServer, sender, msg)
{
    wsServer.clients.forEach(s => {
        //if (sender.sessionId != s.user.uuid) {
            s.send(msg);
        //}
    });
}

exports.broadcast = broadcast;