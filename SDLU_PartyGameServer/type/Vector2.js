module.exports = class Vector2 {
    /**
     * 
     * @param {number} x 
     * @param {number} y 
     */
    constructor(x, y) {
        this.x = x;
        this.y = y;
    }

    length() {
        return Math.sqrt(this.x * this.x + this.y * this.y);
    }

    normalize() {
        const length = this.length();
        if (length > 0) {
            const invLength = 1 / Math.sqrt(length);
            this.x *= invLength;
            this.y *= invLength;
        }
        return this;
    }

    /**
     * 
     * @param {Vector2} v 
     */
    add(v) {
        this.x += v.x;
        this.y += v.y;
    }

    sub(v) {
        this.x -= v.x;
        this.y -= v.y;
    }

    mul(v) {
        this.x *= v.x;
        this.y *= v.y;
    }

    distance(v) {
        return Math.sqrt(Math.pow(this.x - v.x, 2) + Math.pow(this.y - v.y, 2));
    }

    subtract(v) {
        return new Vector2(this.x - v.x, this.y - v.y);
    }

    multiply(v) {
        return new Vector2(this.x * v, this.y * v);
    }

    copy() {
        return new Vector2(this.x, this.y);
    }
}