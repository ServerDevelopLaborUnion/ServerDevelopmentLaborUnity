import PlayerData from "../Data/Player/PlayerData.js";
import DataVO     from "../VO/DataVO.js";

export default class InitPlayerData{
    constructor(socket) {
        let playerData = JSON.stringify(new PlayerData(socket.socketId));
        let msg = JSON.stringify({ type:"initdata", payload:playerData });
        socket.send(msg);
    }
}