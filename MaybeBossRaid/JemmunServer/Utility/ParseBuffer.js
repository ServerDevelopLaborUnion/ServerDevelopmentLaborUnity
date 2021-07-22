// 왜 이 함수를 사용하는지에 대한 주석
/* 
유니티에서 이 서버로 버퍼를 보낼 때, { "type": "TYPE" }, { "payload": "PAYLOAD" }
형식으로 보낼 거에요.(위에서 설명했긴 했지만)
하지만 저것이 문자열로 들어오게 됩니다.
그래서 JSON.parse(JSON문자열);
함수를 사용해 문자열에서 객채로 바꿔 줍니다.

*JSON 데이터를 기반으로 객채를 만들어서 리턴해줘요 * (클래스 만들어 준다는 것)

+
Javascript 는 형이 없는 언어에요.
let a = "asdasd";
let b = 15.0 f;
let c = { "Name": "SampleName", "Age": 0, "BirthDate": { "Year": 2004, "Month": 2, "Date": 25 } };
이 전부
let 하나로 가능해요.
*/

function parseBuffer(msg){
    try {
        // C#에서 const var, C/C++에서 cont auto(C#의 var 와 비슷한 기능을 함) 와 같은 의미입니다.
        const data = JSON.parse(msg);
        return data;
    } catch (e) {
        // 전달된 msg 가 JSON 형식이 아니라면 이 쪽으로 오게 됩니다.
        // try catch 문을 사용하지 않으면 잘못된 요청이 올 때 마다 서버가 터지게 됨

        console.log("클라이언트에서 잘못된 요청이 발생했어요.");
        console.log(`클라이언트에게서 받은 메세지: ${msg}`);
        console.log(e);
        return -1;
    }
}

export default { parseBuffer };