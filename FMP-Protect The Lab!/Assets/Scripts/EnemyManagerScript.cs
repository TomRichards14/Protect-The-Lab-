using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManagerScript : MonoBehaviour {

    public GameObject NormalEnemyPrefab;
    public GameObject CoreEnemyPrefab;
    //public GameObject PlayerGameObject;

    private float SpawnXPosition;
    private float SpawnZPosition;

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
        Debug.Log("X position " + SpawnXPosition);
        Debug.Log("Z position " + SpawnZPosition);

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
                Debug.Log("Right Spawn");
                SpawnXPosition = Random.Range(40.0f, 45.0f);
                SpawnZPosition = Random.Range(-26.0f, 26.0f);
                
            }
            //Spawn on the left side
            else
            {
                Debug.Log("Left Spawn");
                SpawnXPosition = Random.Range(-40.0f, -45.0f);
                SpawnZPosition = Random.Range(-26.0f, 26.0f);
                
            }
        }
        //Spawn on the top/bottom
        else
        {
            //Spawn on the top
            if (Random.value <= 0.5f)
            {
                Debug.Log("Top Spawn");
                SpawnXPosition = Random.Range(-45.0f, 45.0f);
                SpawnZPosition = Random.Range(20.0f, 26.0f);
                
            }
            //Spawn on the bottom
            else
            {
                Debug.Log("Bottom Spawn");
                SpawnXPosition = Random.Range(-45.0f, 45.0f);
                SpawnZPosition = Random.Range(-20.0f, -26.0f);
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

                NormalEnemiesObjectPool[i].transform.position = new Vector3(SpawnXPosition, 1.0f, SpawnZPosition);
                NormalEnemiesObjectPool[i].transform.eulerAngles = transform.eulerAngles;
            }
        }

        for (int i = 0; i < CurrentSpecialQuantityInWave; i++)
        {
            if (!SpecialEnemiesObjectPool[i].activeInHierarchy)
            {
                SpecialEnemiesObjectPool[i].SetActive(true);

                SpecialEnemiesObjectPool[i].transform.position = new Vector3(SpawnXPosition, 1.0f, SpawnZPosition);
                SpecialEnemiesObjectPool[i].transform.eulerAngles = transform.eulerAngles;
            }
        }
    }
}
