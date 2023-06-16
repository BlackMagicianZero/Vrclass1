using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class Drone : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
    Transform tower;
    public float ATTACK_TIME = 2;
    float attackTime = 0;
    public int MAX_HP = 3;
    [System.NonSerialized]
    public int hp = 0;

    public float ATTACK_DISTANCE = 0.1f;

    public delegate void DamageTowerDelegate();
    public event DamageTowerDelegate OnDamageTower;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        tower = GameObject.Find("Tower").transform;
        agent.destination = tower.position;

        hp = MAX_HP;
        attackTime = ATTACK_TIME;
    }

    void Update()
    {
        if (agent.remainingDistance <= ATTACK_DISTANCE)
        {
            attackTime += Time.deltaTime;
            if (attackTime > ATTACK_TIME)
            {
                attackTime = 0f;
                if (OnDamageTower != null)
                {
                    OnDamageTower(); // 타워에 데미지를 입히기 위해 이벤트 호출
                    Destroy(gameObject); // 드론을 파괴
                }
            }
        }
    }
}
