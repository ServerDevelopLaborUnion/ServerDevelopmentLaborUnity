const { HttpServer } = require('./server/http-server');
const { WebsocketServer } = require('./server/websocket-server');

var httpServer = new HttpServer(8080);
var webSocketServer = new WebsocketServer(8081);