using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerr : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    private CharacterController controller;
    private RaycastHit hit;
    [SerializeField] private Camera camerA;
    private float MouseX;
    private float MouseY;
    int item;
    private float TurnSpeed = 90f;
    private float CurrentAngle = 0f;
    private float vertticalPosition, jumpSpeed = 80f, gravitiScale = 9.8f;
    private int AmountOfBullets = 10;
    [SerializeField] public Text text; 
    //public GameObject flaer;
    public GameObject currentEquipItem;
    public GameObject[] equipableItems;
    public GameObject[] thingToSpawn;
    public List<GameObject> inventoryitems;
    public GameObject airDropPrefab;
    private float Damage;
    public int playerhealth = 50;
    [SerializeField] private Text Health;
    public Image panel;
    public Image EnemyConditions;
    public Text enemyHealth, distanceToEnemy, TimeAfterTheEnemyWasCreated;
    //public GameObject objj;
    Vector3 currentPos;
    Vector3 directions;
    Vector3 forGravity;
    
    private void cheking()
    {
        inventoryitems.Add(equipableItems[1]);
        inventoryitems.Add(equipableItems[2]);
    }
    private void Awake()
    {

    }
    void Start()
    
    {
        forGravity.y = gravitiScale * -1f;
        controller = GetComponent<CharacterController>();
        
        //objj = gameObject;
        gameObject.transform.position = new Vector3(0, 10, 0);
        StartCoroutine(nameof(RandomSpawnOfSurprises));
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        currentEquipItem = equipableItems[0];
        //Debug.Log(inventoryitems);
        inventoryitems.Add(equipableItems[0]);
        cheking();
        //Debug.Log(inventoryitems);
    }
    private void EquipRifle()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipItem("Rifle");
            Damage = 2f;
        }
    }
    private void EquipPistol()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipItem("Pistol");
            Damage = 1f;
        }
    }
    private void EquipFlayer()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EquipItem("Flayer");
            Damage = 0; 
        }
    }
    private void EquipItem(string itemM)
    {
        currentEquipItem.SetActive(false);
        foreach (GameObject item in inventoryitems)
        {
            if (item.name == itemM)
            {
                currentEquipItem = item;
                currentEquipItem.SetActive(true);
            }
            else
            {
                continue;
            }
        }
    }
    void Update()
    {
        
        //objj.transform.position = gameObject.transform.position;
        currentPos = new Vector3(
          gameObject.transform.position.x,
          gameObject.transform.position.y,
          gameObject.transform.position.z);
        
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log($"CharacterController: {controller.isGrounded} ");
        }
        if (Input.GetMouseButtonDown(1))
            {
                ActionWithRightButton();
            }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 7f;
        }
        EquipRifle();
        EquipPistol();
        EquipFlayer();
        text.GetComponent<Text>().text = "Amount of bullets: " + AmountOfBullets.ToString();
        Rotate();
        Move();
        Health.GetComponent<Text>().text = $"Health: {playerhealth}";
        if (Physics.Raycast(camerA.transform.position, camerA.transform.forward, out hit, 100f))
        {
            if (hit.transform.tag == "Enemy")
            {
                int EnemyHealth11 = hit.transform.gameObject.GetComponent<EnemyController>().health;
                float Distance1 = hit.transform.gameObject.GetComponent<EnemyController>().dist;
                float EnemyLifeTime = hit.transform.gameObject.GetComponent<EnemyController>().ExistTime;
                EnemyConditions.gameObject.SetActive(true);
                enemyHealth.GetComponent<Text>().text = $"Health : {EnemyHealth11}";
                distanceToEnemy.GetComponent<Text>().text = "Distance: " + Distance1.ToString();
                //TimeAfterTheEnemyWasCreated.GetComponent<Text>().text = EnemyLifeTime.ToString();
            }
            else
            {
                EnemyConditions.gameObject.SetActive(false);
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (AmountOfBullets > 0)
                {
                    Debug.Log("Hit gameObject: " + hit.transform.gameObject.name);
                    Debug.Log("Amount of bullets: " + AmountOfBullets);
                    AmountOfBullets--;
                    Actions(hit.transform.gameObject);
                }

            }
            

            if (Input.GetKeyDown(KeyCode.E))
            {
                PickUpItem(hit.transform.gameObject);
            }
        }
        if (Physics.Raycast(camerA.transform.position, camerA.transform.forward, out hit, 3))
        {
            if (hit.transform.tag == "AirDrop")
            {
                panel.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("hitted");
                    AmountOfBullets += 5;
                    Destroy(hit.transform.gameObject);
                }
            }
            else
            {
                panel.gameObject.SetActive(false);
            }
        }
        if (playerhealth <= 0)
            {
                Destroy(gameObject);
            }
    }
    private void Actions(GameObject obj)
    {
        switch (obj.tag)
        {
            case "Enemy":
                Shoot(obj);
                break;
        }
    }
    private void ActionWithRightButton()
    {
        Debug.Log("ARB works");
        switch (currentEquipItem.tag)
        {
            case "Flayer":
                Debug.Log("S works");
                ThrowFlayer(currentEquipItem.gameObject);
                break;
        }
    }
    private void PickUpItem(GameObject item)
    {
        switch (item.tag)
        {
            case "Ammo":
                AmountOfBullets += 10;
                break;
            case "Rifle":
                inventoryitems.Add(equipableItems[1]);
                break;
            case "Pistol":
                inventoryitems.Add(equipableItems[0]);
                break;
        }
    }
    IEnumerator RandomSpawnOfSurprises()
    {
        Debug.Log("IE works");
        item = UnityEngine.Random.Range(0, 3);
        int posX = UnityEngine.Random.Range(19, 51);
        int posZ = UnityEngine.Random.Range(4, 16);
        GameObject thing = Instantiate(thingToSpawn[item], new Vector3(posX, 12, posZ), Quaternion.identity);
        //Debug.Log("thing position: " + thing.transform.position);
        yield return new WaitForSeconds(5f);
        StartCoroutine(nameof(RandomSpawnOfSurprises));
    }
    private void ThrowFlayer(GameObject Flayer)
    {
        Debug.Log("TF works");
        Flayer.AddComponent<Rigidbody>();
        Flayer.transform.parent = Flayer.gameObject.transform;
        Vector3 AirDropSpawnPos = new Vector3(Flayer.transform.position.x, 50, Flayer.transform.position.z);
        inventoryitems.Remove(Flayer);
        Instantiate(airDropPrefab, AirDropSpawnPos, Quaternion.identity);
    }
    private void Move()
    {
        //Debug.Log($"Controller: {controller.isGrounded}");
        controller.Move(forGravity * Time.deltaTime);
        directions = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        vertticalPosition = 0;
        directions = transform.TransformDirection(directions) * speed;
        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                vertticalPosition = jumpSpeed;
            }
        }
        vertticalPosition -= gravitiScale * Time.deltaTime;
        directions.y = vertticalPosition;
        controller.Move(directions * Time.deltaTime);
    }
    private void Shoot(GameObject target)
    {
        target.GetComponent<EnemyController>().health -= (int)Damage;
        Debug.Log("Enemy health: " + target.GetComponent<EnemyController>().health);
       // Debug.Log(Damage);
    }
    private void Rotate()
    {
        MouseX = Input.GetAxis("Mouse X");
        MouseY = Input.GetAxis("Mouse Y");
        gameObject.transform.Rotate(new Vector3(0, MouseX * TurnSpeed * Time.deltaTime, 0));
        CurrentAngle += MouseY * TurnSpeed * Time.deltaTime * -1f;
        CurrentAngle = Mathf.Clamp(CurrentAngle, -60f, 60f);
        camerA.transform.localEulerAngles = new Vector3(CurrentAngle, 0, 0);
        // Debug.Log(camerA.transform.rotation);
    }
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Touched");
        if (collision.gameObject.tag == "Enemy")
        {
            playerhealth--;
            Debug.Log($"Player health {playerhealth}");
        }
        /*
        if (collision.gameObject.tag == "Ground")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                vertticalPosition = jumpSpeed;
            }
            vertticalPosition -= gravitiScale * Time.deltaTime;
            directions.y = vertticalPosition;
        }
        */
    }
    private void OnTriggerStay(Collider collision)
    {
        Debug.Log("Touched");
        if (collision.gameObject.tag == "Enemy")
        {
            playerhealth--;
            Debug.Log($"Player health {playerhealth}");
        }
    }

}
