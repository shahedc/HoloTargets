using UnityEngine;
using System.Collections;

public class ItemGenerator : MonoBehaviour
{
    public GameObject redBall;
    public float spawnTime = 3f;
    // Use this for initialization
    void Start ()
    {
        InvokeRepeating("SpawnBall", spawnTime, spawnTime);
    }
	
	// Update is called once per frame
	void Update () {

    }

    void SpawnBall()
    {
        var spawnerCenter = transform.position;
        var spawnerWidth = 3.0f;
        var spawnerHeight = 3.0f;
        var newX = spawnerCenter.x + Random.Range(-spawnerWidth, spawnerWidth);
        var newY = 0.5f;
        var newZ = spawnerCenter.z + Random.Range(-spawnerWidth, spawnerWidth);

        var newBall = Instantiate(redBall, new Vector3(newX, newY, newZ), Quaternion.identity);
    }
}
