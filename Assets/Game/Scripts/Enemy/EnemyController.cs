using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum EnemyState
{
    PATROL,
    CHASE,
    ATTACK
}
public class EnemyController : MonoBehaviour
{
    private EnemyAnimator enemy_Anim;

    private NavMeshAgent navAgent;

    public EnemyState enemy_State { get; private set; }

    private Transform target;

    public float walkSpeed=0.5f;
    public float run_Speed = 4f;

    public float chase_Distance = 7;
    private float current_Chase_Distance;

    public float attack_Distance = 1.8f;

    public float patrol_Radius_Min = 20f, patrol_Radius_Max = 60f;
    public float patrol_For_This_Time = 10;

    public float wait_Before_Attack = 2f;

    private float patrol_Timer;
    private float attack_Timer;

    private EnemyAudio enemy_Audio;
    private AttackPoint attackPoint;
    void Awake()
    {
        enemy_Anim = GetComponent<EnemyAnimator>();
        navAgent = GetComponent<NavMeshAgent>();

        target = GameObject.FindWithTag("Player").transform;

        enemy_Audio = GetComponentInChildren<EnemyAudio>();

        attackPoint = GetComponentInChildren<AttackPoint>();
    }

    // Use this for initialization
    void Start()
    {

        enemy_State = EnemyState.PATROL;

        patrol_Timer = patrol_For_This_Time;

        // when the enemy first gets to the player
        // attack right away
        attack_Timer = wait_Before_Attack;

        // memorize the value of chase distance
        // so that we can put it back
        current_Chase_Distance = chase_Distance;



    }
    // Update is called once per frame
    void Update()
    {
        if (enemy_State == EnemyState.PATROL)
        {
            Patrol();
        }

        if (enemy_State == EnemyState.CHASE)
        {
            Chase();
        }

        if (enemy_State == EnemyState.ATTACK)
        {
            Attack();
        }
    }
    void Patrol()
    {
        // tell nav agent that he can move
        navAgent.isStopped = false;
        navAgent.speed = walkSpeed;

        // add to the patrol timer
        patrol_Timer += Time.deltaTime;

        if (patrol_Timer > patrol_For_This_Time)
        {

            SetNewRandomDestination();

            patrol_Timer = 0f;

        }

        if (navAgent.velocity.sqrMagnitude > 0)
        {

            enemy_Anim.Walk(true);

        }
        else
        {

            enemy_Anim.Walk(false);

        }

        // test the distance between the player and the enemy
        if (Vector3.Distance(transform.position, target.position) <= chase_Distance)
        {

            enemy_Anim.Walk(false);

            enemy_State = EnemyState.CHASE;

            // play spotted audio
            enemy_Audio.Play_ScreamSound();

        }
    }
    void Chase()
    {
        // enable the agent to move again
        navAgent.isStopped = false;
        navAgent.speed = run_Speed;

        // set the player's position as the destination
        // because we are chasing(running towards) the player
        navAgent.SetDestination(target.position);

        if (navAgent.velocity.sqrMagnitude > 0)
        {

            enemy_Anim.Run(true);

        }
        else
        {

            enemy_Anim.Run(false);

        }

        // if the distance between enemy and player is less than attack distance
        if (Vector3.Distance(transform.position, target.position) <= attack_Distance)
        {

            // stop the animations
            enemy_Anim.Run(false);
            enemy_Anim.Walk(false);
            enemy_State = EnemyState.ATTACK;

            // reset the chase distance to previous
            if (chase_Distance != current_Chase_Distance)
            {
                chase_Distance = current_Chase_Distance;
            }

        }
        else if (Vector3.Distance(transform.position, target.position) > chase_Distance)
        {
            // player run away from enemy

            // stop running
            enemy_Anim.Run(false);

            enemy_State = EnemyState.PATROL;

            // reset the patrol timer so that the function
            // can calculate the new patrol destination right away
            patrol_Timer = patrol_For_This_Time;

            //reset the chase distance to previous
            if (chase_Distance != current_Chase_Distance)
            {
                chase_Distance = current_Chase_Distance;
            }


        }
    }

    void Attack()
    {

        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;

        attack_Timer += Time.deltaTime;

        if (attack_Timer > wait_Before_Attack)
        {

            enemy_Anim.Attack();

            attack_Timer = 0f;

            // play attack sound
            enemy_Audio.Play_AttackSound();

        }

        if (Vector3.Distance(transform.position, target.position) >
           attack_Distance + 2)
        {

            enemy_State = EnemyState.CHASE;

        }


    }
    void SetNewRandomDestination()
    {

        float rand_Radius = Random.Range(patrol_Radius_Min, patrol_Radius_Max);

        Vector3 randDir = Random.insideUnitSphere * rand_Radius;
        randDir += transform.position;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDir, out navHit, rand_Radius, -1);

        navAgent.SetDestination(navHit.position);

    }
    public void AttackActivate()
    {
        attackPoint.gameObject.SetActive(true);
    }
}
