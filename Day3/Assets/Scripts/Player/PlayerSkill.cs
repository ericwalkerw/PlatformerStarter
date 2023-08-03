using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public KeyCode[] keys;
    public string skillBuff;

    Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }
    public void SkillBuff()
    {
        for (int i = 0; i < keys.Length; i++)
        {
            if (Input.GetKey(keys[i]))
            {
                anim.SetInteger(skillBuff[i], i);
                anim.SetTrigger(skillBuff);
            }
        }
    }
}
