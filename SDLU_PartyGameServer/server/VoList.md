# VO List

서버에서 사용되는 VO들의 목록입니다.

## Server

서버가 클라이언트에게 보내는 VO들입니다.

### UserNameVO

```js
{
    type: "UserName",
    payload: {
        name: 닉네임: string,
        id: 아이디: string
    }
}
```

### RoomDataVO

방의 정보를 보내는 VO입니다.

```js
{
    type: 'RoomData',
    payload: {
        id: RoomID: Number,
        name: RoomName: String,
        userList: [
            {
                id: UserID: Number,
                name: UserName: String
            }
        ]
    }
}
```

### JoinRoomVO

들어갈 수 있는 방의 정보를 보내는 VO입니다.

```js
{
    type: 'JoinRoom',
    payload: {
        id: 방ID: Number
    }
}
```

### RoomUserJoinVO

유저가 방에 입장할 때 보내는 VO입니다.

```js
{
    type: 'RoomUserJoin',
    payload: {
        user: 유저ID: Number
    }
}
```

### RoomUserLeaveVO

유저가 방에서 나갈 때 보내는 VO입니다.

```js
{
    type: 'RoomUserLeave',
    payload: {
        user: 유저ID: Number
    }
}
```

### RoomUserReadyVO

유저의 레디 상태가 변경될 때 보내는 VO입니다.

```js
{
    type: 'RoomUserReady',
    payload: {
        user: 유저ID: Number,
        ready: Boolean
    }
}
```

### RoomStartVoteVO

방의 게임 종목 투표가 시작될 때 보내는 VO입니다.

```js
{
    type: 'RoomStartVote',
    payload: {
        count: 투표인원:Number
    }
}
```

### RoomUserVoteVO

유저가 투표할 때 보내는 VO입니다.

```js
{
    type: 'RoomUserVote',
    payload: {
        user: 유저ID: Number
        voteList: [
            "종목": 투표수: Number
        ]
    }
}
```

### RoomGameStartVO

게임이 시작될 때 보내는 VO입니다.

```js
{
    type: 'RoomGameStart',
    payload: {
        game: 게임ID: Number
    }
}
```

### RoomUserChatVO

방의 채팅이 보내질 때 보내는 VO입니다.

```js
{
    type: 'RoomUserChat',
    payload: {
        message: 메시지: String
    }
}

## Client

클라이언트가 서버에게 보내는 VO입니다.

### GetRoomDataVO

방에 대한 정보를 요청할 때 보내는 VO입니다.

```js
{
    type: 'GetRoomData',
    payload: {
        id: 방ID: Number,
    }
}
```

### LeaveRoomVO

방에서 나가기를 요청할 때 보내는 VO입니다.

```js
{
    type: 'LeaveRoom',
    payload: {
        // 비어도 됨.
    }
}
```

### MatchMakingVO

매칭을 할 때 보내는 VO입니다.

```js
{
    type: 'MatchMaking',
    payload: {
        // 비어도 됨.
    }
}
```

### JoinRoomVO

방에 입장을 요청할 때 보내는 VO입니다.

```js
{
    type: 'JoinRoom',
    payload: {
        id: 방ID: Number
    }
}
```

### ReadyVO

준비 상태를 변경할 때 보내는 VO입니다.

```js
{
    type: 'Ready',
    payload: {
        state: Boolean
    }
}
```

### VoteVO

투표를 할 때 보내는 VO입니다.

```js
{
    type: 'Vote',
    payload: {
        item: Number
    }
}
```

### ChatVO

채팅을 보낼 때 보내는 VO입니다.

```js
{
    type: 'Chat',
    payloadL {
        message: 메시지: String
    }
}
```

## 게임에 사용되는 VO

### GameVO

게임과 관련된 VO는 모두 GameVO 안에 GameID와 함께 전송됩니다.

> Client와 Server 모두가 사용하는 VO 입니다

```js
{
    type: 'Game',
    payload: {
        id: 게임ID: Number,
        payload: 게임에 사용되는 VO // 직접 구현하셔야 합니다.
    }
}
```

### 게임 Handler와 서버 제작 관련 숙지사항

`./game/{GameName}` 폴더를 생성하시고 `./game/{GameName}/{GameName}.js 파일을 만들어 게임 서버를 제작하시면 됩니다.

`./game/{GameName}/handler` 폴더에 게임의 `handler`를 제작하시면 됩니다.

기본적으로 `GameBase`를 상속받아 게임을 제작해야 합니다. (LOS를 참고해 주세요)

`this.room.broadcast`를 이용하여 게임 인원 모두에게 전송이 가능합니다.

`socket.user.name`을 이용하여 유저네임을 받아올 수 있습니다.

`socket.user.id`를 이용하여 유저의 id를 받아올 수 있습니다.

### EX) 게임 VO 구조

```js
{
    type: 'Game',
    payload: {
        id: 게임ID: Number,
        payload: {
            type: 'Move',
            payload: {
                user: 유저ID: Number,
                x: 좌표X: Number,
                y: 좌표Y: Number
            }
        }
    }
}
