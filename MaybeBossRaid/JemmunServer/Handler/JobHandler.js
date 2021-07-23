import ParseBuffer from "../Utility/ParseBuffer.js"
import broadCast from "../Utility/WsBroadCast.js";
import DataVO from "../VO/DataVO.js";


export default class JobHandler{
    constructor(ws, socket, payload) {
        let { job, selected } = ParseBuffer.parseBuffer(payload);
        
        ++jobSelectedCount;
        socket.selectedJob = job; // 자바스크립트는 신이야.

        broadCast(ws, JSON.stringify(new DataVO("jobselect", payload)), false, socket, true);

        if (jobSelectedCount == 5) { // 전원 직업 선택
            broadCast(ws, JSON.stringify(new DataVO("battlestart", ""))); // 직업 선택했다고 보내줌
        }
    }
}

let jobSelectedCount = 0;