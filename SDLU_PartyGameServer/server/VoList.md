# VO List

## Server

### JoinRoomVO

```js
{
    type: 'RoomUserJoin',
    data: {
        user: UserID: Number
    }
}
```

### RoomLeaveVO

```js
{
    type: 'RoomUserLeave',
    data: {
        user: UserID: Number
    }
}
```

### RoomUserReadyVO

```js
{
    type: 'RoomUserReady',
    data: {
        user: UserID: Number,
        ready: Boolean
    }
}
```

### RoomStartVoteVO

```js
{
    type: 'RoomStartVote',
    data: {
        count: Number
    }
}
```

### RoomUserVoteVO

```js
{
    type: 'RoomUserVote',
    data: {
        user: UserID: Number
    }
}
```

### RoomGameStartVO

```js
{
    type: 'RoomGameStart',
    data: {
        game: GameID: Number
    }
}
```

## Client

### RoomDataVO

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
