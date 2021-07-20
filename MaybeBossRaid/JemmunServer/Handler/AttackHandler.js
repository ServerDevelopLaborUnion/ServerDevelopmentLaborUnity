import parse    from '../ParseBuffer.js';
import AttackVO from '../VO/AttackVO.js';
import DataVO   from '../VO/DataVO.js';

// target 에게 damage 전달
export default class AttackHandler{
    constructor(socket, payload) {
        let data = parse.parseBuffer(payload);
        let { target, skillEnum } = data;
        
        let attack = JSON.stringify(new AttackVO(target, skillEnum));
        let vo = new DataVO("attacked", attack);

        console.log(skillEnum);

        socket.send(JSON.stringify(vo));
    } 
}