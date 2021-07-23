import PlayerData from "../Data/Player/PlayerData.js";
import DataVO     from "../VO/DataVO.js";

export default class InitPlayerData{
    constructor(wsService) {

        // 이야...

        wsService.clients.forEach(s => {
            if (s.selectedJob != -1) {
                let id = s.socketId;
                let hp = 100;
                let mp = 100;
                let job = s.selectedJob;
                
                let playerData = JSON.stringify(new PlayerDataVO(id, hp, mp, job));
                let msg = JSON.stringify(new DataVO("initdata", playerData));
                
                wsService.clients.forEach(socket => {
                    socket.send(msg);
                });
            }
        });

    }
}


class PlayerDataVO{
    constructor(id, hp, mp, job) {
        this.id = id;
        this.hp = hp;
        this.mp = mp;
        this.job = job;
    }
}