module.exports = class Vector3 {
    /**
     * 
     * @param {number} x 
     * @param {number} y 
     * @param {number} z 
     */
    constructor(x, y, z) {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    length() {
        return Math.sqrt(this.x * this.x + this.y * this.y + this.z * this.z);
    }

    normalize() {
        const length = this.length();
        if (length > 0) {
            const invLength = 1 / Math.sqrt(length);
            this.x *= invLength;
            this.y *= invLength;
            this.z *= invLength;
        }
        return this;
    }

    distance(v) {
        return Math.sqrt(Math.pow(this.x - v.x, 2) + Math.pow(this.y - v.y, 2) + Math.pow(this.z - v.z, 2));
    }

    add(v) {
        this.x += v.x;
        this.y += v.y;
        this.z += v.z;
    }

    sub(v) {   
        this.x -= v.x;
        this.y -= v.y;
        this.z -= v.z;
    }

    mul(v) {
        this.x *= v.x;
        this.y *= v.y;
        this.z *= v.z;
    }

    copy() {
        return new Vector3(this.x, this.y, this.z);
    }

    subtract(v) {
        return new Vector3(this.x - v.x, this.y - v.y, this.z - v.z);
    }

    multiply(v) {
        return new Vector3(this.x * v, this.y * v, this.z * v);
    }
}