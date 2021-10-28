const { UserUtil } = require("./Utils/UserUtil.js");
const { DataVO } = require("../VO/DataVO");
const {broadcast} = require("../Utils/Broadcast.js");
// 그러고보니 경로 표현할 때 윈도우에서는 \ <== 요거 씁니다.
// 윈도우가 알아서 변환함
// 리눅스 최고


function movementHander(message)
{
    let users =  UserUtil.getUsers();
    let  {pos , rot , id } = JSON.parse(message);
    broadcast(message);
    /*로테이션 하고 포지션하고 아이디 받아 */
}