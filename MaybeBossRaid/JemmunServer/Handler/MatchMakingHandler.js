import ParseBuffer from "../Utility/ParseBuffer.js";
import DataVO from "../VO/DataVO.js";
import MatchMakingVO from "../VO/MatchMakingVO.js";
import broadCast from "../Utility/WsBroadCast.js";
import SocketStatus from "../Enum/SocketStatus.js";

export default class MatchMakingHandler{
    constructor(ws, socket, payload, playercount) {
        let matchmakeSize = 5;

        let { players, start } = ParseBuffer.parseBuffer(payload);

        // 메치메이킹 취소 체크
        if (start) {
            --playercount.count;
            socket.matchmaking = false;
        } else {
            ++playercount.count;
            socket.matchmaking = true;
        }

        let gameStart = playercount.count == matchmakeSize ? true : false; // 게임 시작 여부 저장
        let msg = JSON.stringify(new DataVO("matchmaking", JSON.stringify(new MatchMakingVO(playercount.count, gameStart))));

        broadCast(ws, msg);

        // 게임 시작 여부 체크
        if (gameStart) {
            broadCast(ws, JSON.stringify(new DataVO("gamestart", "null")), true);
        }
    }
}