using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkill : MonoBehaviour
{
    public Coroutine skillCoroutine;

    /// <summary>
    /// �ɷ�ġ ���� ���� ��ų
    /// </summary>
    public virtual void UseSkill() { }


    /// <summary>
    /// �߻�ü �߻� ��ų
    /// </summary>
    /// <param name="target"></param>
    public virtual void UseSkillTarget(GameObject target) { }
}
