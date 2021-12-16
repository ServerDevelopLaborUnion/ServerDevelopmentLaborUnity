class DataVO {
    constructor(type, payload) {
        this.type = type;
        this.payload = payload;
    }

    toJSON() {
        return JSON.stringify(this);
    }
}

exports.DataVO = DataVO;