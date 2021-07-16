# 타입과 보내야 하는 데이터 정리

### 서버에 보낼 때의 타입 정리

로그인 시: login<br>
방 입장 시: entry<br>
게임 시작 시: gamestart<br>
* * * 
직업 선택 시: jobselect<br>
스텟 배분 시: stat<br>
* * *
공격 시: attack<br>
사망 시: dead<br>
아이탬 사용 시: item<br>
위치 이동 시: moved<br>
턴 종료 시: endturn<br>
탈주 시: exitted<br>

* * *

### 서버에 보낼 때의 데이터 정리

로그인 시: { "id": (string), "pw": (string) }<br>
방 입장 시: { "roomnum": (int) }<br>
게임 시작 시 (방장만 전송 가능): (아무 데이터도 안 보내도 됩니다.)<br>
* * *
직업 선택 시: { "selectedjob": (Enum 을 int 로 변환) }<br>
스텟 배분 시: { "curstat": (stat class 를 Json 으로 변환) }<br>
* * *
공격 시: (아무 데이터도 안 보내도 됩니다.)<br>
사망 시: (아무 데이터도 안 보내도 됩니다.)<br>
아이탬 사용 시: { "itemid": (Enum 을 int 로 변환) }<br>
위치 이동 시" { "movedto": (int) }<br>
턴 종료 시: (아무 데이터도 안 보내도 됩니다.)<br>
탈주 시: {"isHost": (bool) }<br>

* * *

### 데이터 의미 정리

id: 아이디<br>
pw: 비밀번호<br>
roomnum: 방 번호<br>
* * *
selectedjob: 선택된 직업 Enum<br>
curstat: 현재 스텟 클레스 (Json으로 변환 후 넣어줘요.)<br>
* * *
itemid: 사용한 아이탬의 Enum<br>
movedto: 이동한 위치<br>
isHost: 방장 여부<br>

* * *

### 왜 아무 데이터도 안 보내도 되는지 정리

게임 시작 시: 서버에서 방에 접속되어있는 모든 클라이언트에게 게임 시작했다고 보내기만 하면 되서 그렜어요.<br>
공격 시: 위에 스텟 배분 에서 스텟을 전부 보냈어요. 그걸 가지고 서버에서 데미지 처리 할 예정입니다.<br>
사망 시: 게임 시작 시와 같은 이유에요.<br>
턴 종료 시: 게임 시작 시와 같은 이유에요.<br>

* * *

만약 애매하거나 궁금한 것이 있다면 언제든지 물어보세요.
