using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerManagerScript : MonoBehaviour {

    public GameObject BulletPrefab;

    public Transform GameOverCanvas;
    public Transform GamePlayingCanvas;
    public Transform GamePausedCanvas;
    public Transform BloodSplatterUI;
    public Transform LowHealthSplatterUI;
    public Text DeadFinalWaveNumber;
    public Text AmmoRemainingText;

    private float PlayerMovementSpeed = 5.0f;
    private float PlayerRotationSpeed = 250.0f;
    private float BulletTravelSpeed = 1500.0f;
    private float ReloadTimer = 2.0f;
    private float UITimer = 1.0f;
    public float FireAngle;
    public float AccelOffsetZ = 0.0f;

    private int QuantityOfBulletsInObjectPool = 25;
    private int MaximumHealth = 1000;
    public int CurrentHealth;
    private int AmmoCapacity = 10;
    public int CurrentAmmo;
    public int BulletDamage;

    private bool HasBulletFired = false;
    public bool IsPlayerDead = false;
    private bool GunIsReloading = false;

    public Vector3 PlayerDirection = Vector3.zero;
    public Vector3 MousePosition;
    public Vector3 RotatePlayerTo;

    List<GameObject> BulletObjectPool;

	// Use this for initialization
	void Start ()
    {
        //Instantiating the object pool for the bullets
        BulletObjectPool = new List<GameObject>();
        CurrentHealth = MaximumHealth;
        CurrentAmmo = AmmoCapacity;
        BulletDamage = 20;
        AccelOffsetZ = Input.acceleration.z;

        //Adding the GameObjects to the object pool
        for (int i = 0; i < QuantityOfBulletsInObjectPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(BulletPrefab);
            obj.SetActive(false);
            BulletObjectPool.Add(obj);
        }
	}

    // Update is called once per frame
    void Update ()
    {
        CheckForMovementInput();
        CorrectingPlayerPosition();
        UIChecks();

        if (CurrentAmmo <= 0)
        {
            StartCoroutine(GunReload());
        }

        //If the player has died
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            IsPlayerDead = true;
            Time.timeScale = 0;

            if (GamePlayingCanvas.gameObject.activeInHierarchy == true)
            {
                GamePlayingCanvas.gameObject.SetActive(false);
            }

            if (GamePausedCanvas.gameObject.activeInHierarchy == true)
            {
                GamePausedCanvas.gameObject.SetActive(false);
            }

            DeadFinalWaveNumber.text = "You only made it to wave " + GameObject.FindGameObjectWithTag("GameController").GetComponent<EnemyManagerScript>().WaveNumber + "...";
            GameOverCanvas.gameObject.SetActive(true);
        }

        if ((Input.GetButtonDown("Fire1")) && (GunIsReloading == false))
        {
            HasBulletFired = false;
            PlayerFiring();
        }

        
    }

    IEnumerator GunReload()
    {
        //Debug.Log("Reloading...");
        GunIsReloading = true;

        yield return new WaitForSeconds(ReloadTimer);
        CurrentAmmo = AmmoCapacity;
        GunIsReloading = false;
    }

    public void CheckForMovementInput()
    {
        //Movement for Android
        transform.Translate(Input.acceleration.x * PlayerMovementSpeed, 0.0f, (-Input.acceleration.z - AccelOffsetZ) * PlayerMovementSpeed);

        //Movement for testing on Windows
        
        /*if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 0, PlayerMovementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, 0, -PlayerMovementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Vector3.up * PlayerRotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * PlayerRotationSpeed * Time.deltaTime);
        }*/
        
    }

    public void PlayerFiring()
    {
        //Takes the mouse position and converts it from screen to world

        Ray playerHitsRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(playerHitsRay, out hit, 100);

        //Debug.Log(hit.point);

        Vector3 PointToLookAt = hit.point;
        PointToLookAt.y = this.transform.position.y;

        transform.LookAt(PointToLookAt);

        CurrentAmmo--;

        //Goes through the object pool and sets one to active if it's inactive
        for (int i = 0; i < BulletObjectPool.Count; i++)
        {
            if ((!BulletObjectPool[i].activeInHierarchy) && (HasBulletFired == false))
            {
                BulletObjectPool[i].SetActive(true);

                BulletObjectPool[i].transform.position = transform.position;
                BulletObjectPool[i].transform.eulerAngles = new Vector3(90.0f, transform.eulerAngles.y, FireAngle);

                Rigidbody rigidComponent = BulletObjectPool[i].GetComponent<Rigidbody>();
                rigidComponent.AddForce(transform.forward * BulletTravelSpeed);

                HasBulletFired = true;

                
            }
        }
    }

    public void CorrectingPlayerPosition()
    {
        transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);

        if ((transform.position.y < 1) || (transform.position.y > 1))
        {
            transform.position = new Vector3(transform.position.x, 1.0f, transform.position.z);
        }

        if ((transform.rotation.x < 0) || (transform.rotation.x > 0))
        {
            transform.rotation = new Quaternion(0.0f, transform.rotation.y, 0.0f, 0.0f);
        }

        if ((transform.rotation.z < 0) || (transform.rotation.z > 0))
        {
            transform.rotation = new Quaternion(0.0f, transform.rotation.y, 0.0f, 0.0f);
        }
    }

    public void OnCollisionEnter(Collision OtherCollider)
    {
        if (OtherCollider.gameObject.tag == "Normal")
        {
            if ((BloodSplatterUI.gameObject.activeInHierarchy == false) && (CurrentHealth > 300))
            {
                BloodSplatterUI.gameObject.SetActive(true);
            }
        }

        if (OtherCollider.gameObject.tag == "Wall")
        {
            transform.position = new Vector3(-3.0f, 1.0f, 0.0f);
        }
    }

    public void UIChecks()
    {
        if (BloodSplatterUI.gameObject.activeInHierarchy == true)
        {
            UITimer -= Time.deltaTime;
        }

        if (UITimer <= 0.0f)
        {
            UITimer = 1.0f;
            BloodSplatterUI.gameObject.SetActive(false);
        }

        if (CurrentHealth <= 300)
        {
            BloodSplatterUI.gameObject.SetActive(false);
            LowHealthSplatterUI.gameObject.SetActive(true);
        }

        AmmoRemainingText.text = CurrentAmmo.ToString();
    }
}
