class DataVO
{
<<<<<<< HEAD
    constructor(type, id, payload)
=======
    /**
     * @param {string} type
     * @param {string} payload
     */
    constructor(type, payload)
>>>>>>> 48d54cf1ed8b83871d12f05f2482e88a0fb0f3b1
    {
        this.type = type;
        this.id = id;
        this.payload = payload;
    }
}

exports.DataVO = DataVO;