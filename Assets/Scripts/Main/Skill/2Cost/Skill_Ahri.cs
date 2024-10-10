using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Ahri : BaseSkill
{
    [SerializeField] private Transform skillReturnTransform;
    [SerializeField] private Transform[] skillTransform;
    [SerializeField] private GameObject skillProjectile;

    /// <summary>
    /// ������ �� ���� �� �Լ� ����
    /// </summary>
    /// <param name="target"></param>
    public override void UseSkillTarget(GameObject target)
    {
        StartCoroutine(SkillAttackRoutine(target));
        StartCoroutine(ReturnSkillRoutine(target));
    }

    #region ���� �߻�

    private IEnumerator ReturnSkillRoutine(GameObject target)
    {
        GameObject projectile = Instantiate(skillProjectile, skillReturnTransform.position, Quaternion.identity);
        Vector3 targetPosition = target.transform.position;

        yield return new WaitForSeconds(1.0f);
        yield return StartCoroutine(MoveProjectileToTarget_Return(projectile, targetPosition));

        OnProjectileHit_Return(targetPosition);

        yield return StartCoroutine(MoveProjectileToTarget_Return(projectile, skillReturnTransform.position));

        OnProjectileReturn(skillReturnTransform.position);

        Destroy(projectile);
    }

    // �߻�ü�� ��ǥ �������� �̵���Ű�� �ڷ�ƾ
    private IEnumerator MoveProjectileToTarget_Return(GameObject projectile, Vector3 targetPosition)
    {
        float duration = 0.5f; // �̵� �ð�
        float elapsed = 0f;
        Vector3 startPosition = projectile.transform.position;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            projectile.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
            yield return null;
        }
    }

    private void OnProjectileHit_Return(Vector3 hitPosition)
    {
        Debug.Log("Hit at " + hitPosition);
    }

    private void OnProjectileReturn(Vector3 returnPosition)
    {
        Debug.Log("Projectile returned to " + returnPosition);

    }
    #endregion


    #region �ֺ� ������ ����� �߻�
    private IEnumerator SkillAttackRoutine(GameObject target)
    {
        Vector3 targetPosition = target.transform.position;

        foreach (Transform spawnPoint in skillTransform)
        {
            GameObject projectile = Instantiate(skillProjectile, spawnPoint.position, Quaternion.identity);

            yield return new WaitForSeconds(1.0f);

            StartCoroutine(MoveProjectileToTarget(projectile, targetPosition));
        }
    }

    private IEnumerator MoveProjectileToTarget(GameObject projectile, Vector3 targetPosition)
    {
        float duration = 0.5f; 
        float elapsed = 0f;
        Vector3 startPosition = projectile.transform.position;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            projectile.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
            yield return null;
        }

        OnProjectileHit(targetPosition);
        Destroy(projectile);
    }

    private void OnProjectileHit(Vector3 hitPosition)
    {
        Debug.Log("Hit at " + hitPosition);
    }

    #endregion
}
