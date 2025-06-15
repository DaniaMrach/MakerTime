using System.Collections;
using UnityEngine;
public class airDropSpawner : MonoBehaviour
{

    private float posX, posZ, posY = 50;
    public GameObject airDrop;
    private void Start()
    {
        posX = Random.Range(19, 51);
        posZ = Random.Range(4, 16);
        StartCoroutine(nameof(AirDropSpawn));
    }
    IEnumerator AirDropSpawn()
    {
        airDrop = Instantiate(airDrop, new Vector3(posX, posY, posZ), Quaternion.identity);
        Debug.Log(airDrop.transform.position);
        yield return new WaitForSeconds(5f);
        StartCoroutine(nameof(AirDropSpawn));
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}
