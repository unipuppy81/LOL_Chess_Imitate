using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Akali : BaseSkill
{
    [SerializeField] private float dashRange = 3.0f; // 돌진할 최대 거리
    [SerializeField] private float dashSpeed = 10.0f; // 돌진 속도
    [SerializeField] private int enhancedAttackCount = 3; // 강화된 공격 횟수
    [SerializeField] private int enhancedDamage = 20; // 강화된 공격 데미지
    [SerializeField] private int normalDamage = 10; // 기본 공격 데미지

    private int remainingEnhancedAttacks = 0; // 남은 강화 공격 횟수

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

        // 목표 위치에 접근할 때를 위한 거리 임계값
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

            // 장애물 감지
            if (Physics.Raycast(transform.position, direction, out hit, 10))
            {
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider != null && hit.collider.gameObject == target)
                {
                    // 장애물 감지 시 위로 회피 이동
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
            // 기본 공격 수행 로직
            // 여기서 기본 공격을 수행하는 메소드를 호출하거나 필요한 로직을 추가하세요.
            Debug.Log("Performing enhanced attack " + (i + 1));


            ChampionBase targetHealth = target.GetComponent<ChampionBase>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(enhancedDamage); // 추가 데미지 적용
            }

            // 다음 공격 간의 시간 지연 (예: 0.5초)
            yield return new WaitForSeconds(0.5f);
        }

        // 모든 강화 공격이 끝난 후 남은 강화 공격 수 초기화
        remainingEnhancedAttacks = 0;
    }

    // 거리 내에서 가장 멀리 있는 적을 찾는 메서드
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
