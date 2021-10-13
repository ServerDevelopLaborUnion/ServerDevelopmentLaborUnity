class Utile
{
    isUndefined(str) {
        if (typeof str == "undefined" || str == null || str == "")
            return true;
        else
            return false;
    }
}

exports.Utile = new Utile();