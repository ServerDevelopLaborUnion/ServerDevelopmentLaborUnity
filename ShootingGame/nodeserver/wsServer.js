const WebSocket = require('ws');
const Vector3 = require('./Vector3');
const Vector2 = require('./Vector2');
const port = 32000;

let socketIdx = 0;
let connectedSocket = {};
let userList = {};

let shootData = false;
let sendShootD;

const wsService = new WebSocket.Server({ port }, () => {
    console.log("웹소켓 서버 스타트");

    setInterval(() => {
        let keys = Object.keys(userList); //userList의 키를 배열로 뽑아냄
        let dataList = [];
        for (let i = 0; i < keys.length; i++) {
            dataList.push(userList[keys[i]]); // dataList에 userList안의 키안의 값을 푸쉬함
        }
        //모든 소켓에 데이터 갱신해줌.
        wsService.clients.forEach(s => {
            s.send(JSON.stringify({ type: "Refresh", payload: JSON.stringify({ dataList }) }));
        });
    }, 1000 / 60);
});
//데이터 전송간 명령어와 페이로드 구분해주는 함수
const getPayload = str => {
    let idx = str.indexOf(":");
    let op = str.substr(0, idx);
    let payload = str.substr(idx + 1);
    return { op, payload };
};

const State = {
    IN_LOGIN: 0,
    IN_GAME: 1,
}

//wsService는 내부에 clients라는 속성을 가지고 있고 해당 
//속성을 이용해서 브로드캐스트등을 구현할 수 있다.

wsService.on("connection", socket => {
    socket.send(JSON.stringify({ payload: "Welcome", type: "Chat" }));

    socket.state = State.IN_LOGIN;
    socket.id = socketIdx;
    connectedSocket[socketIdx] = socket;
    socketIdx++;

    console.log(`${socket.id} 접속`);

    socket.on("close", () => {
        console.log(`${socket.id} 접속 해제`);
        delete connectedSocket[socket.id];
        if(socket.state === State.IN_GAME){
            delete userList[socket.id];
            wsService.clients.forEach(s=>{
                s.send(JSON.stringify({type:"Disconnect", payload:socket.id }))
            })
        }
    });

    socket.on("message", msg => {
        const data = JSON.parse(msg);

        if (data.type === "Login") {
            LoginHandler(data, socket);
        }
        if (data.type === "Transform") {
            TransformHandler(data, socket);
        }
        if(data.type === "OnDamage"){
            OnDamageHandler(data, socket);
        }
        if(data.type === "Shoot"){
            ShootHandler(data, socket);
        }
    });
});

function TransformHandler(data, socket) {
    let payload = JSON.parse(data.payload);
    let user = userList[payload.socketId + ""];

    user.point = payload.point;
    user.rotation = payload.rotation;
}

function LoginHandler(data, socket) {
    const { name } = data;
    const maxMap = 154;

    let x = Math.floor(Math.random() * maxMap - (maxMap/2));
    let y = Math.floor(Math.random() * maxMap - (maxMap/2));
    let point = new Vector2(x, y);

    socket.state = State.IN_GAME;

    let sendData = JSON.stringify({
        type: "Login",
        payload: JSON.stringify({ point, rotation: Vector3.zero, socketId: socket.id })
    });

    userList[socket.id] = {
        point,
        rotation: Vector3.zero,
        socketId: socket.id,
    };
    socket.send(sendData);
}

function OnDamageHandler(data, socket) {
    let payload = JSON.parse(data.payload);

    let sendData = JSON.stringify({
        type: "OnDamage",
        payload: JSON.stringify({socketId:payload.socketId, damage:payload.damage})
    });

    wsService.clients.forEach(s => {
        s.send(sendData);
    });
}

function ShootHandler(data, socket) {
    let payload = JSON.parse(data.payload);

    let sendData = JSON.stringify({
        type: "Shoot",
        payload: JSON.stringify({rotation:payload.rotation, socketId:socket.id})
    });

    //모든 소켓에 데이터 갱신해줌.
    wsService.clients.forEach(s => {
        s.send(sendData);
    });
}

wsService.on('listening', () => {
    console.log(`server listen on ${port}`);
});