using UnityEngine;
using System.Collections;

public class EnemyCOntroller : MonoBehaviour {

  
    float spawnTimer = 0;
    float spawnInterval = 3;


	void Start ()
    {
        
	}
	

	void Update ()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            float routeSeed = Random.Range(0, 1000);
            float numberOfRoutes= (float)transform.childCount;
            float randomRoute =  Mathf.Lerp(0, numberOfRoutes-1, routeSeed/1000);
           int  round = Mathf.RoundToInt(randomRoute);
         
            spawnInterval = Random.Range(100, 1000) / 100;
            float seed = Random.Range(0, 1000);
            float RandomSpeed = Mathf.Lerp(0.4f, 1.5f, seed / 1000);
            bool RandomDir;
            int a = Random.Range(0, 1000);
            if (a % 2 == 0)
            {
                RandomDir = false;
            }
           
            else
            {
                RandomDir = true;
            }
            spawnTimer = 0;
            SpawnEnemy(round, RandomDir,RandomSpeed);
        }
    }


    void SpawnEnemy(int route,bool direction, float speed)
    {
        Vector3 StartPOint;
        bool routeDirection = direction;

        if (routeDirection == false)
        {
             StartPOint = transform.GetChild(route).GetChild(0).position;
        }
        else
        {
            int childsNumber = transform.GetChild(route).childCount;
             StartPOint = transform.GetChild(route).GetChild(childsNumber-1).position;
        }
        GameObject go = (GameObject)Instantiate(Resources.Load("EnemyShip"), StartPOint, Quaternion.identity);
        go.GetComponent<EnemyShip>().SetPath(transform.GetChild(route).gameObject);
        go.GetComponent<EnemyShip>().SetDirection(direction);
        go.GetComponent<EnemyShip>().SetSpeed(speed);

    }
}
