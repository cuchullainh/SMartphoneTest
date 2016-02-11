using UnityEngine;
using System.Collections;

public class ship : MonoBehaviour {
    int nexWayPoint = 0;
    GameObject myParentPath;
    int childItems;
    Vector3 despawnPoint;
    int passengersLoaded;
    GameManager myManager;
    int livingPassengersValue = 100;
    float speed;

    void Start ()
    {
        myManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }
	
	
	void Update ()
    {
        if (Vector3.Distance(transform.position, myParentPath.transform.GetChild(nexWayPoint).position) <= 0.1f)
        {
            if (nexWayPoint < childItems - 1)
                nexWayPoint++;
        }
        if (Vector3.Distance(transform.position, despawnPoint) <= 0.1f)
        {
            myManager.earnMoney(passengersLoaded * 100);
            Destroy(gameObject);
        }
        transform.LookAt(myParentPath.transform.GetChild(nexWayPoint).position);
        transform.Translate(0,0, speed * Time.deltaTime);
	}

    public void SetPath(GameObject parent)
    {
        myParentPath = parent;
        childItems = myParentPath.transform.childCount;
        despawnPoint = myParentPath.transform.GetChild(childItems - 1).position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Enemy")
            Instantiate(Resources.Load("ShipDownParticle"), transform.position, Quaternion.identity);
            Destroy(gameObject);

    }

    public void loadPassengers(int passengers)
    {
        passengersLoaded = passengers;
    }

    public void setSpeed(float newSpeed)
    {
        float calcSpeed = Mathf.Lerp(0.1f, 1, newSpeed/800);
        speed = calcSpeed;
    }
}
