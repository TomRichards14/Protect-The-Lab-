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
    public const int Damage = 5;

    public const float MeleeRange = 1.0f;
    public const float CollectRangee = 1.0f;

    public bool IsAlive;

    public Vector3 SpawnPosition;

    private NavMeshAgent EnemyAgent;

	// Use this for initialization
	void Start ()
    {
        EnemyAgent = GetComponent<NavMeshAgent>();
        SpawnPosition = transform.position;
        CurrentHealth = MaximumHealth;
        IsAlive = true;
	}
	
	public void MoveTowardsPlayer(GameObject PlayerGameObject)
    {
        EnemyAgent.destination = PlayerGameObject.transform.position;
    }

    public void MoveTowardsCorePiece(GameObject CorePieceGameObject)
    {
        EnemyAgent.destination = CorePieceGameObject.transform.position;
    }

    public void AttackPlayer(GameObject PlayerGameObject)
    {
        if ((PlayerGameObject.CompareTag(ConstantTags.PlayerTag)) && (Vector3.Distance(transform.position, PlayerGameObject.transform.position) < MeleeRange))
        {

        }
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
