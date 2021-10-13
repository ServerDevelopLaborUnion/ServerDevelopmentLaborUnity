class Utile
{
    isUndefined(str) {
        return (typeof str == "undefined" || str == null || str == "");
    }
}

exports.Utile = new Utile();