using System;

// ���ѽ� �ڵ带 �ôٸ� ������ �� �ϴ��� �� ���� ������
// ���� �������� �׸� �����Ȱ� �ƴ϶� ������ ����ΰٽ����.

// VO Ŭ������ �����͵��� Json���� ����� ���� ����� �� Ŭ��������.
// Json ���� ����� ������ �Ƹ� �ƽǰŶ�� �����ϰ��ս�

[Serializable]
public class HealthVO
{
    public int hp;
    public int mp;


    public HealthVO(int hp, int mp)
    {
        this.hp = hp;
        this.mp = mp;
    }
}
