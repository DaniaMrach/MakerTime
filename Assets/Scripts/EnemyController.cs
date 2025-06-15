using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Scripting.APIUpdating;

public class EnemyController : MonoBehaviour
{
    private float speed = 1f;
    public Transform[] points;
    private NavMeshAgent agent;
    private Transform transformM;
    //[SerializeField] private GameObject player;
    public int health = 2;
    //private float Distance;
    public float dist;
    public GameObject dest;
    
    public float ExistTime;
    private PlayerController playerController;
    GameObject playerPosition;
    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player");
        playerController = GetComponent<PlayerController>();
        //Debug.Log(agent.destination);
        ExistTime = Time.time;
        agent = GetComponent<NavMeshAgent>();
        transformM = GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        
        agent.speed = speed;
        /*  Distance = Vector3.Distance(new Vector3(
              gameObject.transform.position.x,
              gameObject.transform.position.y,
              gameObject.transform.position.z),
              new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z));
        */
        //dist = Vector3.Distance(player.transform.position, gameObject.transform.position);
        //Debug.Log(dist);
        DestroyItself();
        agent.destination = playerPosition.transform.position;
        
        
        
            //Patrool();
        
    }
    public void DestroyItself()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    /*private void Patrool()
    {
        for (int i = 0; i <= 3; i++)
        {
            gameObject.GetComponent<NavMeshAgent>().Move(points[i].transform.position);
        }
    }*/
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerControllerr>().playerhealth -= 1;
            int playerh = collision.gameObject.GetComponent<PlayerControllerr>().playerhealth;
            Debug.Log($"Player health {playerh}");
        }
    }
}
