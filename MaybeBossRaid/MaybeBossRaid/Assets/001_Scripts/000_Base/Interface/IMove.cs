using UnityEngine;

// �̰� �ʿ������� �𸣰���
/*
���� ĳ�� ������ ���� �����ϰ�
�ٸ� ��� ĳ�� ��Ŷ���� Vector2 �� �޾Ƽ� �ֱ⸸ �ϸ� �ǰԲ� �ϰ� ������ ������

- �쿱
*/

interface IMove
{
    void Move(Vector3 pos);
    void Rotate(Vector3 rot);
}