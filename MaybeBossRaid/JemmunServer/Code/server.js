// npm i --s ws, npm i --s mysql2 를 실행해 줘야 해요.
import ws    from 'ws';
import parse from '../ParseBuffer.js'; // json 파싱 용
import pData from '../Data/Player/PlayerData.js'; // 플레이어 데이터 가지고 있는 클레스

//#region VO require

import LoginVO from '../VO/LoginVO.js';

//#endregion

//#region Handler require

// 유니티에서의 핸들러와 같은 역할을 합니다.
import LoginHandler   from '../Handler/LoginHandler.js';
import AttackHandler  from '../Handler/AttackHandler.js';
import InitPlayerData from '../Handler/InitPlayerData.js';

//#endregion

const port = 32000;

const wsService = new ws.Server({ port }, () => {
    console.log(`서버가 ${port} 에서 실행중입니다.`);
});



// 플레이어들의 스텟을 가지고 있는 list
// List<PlayerStat> playerStats = new List<PlayerStat>(); 와 같아요.
let playerStats = [];

// 플레이어들의 위치를 가지고 있는 list
let playerLocation = [];

// 플레이어들의 정보를 가지고 있는 list
// ID, HP, MP 등을 가지고 있어요.
// 클라이언트에서 직접 HP, MP를 조정하는 것이 아닌 서버에서 건드릴 예정
let playerData = [];

// 플레이어 고유 id.
// 사람이 접속할때마다 1 식 증가합니다.
let id = 1;


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
    
    // js는 클래스에 없는 변수를 접근하려고 하면 알아서 만들어 줍니다.
    // 엄청난 언어임.
    socket.socketId = id++;
    console.log(`클라이언트 접속, id: ${socket.socketId}`);
    new InitPlayerData(socket);



    // 클라이언트의 연결이 끊겼을 때 실행됩니다.
    socket.on("close", () => {
        console.log(`클라이언트 접속 종료, id: ${socket.socketId}`);
    });

    // 클라이언트에게서 메세지가 도착할때마다 실행됩니다.
    socket.on("message", msg => {

        const data = parse.parseBuffer(msg);
        if (data == -1) return;
        const { type, payload } = data; // 구조 분해 할당이에요. type = data.type, payload = data.payload 와 같은 의미입니다.
        
        switch (type) {

            //#region 메뉴 타입

            case "login": // 로그인 시
                // 이 것은 2학기 성과물로
                // 아레는 디버그 위한 코드들
                //new LoginHandler(socket, payload);

                break;
            
            case "entry": // 방 입장 시
                // 아마도 2학기 성과물로
                break;
            
            case "gamestart": // 게임 시작 시
                // 아마도 2학기 성과물로
                break;
            
            //#endregion

            //#region 준비 단계 타입
            
            case "jobselect": // 직업 선택 시
                break;
            
            case "stat": // 스텟 배분 시
                // 아마도 2학기 성과물로
                break;

            //#endregion

            //#region 인게임 타입
            
            case "attack": // 공격 시
                new AttackHandler(socket, payload);
                break;

            case "dead": // 사망 시
                break;

            case "item": // 아이탬 사용 시
                break;

            case "moved": // 위치 이동 시
                break;

            case "endturn": // 턴 종료 시
                break;

            case "exitted": // 탈주 시
                // 아마도 2학기 성과물로
                break;

            //#endregion

            default:
                console.log(`클라이언트에게서 알 수 없는 타입을 전송받았습니다.\r\n타입: ${type}`);
                break;
        }
    });

});