import parseBuffer from '../Utility/ParseBuffer.js';

export default class LoginHandler {
    constructor(payload) {
        this.data = parseBuffer.parseBuffer(payload);

        console.log(this.data);
    }
}