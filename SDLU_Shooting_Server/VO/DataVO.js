class DataVO
{
    constructor(type, id, payload)
    /**
     * @param {string} type
     * @param {string} payload
     */
    {
        this.type = type;
        this.id = id;
        this.payload = payload;
    }
}

exports.DataVO = DataVO;