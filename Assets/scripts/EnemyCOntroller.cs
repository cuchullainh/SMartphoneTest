using UnityEngine;
using System.Collections;

public class EnemyCOntroller : MonoBehaviour {

    GameManager myManager;
    float spawnTimer = 0;
    float spawnInterval = 1;
    int shipCounter = 0;
    int maxShips = 8;
    int minShips = 2;
    int currentMaxShipsFromAwareness;
    float minSpeed = 1f;
    float maxSpeed = 2f;


    public int ShipCounter
    {
        get
        {
            return shipCounter;
        }

        set
        {
            shipCounter = value;
        }
    }

    void Start ()
    {
        myManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
	

	void Update ()
    {
       // print(currentMaxShipsFromAwareness);
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval )
        {
            currentMaxShipsFromAwareness =(int) Mathf.Lerp(minShips, maxShips, myManager.Awareness / myManager.MaxAwareNess);
            if (shipCounter < currentMaxShipsFromAwareness)
            {
                float routeSeed = Random.Range(0, 1000);
                float numberOfRoutes = (float)transform.childCount;
                float randomRoute = Mathf.Lerp(0, numberOfRoutes - 1, routeSeed / 1000);
                int round = Mathf.RoundToInt(randomRoute);

                // spawnInterval = Random.Range(100, 1000) / 100;
                //float seed = Random.Range(0, 1000);
                // float RandomSpeed = Mathf.Lerp(0.4f, 1.5f, seed / 1000);
                float speed = Mathf.Lerp(minSpeed, maxSpeed, myManager.Awareness / myManager.MaxAwareNess);

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
                SpawnEnemy(round, RandomDir, speed);
            }
        }
    }


    void SpawnEnemy(int route,bool direction, float speed)
    {
        ShipCounter++;
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
