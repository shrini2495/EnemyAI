using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float turnspeed = 5f;


    NavMeshAgent navmeshAgent;
    WeaponScript weapon;
    public float distancetoTarget = Mathf.Infinity;
    public bool isProvoked = false;
    public bool isMoving = true;
    void Start()
    {
        navmeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        NavMeshMove();
        
    }
    public void NavMeshMove()
    {
        distancetoTarget = Vector3.Distance(target.position, transform.position);
        
        //if (weapon.hit.transform.name == "Enemy")
        //{
        //    isProvoked = true;
        //}
       if (isProvoked)
        {
            Engagetarget();
        }
       else if (distancetoTarget <= chaseRange)
        {
            isProvoked = true;
            
        }
    }

    public void Engagetarget()

    {
        FaceTarget();
        if (distancetoTarget >= navmeshAgent.stoppingDistance) 
        {
            ChaseTarget();
        } 
        if(distancetoTarget <= navmeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }
    public void ChaseTarget()
    {
        GetComponent<Animator>().SetTrigger("charge");
        GetComponent<Animator>().SetBool("attack", false);
        GetComponent<Animator>().SetBool("Run",true);
        navmeshAgent.SetDestination(target.position);
    }

    private void  AttackTarget()
    {
        GetComponent<Animator>().SetBool("attack", true);
        Debug.Log(name + "Attacking the Target" + target.name);
    }
    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookatrotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookatrotation, Time.deltaTime * turnspeed);


    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
