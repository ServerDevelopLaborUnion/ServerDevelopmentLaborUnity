import ParseBuffer from "../Utility/ParseBuffer.js";
import DataVO from "../VO/DataVO.js";
import MatchMakingVO from "../VO/MatchMakingVO.js";
import BroadCast from "../Utility/WsBroadCast.js";

export default class MatchMakingHandler{
    constructor(ws, playercount) {
        let msg = JSON.stringify(new DataVO("matchmaking", JSON.stringify(new MatchMakingVO(playercount, playercount == 4 ? true : false))));

        BroadCast(ws, msg);
    }
}