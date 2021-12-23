const request = require('request');
const url = "http://127.0.0.1/SDLU_PartyGameDatabaseServer/";


function sendPost(type, params) {

    request.post(url, {
        form: {
            type: type,
            data: params
        }
    }, (err, res, body) => {
        try {
            console.log(JSON.parse(body));
        } catch (e) {
            console.log("err\r\n" + body);
        }
    });
}

// 디버그
// sendPost("register", JSON.stringify({ id: "jan", pw: "1234" }));
sendPost("login", JSON.stringify({ id: "jan", pw: "1234" }));