using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyCOntroller : MonoBehaviour {

    GameManager myManager;
    float spawnTimer = 0;
    float spawnInterval = 20;
    bool firstSPawn = false;
    int shipCounter = 0;
    int maxShips = 20;
    int minShips = 5;
    int currentMaxShipsFromAwareness;
    float minSpeed = 1f;
    float maxSpeed = 2f;
  
    Text debugFIeld;


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
        debugFIeld = GameObject.Find("DebugBox").GetComponent<Text>();
    }
	

	void Update ()
    {
      
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval || firstSPawn == false)
        {
            
            firstSPawn = true;
            currentMaxShipsFromAwareness =(int) Mathf.Lerp(minShips, maxShips, myManager.Awareness / myManager.MaxAwareNess);
            if (shipCounter < currentMaxShipsFromAwareness)
            {
                float highestPoints = 0;
                int tempRoute = 0;

                for (int b = 0; b < transform.childCount; b++)
                {
                   
                    if (highestPoints < transform.GetChild(b).GetComponent<EnemyPath>().UsagePoints)
                    {
                        highestPoints = transform.GetChild(b).GetComponent<EnemyPath>().UsagePoints;
                        tempRoute = b;
                    }
                }
             
              //  float routeSeed = Random.Range(0, 1000);
              
                //float numberOfRoutes = (float)transform.childCount;
             //   float randomRoute = Mathf.Lerp(0, numberOfRoutes - 1, routeSeed / 1000);
               // int round = Mathf.RoundToInt(randomRoute);

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
                SpawnEnemy(tempRoute, RandomDir, speed);
            }
        }
    }


    void SpawnEnemy(int route,bool direction, float speed)
    {
       // transform.GetChild(route).GetComponent<EnemyPath>().UsagePoints += routeIncrease;
      //  debugFIeld.text = transform.GetChild(route).GetComponent<EnemyPath>().UsagePoints.ToString();

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
