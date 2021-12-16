const { DataVO } = require('../type/VO');

class TestVO {
    constructor(str, num) {
        this.str = str;
        this.num = num;
    }
}

module.exports = {
    type: "testMessage",
    async handle(socket, payload) {

    }
}