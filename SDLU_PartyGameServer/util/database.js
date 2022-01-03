const request = require('request');
const url = "http://127.0.0.1/SDLU_PartyGameDatabaseServer/";
const Logger = require('../util/logger').Get('Database');

function sendPost(type, params) {

    request.post(url, {
        form: {
            type: type,
            data: params
        }
    }, (err, res, body) => {
        try {
            Logger.Debug(JSON.parse(body));
        } catch (e) {
            Logger.Error(e);
        }
    });
}

module.exports = {
    sendPost: sendPost
}

// 디버그
// sendPost("register", JSON.stringify({ id: "jan", pw: "1234" }));
// sendPost("login", JSON.stringify({ id: "jan", pw: "1234" }));