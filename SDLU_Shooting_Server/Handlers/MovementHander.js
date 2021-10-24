const { UserUtil } = require("./Utils/UserUtil.js");
const { DataVO } = require("../VO/DataVO");
// 그러고보니 경로 표현할 때 윈도우에서는 \ <== 요거 씁니다.
// 윈도우가 알아서 변환함
// 리눅스 최고


function movementHander()
{
    let users =  UserUtil.getUsers();
}

