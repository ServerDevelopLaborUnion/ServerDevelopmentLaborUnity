import parseBuffer from '../Utility/ParseBuffer.js';

export default class LoginHandler {
    constructor(socket, payload) {
        let data = parseBuffer.parseBuffer(payload); // 추후 사용 예정

        socket.send(JSON.stringify("login", JSON.stringify({ "success": true, "whyfailed": "" })));
    }
}