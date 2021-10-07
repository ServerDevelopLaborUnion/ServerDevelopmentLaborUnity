class DataVO
{
    /**
     * @param {string} type
     * @param {string} payload
     */
    constructor(type, payload)
    {
        this.type = type;
        this.payload = payload;
    }
}

exports.DataVO = DataVO;