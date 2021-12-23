# VO List

서버에서 사용되는 VO들의 목록입니다.

## Server

서버가 클라이언트에게 보내는 VO들입니다.

### RoomDataVO

방의 정보를 보내는 VO입니다.

```js
{
    type: 'RoomData',
    data: {
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

```js
{
    type: 'JoinRoom',
    data: {
        id: 방ID: Number
    }
}
```

### RoomUserJoinVO

유저가 방에 입장할 때 보내는 VO입니다.

```js
{
    type: 'RoomUserJoin',
    data: {
        user: 유저ID: Number
    }
}
```

### RoomUserLeaveVO

유저가 방에서 나갈 때 보내는 VO입니다.

```js
{
    type: 'RoomUserLeave',
    data: {
        user: 유저ID: Number
    }
}
```

### RoomUserReadyVO

유저의 레디 상태가 변경될 때 보내는 VO입니다.

```js
{
    type: 'RoomUserReady',
    data: {
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
    data: {
        count: 투표인원:Number
    }
}
```

### RoomUserVoteVO

유저가 투표할 때 보내는 VO입니다.

```js
{
    type: 'RoomUserVote',
    data: {
        user: 유저ID: Number
    }
}
```

### RoomGameStartVO

게임이 시작될 때 보내는 VO입니다.

```js
{
    type: 'RoomGameStart',
    data: {
        game: 게임ID: Number
    }
}
```

## Client

클라이언트가 서버에게 보내는 VO입니다.

### GetRoomDataVO

방에 대한 정보를 요청할 때 보내는 VO입니다.

```js
{
    type: 'GetRoomData',
    data: {
        id: 방ID: Number,
    }
}
```

### LeaveRoomVO

방에서 나가기를 요청할 때 보내는 VO입니다.

```js
{
    type: 'LeaveRoom',
    data: {
        // 비어도 됨.
    }
}
```

### MatchMakingVO

매칭을 할 때 보내는 VO입니다.

```js
{
    type: 'MatchMaking',
    data: {
        // 비어도 됨.
    }
}
```

### JoinRoomVO

```js
{
    type: 'JoinRoom',
    data: {
        id: 방ID: Number
    }
}
```

### ReadyVO

준비 상태를 변경할 때 보내는 VO입니다.

```js
{
    type: 'Ready',
    data: {
        state: Boolean
    }
}
```

### VoteVO

투표를 할 때 보내는 VO입니다.

```js
{
    type: 'Vote',
    data: {
        item: Number
    }
}
```
