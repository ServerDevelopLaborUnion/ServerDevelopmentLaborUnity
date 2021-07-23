import parse    from '../Utility/ParseBuffer.js';
import AttackVO from '../VO/AttackVO.js';
import DataVO   from '../VO/DataVO.js';
import broadCast from '../Utility/WsBroadCast.js';


// target 에게 damage 전달
export default class AttackHandler{
    constructor(ws, payload) {
        let data = parse.parseBuffer(payload);
        let { target, skillEnum } = data;
        
        let attack = JSON.stringify(new AttackVO(target, skillEnum));
        let msg = JSON.stringify(new DataVO("attacked", attack));

        broadCast(ws, msg);
    } 
}