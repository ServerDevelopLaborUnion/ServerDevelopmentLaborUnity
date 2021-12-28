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
        // eslint-disable-next-line no-control-regex
        msg = msg.replace(/\x1b\[\d+m/g, '');
        fs.appendFileSync(this.path, msg + '\n');
    }

    Debug(prefix, msg) {
        if (this.level <= Level.DEBUG) {
            msg = `\x1b[32m[DEBUG]: ${prefix}\x1b[0m ${msg}`;
            this.logWrite(msg);
        }
    }

    Info(prefix, msg) {
        if (this.level <= Level.INFO) {
            msg = `\x1b[34m[INFO]: ${prefix}\x1b[0m ${msg}`;
            this.logWrite(msg);
        }
    }

    Warn(prefix, msg) {
        if (this.level <= Level.WARN) {
            msg = `\x1b[33m[WARN]: ${prefix}\x1b[0m ${msg}`;
            this.logWrite(msg);
        }
    }

    Error(prefix, msg) {
        if (this.level <= Level.ERROR) {
            msg = `\x1b[31m[ERROR]: ${prefix}\x1b[0m ${msg}`;
            this.logWrite(msg);
        }
    }

    Fatal(prefix, msg) {
        if (this.level <= Level.FATAL) {
            msg = `[FATAL]: ${prefix}\x1b[0m ${msg}`;
            this.logWrite(msg);
        }
    }
}

const Logger = new Log(Level.DEBUG, 'logs/' + getDate() + '.log');

module.exports = {
    Get(prefix) {
        return {
            Debug(msg) {
                Logger.Debug(`[${prefix}]`, `${msg}`);
            },
            Info(msg) {
                Logger.Info(`[${prefix}]`, `${msg}`);
            },
            Warn(msg) {
                Logger.Warn(`[${prefix}]`, `${msg}`);
            },
            Error(msg) {
                Logger.Error(`[${prefix}]`, `${msg}`);
            },
            Fatal(msg) {
                Logger.Fatal(`[${prefix}]`, `${msg}`);
            }
        }
    }
}