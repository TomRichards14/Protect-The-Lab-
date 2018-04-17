using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManagerScript : MonoBehaviour {

    public GameObject NormalEnemyPrefab;
    public GameObject CoreEnemyPrefab;
    public GameObject PlayerGameObject;

    public float SpawnXPosition;
    public float SpawnXVariation;
    public float SpawnZPosition;
    public float SpawnZVariation;

    public int QuantityOfEnemiesInObjectPool = 125;
    public int CurrentQuantityInWave;

    List<GameObject> NormalEnemiesObjectPool;
    List<GameObject> CoreEnemiesObjectPool;

    public bool IsCurrentWaveOver;

	// Use this for initialization
	void Start ()
    {
        IsCurrentWaveOver = true;
        NormalEnemiesObjectPool = new List<GameObject>();
        CoreEnemiesObjectPool = new List<GameObject>();

        //Object pool for the normal enemies that chase the player
        for (int i = 0; i < QuantityOfEnemiesInObjectPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(NormalEnemyPrefab);
            obj.SetActive(false);
            NormalEnemiesObjectPool.Add(obj);
        }

        //Object pool for the enemies that will only go for the core
        for (int i = 0; i < QuantityOfEnemiesInObjectPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(CoreEnemyPrefab);
            obj.SetActive(false);
            CoreEnemiesObjectPool.Add(obj);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        ChooseNewSpawnPoint();
		//if (IsCurrentWaveOver == true)
        //{
        //    SpawnXPosition = 0.0f;
        //    ChooseNewSpawnPoint();
        //}
	}

    void ChooseNewSpawnPoint()
    {
        //while ((SpawnXPosition > -16.0f) || (SpawnXPosition < 16.0f))
        {
            SpawnXPosition = Random.Range(-20, 20);
        }
        Debug.Log(SpawnXPosition);


        SpawnZPosition = Random.Range(-30, 30);        
    }
}
