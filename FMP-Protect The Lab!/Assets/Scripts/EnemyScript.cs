using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    public GameObject EnemyPrefab;
    public GameObject PlayerGameObject;

    public float SpawnXPosition;
    public float SpawnZPosition;

    public int QuantityOfEnemiesInObjectPool = 125;
    public int CurrentQuantityInWave;

    List<GameObject> EnemiesObjectPool;

    public bool IsCurrentWaveOver;

	// Use this for initialization
	void Start ()
    {
        IsCurrentWaveOver = true;
        EnemiesObjectPool = new List<GameObject>();

        for (int i = 0; i < QuantityOfEnemiesInObjectPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(EnemyPrefab);
            obj.SetActive(false);
            EnemiesObjectPool.Add(obj);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (IsCurrentWaveOver == true)
        {

        }
	}

    void ChooseNewSpawnPoint()
    {
        SpawnXPosition = Random.Range();
        SpawnZPosition = Random.Range();
    }
}
