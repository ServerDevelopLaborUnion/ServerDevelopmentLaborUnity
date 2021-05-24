class Vector3 {
    constructor(x, y, z) {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    static zero = new Vector3(0, 0, 0);
}

module.exports = Vector3;