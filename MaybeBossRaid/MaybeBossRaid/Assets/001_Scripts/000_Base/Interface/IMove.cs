using UnityEngine;

// 이게 필요한지는 모르겠음
/*
본인 캐는 본인이 직접 조종하고
다른 사람 캐는 페킷에서 Vector2 값 받아서 넣기만 하면 되게끔 하고 싶은데 흠흠흠

- 우엽
*/

interface IMove
{
    void Move(Vector3 pos);
    void Rotate(Vector3 rot);
}