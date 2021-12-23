const request = require('request');
const url = "http://127.0.0.1/SDLU_PartyGameDatabaseServer/";


function sendPost(type, params) {

    request.post(url, { form: { type: type } }, (err, res, body) => {
        console.log(JSON.parse(body));
    });
}

sendPost("login");