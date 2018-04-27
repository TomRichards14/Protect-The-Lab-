using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class ConstantTags
{
    public const string PlayerTag = "Player";
    public const string CoreTag = "Core";
}

public class EnemyActionsManagerScript : MonoBehaviour {

    public int MaximumHealth = 100;
    public int CurrentHealth;
    public const int AttackDamage = 5;

    public const float MeleeRange = 1.0f;
    public const float CollectRangee = 1.0f;

    public bool IsAlive;

    public Vector3 SpawnPosition;

    private NavMeshAgent EnemyAgent;

    public string AIState;
    //public GameObject PlayerGameObject;
    public AIStateMachine<EnemyActionsManagerScript> stateMachine { get; set; }

	// Use this for initialization
	void Start ()
    {
        EnemyAgent = GetComponent<NavMeshAgent>();
        stateMachine = new AIStateMachine<EnemyActionsManagerScript>(this);
        stateMachine.ChangeState(ChasePlayer.Instance);
        SpawnPosition = transform.position;
        CurrentHealth = MaximumHealth;
        IsAlive = true;
	}
	void Update ()
    {
        stateMachine.Update();
    }

	public void MoveTowardsPlayer()
    {
        EnemyAgent.destination = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    public void MoveTowardsCore()
    {
        EnemyAgent.destination = GameObject.FindGameObjectWithTag("Core").transform.position;
    }

    public void AttackPlayer()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManagerScript>().CurrentHealth -= AttackDamage;
    }

    public void CollectCorePiece(GameObject CorePiece)
    {

    }

    public void FleeWithCorePiece()
    {
        EnemyAgent.destination = SpawnPosition;
    }

    public void TakeDamage(int DamageTaken)
    {
        CurrentHealth -= DamageTaken;
        if (CurrentHealth <= 0)
        {
            EnemyDeath();
        }
    }

    public void EnemyDeath()
    {
        IsAlive = false;
    }
}

public class AIStateMachine<Enemy>
{
    #region State Machine Initialisation
    public State<Enemy> currentState { get; set; }
    public Enemy EnemyAI;

    public AIStateMachine(Enemy _EnemyAI)
    {
        EnemyAI = _EnemyAI;
        currentState = null;
    }

    public void ChangeState(State<Enemy> NewState)
    {
        if (currentState != null)
        {
            currentState.ExitState(EnemyAI);
        }

        currentState = NewState;

        currentState.EnterState(EnemyAI);
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.ExecuteState(EnemyAI);
        }
    }
    #endregion
}

public abstract class State<Enemy>
{
    #region State class function set up
    public abstract void EnterState(Enemy Enemy);
    public abstract void ExecuteState(Enemy Enemy);
    public abstract void ExitState(Enemy Enemy);
    #endregion
}

public class ChasePlayer : State<EnemyActionsManagerScript>
{
    #region ChasePlayer state instance set up
    static readonly ChasePlayer instance = new ChasePlayer();
    public static ChasePlayer Instance
    {
        get
        {
            return instance;
        }
    }
    static ChasePlayer() { }
    private ChasePlayer() { }
    #endregion


    #region ChasePlayer State Machine
    public override void EnterState(EnemyActionsManagerScript Enemy)
    {
        Enemy.AIState = "ChasePlayer";
        Debug.Log("Entering ChasePlayer State" + Enemy.gameObject);
    }

    public override void ExecuteState(EnemyActionsManagerScript Enemy)
    {
        if (Enemy.tag == "Normal")
        {
            Enemy.MoveTowardsPlayer();
            if (Vector3.Distance(Enemy.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 1.0f)
            {
                Enemy.stateMachine.ChangeState(AttackPlayer.Instance);
            }
        }
        else
        {
            Enemy.MoveTowardsCore();
        }
    }

    public override void ExitState(EnemyActionsManagerScript Enemy)
    {
        Debug.Log("Leaving ChasePlayer State" + Enemy.gameObject);
    }
    #endregion
}

public class AttackPlayer : State<EnemyActionsManagerScript>
{
    #region AttackPlayer state instance set up
    static readonly AttackPlayer instance = new AttackPlayer();
    public static AttackPlayer Instance
    {
        get
        {
            return instance;
        }
    }
    static AttackPlayer() { }
    private AttackPlayer() { }
    #endregion

    public override void EnterState(EnemyActionsManagerScript Enemy)
    {
        Enemy.AIState = "AttackPlayer";
        Debug.Log("Entering AttackPlayer State" + Enemy.gameObject);
    }

    public override void ExecuteState(EnemyActionsManagerScript Enemy)
    {
        Enemy.AttackPlayer();
        if (Vector3.Distance(Enemy.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) > 1.0f)
        {
            Enemy.stateMachine.ChangeState(ChasePlayer.Instance);
        }
    }

    public override void ExitState(EnemyActionsManagerScript Enemy)
    {
        Debug.Log("Leaving AttackPlayer State" + Enemy.gameObject);
    }
}