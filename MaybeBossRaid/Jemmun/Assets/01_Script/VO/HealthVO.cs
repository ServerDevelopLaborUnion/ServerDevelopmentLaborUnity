using System;

// 선한쌤 코드를 봤다면 대충은 뭐 하는지 알 수도 있지만
// 나도 이해한지 그리 오래된게 아니라 설명을 적어두겟스빈다.

// VO 클래스는 데이터들을 Json으로 만들기 위해 만들어 진 클레스에요.
// Json 으로 만드는 이유는 아마 아실거라고 생각하고잇슴

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
