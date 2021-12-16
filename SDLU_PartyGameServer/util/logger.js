const fs = require('fs');

var Level = {
    DEBUG: 0,
    INFO: 1,
    WARN: 2,
    ERROR: 3,
    FATAL: 4
}

function getDate() {
    var date = new Date();
    var year = date.getFullYear();
    var month = date.getMonth() + 1;
    var day = date.getDate();
    return year + '-' + month + '-' + day;
}

class Log {
    constructor(level, path) {
        this.level = level;
        this.path = path;

        if (!fs.existsSync(path)) {
            fs.writeFileSync(path, '');
        }
        else {
            fs.appendFileSync(path, `${new Date().toISOString().replace('T', ' ').replace('Z', '')} =============== Logger started ==============\n`);
        }
    }

    logWrite(msg) {
        msg = `${new Date().toISOString().replace('T', ' ').replace('Z', '')} ${msg}`;
        console.log(msg);
        fs.appendFileSync(this.path, msg + '\n');
    }

    Debug(msg) {
        if (this.level <= Level.DEBUG) {
            msg = `[DEBUG]: ${msg}`;
            this.logWrite(msg);
        }
    }

    Info(msg) {
        if (this.level <= Level.INFO) {
            msg = `[INFO]: ${msg}`;
            this.logWrite(msg);
        }
    }

    Warn(msg) {
        if (this.level <= Level.WARN) {
            msg = `[WARN]: ${msg}`;
            this.logWrite(msg);
        }
    }

    Error(msg) {
        if (this.level <= Level.ERROR) {
            msg = `[ERROR]: ${msg}`;
            this.logWrite(msg);
        }
    }

    Fatal(msg) {
        if (this.level <= Level.FATAL) {
            msg = `[FATAL]: ${msg}`;
            this.logWrite(msg);
        }
    }
}

const Logger = new Log(Level.DEBUG, 'logs/' + getDate() + '.log');

module.exports = {
    Debug(msg) {
        Logger.Debug(msg);
    },
    Info(msg) {
        Logger.Info(msg);
    },
    Warn(msg) {
        Logger.Warn(msg);
    },
    Error(msg) {
        Logger.Error(msg);
    },
    Fatal(msg) {
        Logger.Fatal(msg);
    }
}