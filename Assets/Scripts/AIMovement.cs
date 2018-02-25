using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour {
    // Reads in 2 empty objects as target locations
    public Transform target1;
    public Transform target2;
    NavMeshAgent agent;
    public GameObject player;

    private bool tar = true; // Controls which target is the destination

    public int DrillState;
    public int SentryState;

    public const int PATROL = 0;
    public const int FOLLOW = 1;
    public const int ATTACK = 2;
    public const int DISABLED = 3;

    private GameObject[] attackers;
    private bool summoned = false;
    private bool dead = false;

    private float timer = 0;

    private GameObject closest = null;

    // Use this for initialization
    void Start () {
        player = Camera.main.transform.parent.gameObject;
        agent = GetComponent<NavMeshAgent>();
        DrillState = PATROL;
        SentryState = PATROL;
    }

    // Update is called once per frame
    void Update() {

        if (gameObject.CompareTag("AttackEnemy"))
        {
            switch (DrillState)
            {
                case PATROL:
                    // Set the Nav Agent's destination based on the tar variable
                    if (tar)
                        agent.SetDestination(target1.position);
                    else
                        agent.SetDestination(target2.position);

                    // Switch targets once the target is reached
                    if (Vector3.Distance(agent.transform.position, agent.destination) < agent.stoppingDistance)
                    {
                        tar = !tar;
                    }
                    break;
                case FOLLOW:
                    agent.SetDestination(player.transform.position);
                    if (Vector3.Distance(agent.transform.position, agent.destination) < agent.stoppingDistance)
                    {
                        DrillState = ATTACK;
                        agent.isStopped = true;
                    }
                    break;
			case ATTACK:
				if (!dead)
					player.GetComponent<Heartbeat> ().OnDeath ();
				Time.timeScale = 0.0f;
				dead = true;
				GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().GameOver ();
                    break;
                case DISABLED:
                    agent.isStopped = true;
                    Timer();
                    break;
                default:
                    break;
            }
        }
        if(gameObject.CompareTag("SentryEnemy"))
        {
            switch (SentryState)
            {
                case PATROL:
                    // Set the Nav Agent's destination based on the tar variable
                    if (tar)
                        agent.SetDestination(target1.position);
                    else
                        agent.SetDestination(target2.position);

                    // Switch targets once the target is reached
                    if (Vector3.Distance(agent.transform.position, agent.destination) < agent.stoppingDistance)
                    {
                        tar = !tar;
                    }
                    break;
                case FOLLOW:
                    agent.SetDestination(player.transform.position);
                    if (Vector3.Distance(agent.transform.position, agent.destination) < agent.stoppingDistance)
                    {
                        agent.isStopped = true;
                    }
                    else
                    {
                        agent.isStopped = false;
                    }
                    break;
                case DISABLED:
                    agent.isStopped = true;
                    Timer();
                    break;
                default:
                    break;
            }
        }
	}

    public void OnDetect()
    {
       
        if (gameObject.CompareTag("AttackEnemy") && DrillState != DISABLED)
        {
            player.GetComponent<Heartbeat>().OnDetect();
            agent.speed = 3.5f;
            DrillState = FOLLOW;
        }
        if (gameObject.CompareTag("SentryEnemy") && SentryState != DISABLED)
        {
            player.GetComponent<Heartbeat>().OnDetect();
            SentryState = FOLLOW;
            agent.speed = 5;
            if(!summoned)
            {
                attackers = GameObject.FindGameObjectsWithTag("AttackEnemy");
                float minDistance = Mathf.Infinity;

                foreach(GameObject go in attackers)
                {
                    float dist = Vector3.Distance(go.transform.position, player.transform.position);
                    if( dist < minDistance)
                    {
                        minDistance = dist;
                        closest = go;
                    }
                }
                if (closest != null)
                {
                    closest.gameObject.GetComponent<AIMovement>().OnDetect();
                    summoned = true;
                }
            }
        }
    }

    public void Patrol()
    {
        player.GetComponent<Heartbeat>().OnRelax();
        agent.isStopped = false;
        if (gameObject.CompareTag("AttackEnemy"))
        {
            agent.speed = 1.75f;
            DrillState = PATROL;
        }
        if (gameObject.CompareTag("SentryEnemy"))
        {
            SentryState = PATROL;
            agent.speed = 2.5f;
            if (summoned)
            {
                closest.gameObject.GetComponent<AIMovement>().Patrol();
                summoned = false;
            }
        }
    }

    public void OnEMP()
    {
        DrillState = DISABLED;
        SentryState = DISABLED;
    }

    private void Timer()
    {
        timer += Time.deltaTime;
        if (timer >= 2)
        {
            agent.isStopped = false;
            DrillState = PATROL;
            SentryState = PATROL;
            timer = 0;
        }
    }
}
