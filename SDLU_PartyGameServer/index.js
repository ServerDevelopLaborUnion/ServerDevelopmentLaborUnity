const Logger = require("./util/logger").Get("Main");

const { HttpServer } = require('./server/http-server');
const { WebsocketServer } = require('./server/websocket-server');

var httpServer = new HttpServer(8080);
var webSocketServer = new WebsocketServer(32000);

// eslint-disable-next-line no-undef
// process.on('uncaughtException', (err) => { 
//     Logger.Error(err);
// });

// eslint-disable-next-line no-undef
process.on('warning', (warning) => {
    Logger.Warn(warning);
});