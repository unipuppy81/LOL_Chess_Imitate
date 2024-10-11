using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Akali : BaseSkill
{
    [SerializeField] private float dashRange = 3.0f; // ������ �ִ� �Ÿ�
    [SerializeField] private float dashSpeed = 10.0f; // ���� �ӵ�
    [SerializeField] private int enhancedAttackCount = 3; // ��ȭ�� ���� Ƚ��
    [SerializeField] private int enhancedDamage = 20; // ��ȭ�� ���� ������
    [SerializeField] private int normalDamage = 10; // �⺻ ���� ������

    private int remainingEnhancedAttacks = 0; // ���� ��ȭ ���� Ƚ��

    public override void UseSkill(GameObject thisGameObject)
    {
        GameObject target = FindFarthestTargetInRange();
        if (target != null)
        {
            StartCoroutine(DashToTarget(target));
        }
    }

    private IEnumerator DashToTarget(GameObject target)
    {
        Vector3 targetPosition = target.transform.position;
        Vector3 direction = (targetPosition - transform.position).normalized; 

        Vector3 targetBehindPosition = targetPosition + direction * 5.0f;

        // ��ǥ ��ġ�� ������ ���� ���� �Ÿ� �Ӱ谪
        float arrivalThreshold = 0.5f;

        remainingEnhancedAttacks = enhancedAttackCount;

         Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        while (Vector3.Distance(transform.position, targetBehindPosition) > arrivalThreshold)
        {
            RaycastHit hit;

            Debug.DrawRay(transform.position, direction * 10, Color.red);

            // ��ֹ� ����
            if (Physics.Raycast(transform.position, direction, out hit, 10))
            {
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider != null && hit.collider.gameObject == target)
                {
                    // ��ֹ� ���� �� ���� ȸ�� �̵�
                    Vector3 upwardPosition = transform.position + Vector3.up * 2.0f; 
                    transform.position = Vector3.MoveTowards(transform.position, upwardPosition, dashSpeed * Time.deltaTime);
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, targetBehindPosition, dashSpeed * Time.deltaTime);
            }

            yield return null;
        }

        if (rb != null)
        {
            rb.isKinematic = false;
        }


        LookAtTarget(target);

        StartCoroutine(ApplyEnhancedAttacks(target));
    }

    private void LookAtTarget(GameObject target)
    {
        Vector3 targetDirection = target.transform.position - transform.position;
        targetDirection.y = 0;

        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f); 
        }
    }

    private IEnumerator ApplyEnhancedAttacks(GameObject target)
    {
        for (int i = 0; i < remainingEnhancedAttacks; i++)
        {
            // �⺻ ���� ���� ����
            // ���⼭ �⺻ ������ �����ϴ� �޼ҵ带 ȣ���ϰų� �ʿ��� ������ �߰��ϼ���.
            Debug.Log("Performing enhanced attack " + (i + 1));


            ChampionBase targetHealth = target.GetComponent<ChampionBase>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(enhancedDamage); // �߰� ������ ����
            }

            // ���� ���� ���� �ð� ���� (��: 0.5��)
            yield return new WaitForSeconds(0.5f);
        }

        // ��� ��ȭ ������ ���� �� ���� ��ȭ ���� �� �ʱ�ȭ
        remainingEnhancedAttacks = 0;
    }

    // �Ÿ� ������ ���� �ָ� �ִ� ���� ã�� �޼���
    private GameObject FindFarthestTargetInRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, dashRange);
        GameObject farthestTarget = null;
        float maxDistance = 0;

        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    farthestTarget = collider.gameObject;
                }
            }
        }

        return farthestTarget;
    }

}
