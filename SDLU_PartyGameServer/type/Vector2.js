module.exports = class Vector2 {
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
    }

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
}