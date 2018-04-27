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

    private int MaximumHealth = 100;
    public int CurrentHealth;
    public const int AttackDamage = 5;

    public const float MeleeRange = 1.0f;
    public const float CollectRangee = 1.0f;

    public bool IsAlive;

    public Vector3 SpawnPosition;
    public Vector3 ReturnLocation;

    private NavMeshAgent EnemyAgent;

    public string AIState;
    public GameObject[] CorePieces;
    public AIStateMachine<EnemyActionsManagerScript> stateMachine { get; set; }
    private float ShortestDistanceToPiece = 100.0f;
    public Vector3 CoreTarget = new Vector3(0.0f, 0.0f, 0.0f);

    // Use this for initialization
    void Start ()
    {
        EnemyAgent = GetComponent<NavMeshAgent>();
        stateMachine = new AIStateMachine<EnemyActionsManagerScript>(this);
        stateMachine.ChangeState(ChasePlayer.Instance);
        SpawnPosition = transform.position;
        CurrentHealth = MaximumHealth;
        IsAlive = true;

        CorePieces = GameObject.FindGameObjectsWithTag("Core");
	}
	void Update ()
    {
        stateMachine.Update();
    }

    public void OnCollisionEnter(Collision OtherCollider)
    {
        if (OtherCollider.gameObject.tag == "Bullet")
        {
            CurrentHealth -= GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManagerScript>().BulletDamage;
        }
    }

    public void MoveTowardsPlayer()
    {
        EnemyAgent.destination = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    public void MoveTowardsCore()
    {
        

        for (int i = 0; i < CorePieces.Length; i++)
        {
            float DistanceToPiece = Vector3.Distance(transform.position, CorePieces[i].transform.position);
            if (DistanceToPiece < ShortestDistanceToPiece)
            {
                ShortestDistanceToPiece = DistanceToPiece;
                CoreTarget = CorePieces[i].transform.position;
            }

            EnemyAgent.destination = CoreTarget;
        }
        //EnemyAgent.destination = GameObject.FindGameObjectWithTag("Core").transform.position;
    }

    public void AttackPlayer()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManagerScript>().CurrentHealth -= AttackDamage;
    }

    public void FleeWithCorePiece()
    {
        ReturnLocation = new Vector3(SpawnPosition.x + Random.Range(-5.0f, 5.0f), SpawnPosition.y, SpawnPosition.z + Random.Range(-5.0f, 5.0f));
        EnemyAgent.destination = ReturnLocation;
    }

    public void EnemyDeath()
    {
        IsAlive = false;
        gameObject.SetActive(false);
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
            if (Vector3.Distance(Enemy.transform.position, Enemy.GetComponent<EnemyActionsManagerScript>().CoreTarget) < 1.0f)
            {
                Enemy.stateMachine.ChangeState(Flee.Instance);
            }
            else
            {
                Enemy.MoveTowardsCore();
            }
        }

        if (Enemy.CurrentHealth <= 0)
        {
            Enemy.stateMachine.ChangeState(AIDeath.Instance);
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

    #region AttackPlayer State Machine
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

        if (Enemy.CurrentHealth <= 0)
        {
            Enemy.stateMachine.ChangeState(AIDeath.Instance);
        }
    }

    public override void ExitState(EnemyActionsManagerScript Enemy)
    {
        Debug.Log("Leaving AttackPlayer State" + Enemy.gameObject);
    }
    #endregion
}

public class AIDeath : State<EnemyActionsManagerScript>
{
    #region AIDeath state instance set up
    static readonly AIDeath instance = new AIDeath();
    public static AIDeath Instance
    {
        get
        {
            return instance;
        }
    }
    static AIDeath() { }
    private AIDeath() { }
    #endregion


    #region AIDeath State Machine
    public override void EnterState(EnemyActionsManagerScript Enemy)
    {
        Enemy.AIState = "AIDeath";
        Debug.Log("Entering AIDeath State" + Enemy.gameObject);
    }

    public override void ExecuteState(EnemyActionsManagerScript Enemy)
    {
        Enemy.EnemyDeath();
    }

    public override void ExitState(EnemyActionsManagerScript Enemy)
    {
        Debug.Log("Leaving AIDeath State" + Enemy.gameObject);
    }
    #endregion
}

public class Flee : State<EnemyActionsManagerScript>
{
    #region Flee state instance set up
    static readonly Flee instance = new Flee();
    public static Flee Instance
    {
        get
        {
            return instance;
        }
    }
    static Flee() { }
    private Flee() { }
    #endregion


    #region Flee State Machine
    public override void EnterState(EnemyActionsManagerScript Enemy)
    {
        Enemy.AIState = "Flee";
        Debug.Log("Entering Flee State" + Enemy.gameObject);
    }

    public override void ExecuteState(EnemyActionsManagerScript Enemy)
    {
        if (Enemy.transform.position == Enemy.GetComponent<EnemyActionsManagerScript>().ReturnLocation)
        {
            Enemy.stateMachine.ChangeState(ChasePlayer.Instance);
        }
        else
        {
            Enemy.FleeWithCorePiece();
        }
    }

    public override void ExitState(EnemyActionsManagerScript Enemy)
    {
        Debug.Log("Leaving Flee State" + Enemy.gameObject);
    }
    #endregion
}