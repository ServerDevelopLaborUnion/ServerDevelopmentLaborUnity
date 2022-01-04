using UnityEngine;

public class SkillHandler : MonoBehaviour
{
    private void Start()
    {
        BufferHandler.Instance.AddHandler("skill", (data) => {
            SkillVO vo = JsonUtility.FromJson<SkillVO>(data);
            // vo.skill
            // vo.id 에 해당하는 GameObject 를 찾아와서 vo.skill 을 실행해야 함
            UserManager.Instance.GetUserObject(vo.id);

        });
    }
}