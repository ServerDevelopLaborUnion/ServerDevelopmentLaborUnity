using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleCharacter : MonoBehaviour, IHit, IMove, ISkill
{
    private ExampleStatus status;

    private void Awake()
    {
        status = GetComponent<ExampleStatus>();

        #region null 체크
        #if UNITY_EDITOR
        if (status == null)
        {
            Debug.LogError("cannot find ExampleStatus. Quitting.");
            UnityEditor.EditorApplication.isPlaying = false;
        }
        #endif
        #endregion
    }

    void Update()
    {
        
    }

    #region 스킬

    public void Skill_1() { }
    public void Skill_2() { }
    public void Skill_3() { }
    public void Skill_4() { }

    #endregion

    #region Movement
    
    public void Move(Vector3 pos)
    {
        transform.position += pos;
    }

    public void Rotate(Vector3 rot)
    {
        // 잠만 이거 필요한 함수인가
        //transform.rotation = Quaternion.Euler(rot);
    }

    #endregion

    #region Hit

    public void OnDamage(int damage)
    {
        status.hp -= damage;
    }

    public void OnSkillHit(Classes classEnum, SkillList skillEnum)
    {
        // TODO : 아무것도 없다!
    }

    #endregion
}