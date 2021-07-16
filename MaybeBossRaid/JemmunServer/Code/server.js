const ws = require('ws');
const port = 32000;

const wsService = new ws.Server({ port }, () => {
    console.log(`서버가 ${port} 에서 실행중입니다.`);

});

// on 뒤에 붙는 문자열과 왜 Json 으로 보내는지에 대한 주석
//#region
/*
wsService.on("connection", 람다 식);
socket.on("message 또는 close 등등");
에서 connection, message, close 는 WebSocket 에서 정의한 이벤트명 이에요.

저 이벤트명은 WebSocket에서 정해둔 거라서 우리가 어떻게 할 수 없어요.

그래서 json 으로 페킷 타입과 정보를 직렬화시켜서 보내요.
*/
//#endregion

// 클라이언트가 서버와 연결되었을 때 실행됩니다.
// 서버와 클라이언트의 연결이 이루어진 소켓을 socket 에 넣어 줘요. (socket 말고 다른 이름이어도 됩니다.)
// WinSocket 의 "SOCKET sClient = accept(sListening, ...);" 함수와 같은 기능을 해요. (좀 더 쉬울 뿐이지)
wsService.on("connection", socket => {
    console.log("클라이언트 접속");

    // 클라이언트의 연결이 끊겼을 때 실행됩니다.
    socket.on("close", () => {
        console.log("클라이언트 접속 종료");
    });

    // 클라이언트에게서 메세지가 도착할때마다 실행됩니다.
    socket.on("message", msg => {

        const data = parseBuffer(msg);
        const type = data.type;
        const payload = data.payload;

        // 타입 명 곳 정해서 갈아 낄 예정
        switch (type) {

            //#region 메뉴 타입

            case "": // 로그인 시
                break;
            
            case "": // 방 입장 시
                break;
            
            case "": // 게임 시작 시
                break;
            
            //#endregion

            //#region 준비 단계 타입
            
            case "": // 직업 선택 시
                break;
            
            case "": // 스텟 배분 시
                break;

            //#endregion

            //#region 인게임 타입
            
            case "": // 공격 시
                break;

            case "": // 사망 시
                break;

            case "": // 아이탬 사용 시
                break;

            case "": // 위치 이동 시
                break;

            case "": // 턴 종료 시
                break;

            case "": // 탈주 시
                break;

            //#endregion

            default:
                console.log(`클라이언트에게서 알 수 없는 타입을 전송받았습니다.\r\n타입: ${type}`);
                break;
        }
    });

});

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

function parseBuffer(msg) {
    try {
        // C#에서 const var, C/C++에서 cont auto(C#의 var 와 비슷한 기능을 함) 와 같은 의미입니다.
        const data = JSON.parse(msg);
        return data;
    } catch (e) {
        // 전달된 msg 가 JSON 형식이 아니라면 이 쪽으로 오게 됩니다.
        // try catch 문을 사용하지 않으면 잘못된 요청이 올 때 마다 서버가 터지게 됨

        console.log("클라이언트에서 잘못된 요청이 발생했어요.");
        console.log(e);
    }
}