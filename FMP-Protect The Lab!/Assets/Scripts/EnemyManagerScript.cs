using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManagerScript : MonoBehaviour {

    public GameObject NormalEnemyPrefab;
    public GameObject CoreEnemyPrefab;
    //public GameObject PlayerGameObject;

    public float SpawnXPosition;
    public float SpawnZPosition;

    private int QuantityOfEnemiesInObjectPool = 250;
    private int CurrentNormalQuantityInWave;
    private int CurrentSpecialQuantityInWave;

    List<GameObject> NormalEnemiesObjectPool;
    List<GameObject> SpecialEnemiesObjectPool;

    public bool IsCurrentWaveOver;

	// Use this for initialization
	void Start ()
    {
        IsCurrentWaveOver = true;
        NormalEnemiesObjectPool = new List<GameObject>();
        SpecialEnemiesObjectPool = new List<GameObject>();
        CurrentNormalQuantityInWave = 10;
        CurrentSpecialQuantityInWave = 2;

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
            SpecialEnemiesObjectPool.Add(obj);
        }

        ChooseNewSpawnPoint();
        SpawnNextWave();
    }
	
	// Update is called once per frame
	void Update ()
    {
        /*ChooseNewSpawnPoint();
        SpawnNextWave();*/

        /*if (IsCurrentWaveOver == true)
        {
            CurrentNormalQuantityInWave += 10;
            CurrentSpecialQuantityInWave += 10;
            SpawnXPosition = 0.0f;
            ChooseNewSpawnPoint();
        }*/
    }

    void ChooseNewSpawnPoint()
    {
        //Spawn on the sides
        if (Random.value <= 0.5f)
        {
            //Spawn on the right side
            if (Random.value <= 0.5f)
            {
                SpawnZPosition = Random.Range(-28.0f, -40.0f);
                SpawnXPosition = Random.Range(-17.0f, 17.0f);                
            }
            //Spawn on the left side
            else
            {
                SpawnZPosition = Random.Range(28.0f, 40.0f);
                SpawnXPosition = Random.Range(-17.0f, 17.0f);
            }
        }
        //Spawn on the top/bottom
        else
        {
            //Spawn on the top
            if (Random.value <= 0.5f)
            {
                SpawnZPosition = Random.Range(-30.0f, 30.0f);
                SpawnXPosition = Random.Range(18.0f, 25.0f);
            }
            //Spawn on the bottom
            else
            {
                SpawnZPosition = Random.Range(-30.0f, 30.0f);
                SpawnXPosition = Random.Range(-18.0f, -25.0f);
            }
        }
    }

    public void SpawnNextWave()
    {
        for (int i = 0; i < CurrentNormalQuantityInWave; i++)
        {
            if (!NormalEnemiesObjectPool[i].activeInHierarchy)
            {
                NormalEnemiesObjectPool[i].SetActive(true);

                NormalEnemiesObjectPool[i].transform.position = new Vector3(SpawnXPosition + Random.Range(-1.0f, 1.0f), 1.0f, SpawnZPosition + Random.Range(-1.0f, 1.0f) );
                NormalEnemiesObjectPool[i].transform.eulerAngles = transform.eulerAngles;
            }
        }

        for (int i = 0; i < CurrentSpecialQuantityInWave; i++)
        {
            if (!SpecialEnemiesObjectPool[i].activeInHierarchy)
            {
                SpecialEnemiesObjectPool[i].SetActive(true);

                SpecialEnemiesObjectPool[i].transform.position = new Vector3(SpawnXPosition + Random.Range(-1.0f, 1.0f), 1.0f, SpawnZPosition + Random.Range(-1.0f, 1.0f));
                SpecialEnemiesObjectPool[i].transform.eulerAngles = transform.eulerAngles;
            }
        }
    }
}
