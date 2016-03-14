using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ship : MonoBehaviour {
    int nexWayPoint = 0;
    GameObject myParentPath;
    int childItems;
    Vector3 despawnPoint;
    int passengersLoaded;
    GameManager myManager;
    //int livingPassengersValue = 100;
    float speed;
    int awarenessPerDeath;
    int shipRefund = 0;
    float greenWeight = 1;
    float yellowWeight = 0.6f;
    float redWeight = 0.2f;
    float yellowThreshold = 50;
    float redThreshold = 75;
    float calcSpeed = 0;
    float originSpeed = 0;
    Rigidbody2D myRig;

    Canvas shipCanvas;

    bool oneTimeSet = false;

    float spottedValue = 0;
    float maxSpottedValue = 100;
    bool spotted = false;



    Text debugFIeld;

    

    public int PassengersLoaded
    {
        get
        {
            return passengersLoaded;
        }

       
    }

    public float SpottedValue
    {
        get
        {
            return spottedValue;
        }

        set
        {

            spottedValue = value;
            spottedValue = Mathf.Clamp(spottedValue, 0, maxSpottedValue);
            shipCanvas.GetComponent<ShipText>().SetSliderValue(spottedValue);
            if (spottedValue == maxSpottedValue)
            {
                DestroyMe();
            }
        }
    }
    

    void Start ()
    {
        myRig = GetComponent<Rigidbody2D>();
      //  myRig.WakeUp();
        myManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        awarenessPerDeath = (int)myManager.AwarenessPerPassengerDead;
        shipCanvas = transform.GetChild(0).GetComponent<Canvas>();
       
        shipCanvas.transform.eulerAngles = new Vector3(0, 0, 0);

        debugFIeld = GameObject.Find("DebugBox").GetComponent<Text>();
        SpottedValue = spottedValue;

    }
	
	
	void Update ()
    {
      //  debugFIeld.text = spottedValue.ToString();

        if (oneTimeSet == false)
        {
            shipCanvas.transform.SetParent(null);
            oneTimeSet = true;
        }
        shipCanvas.transform.position = gameObject.transform.position;
     

        if (Vector3.Distance(transform.position, myParentPath.transform.GetChild(nexWayPoint).position) <= 0.1f)
        {
            if (nexWayPoint < childItems - 1)
                nexWayPoint++;
        }
        if (Vector3.Distance(transform.position, despawnPoint) <= 0.1f)
        {
            //doSomethingwhenShipReachesTarget
            myManager.earnMoney(shipRefund);
            Destroy(shipCanvas);
            Destroy(gameObject);
          
        }
        transform.LookAt(myParentPath.transform.GetChild(nexWayPoint).position);
          transform.Translate(0,0, speed * Time.deltaTime);
       

    }
    void FixedUpdate()
    {
     //   myRig.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
    }
    public void ReducePassengers()
    {
        if (passengersLoaded > 0)
        {
            IncreaseAwareness(10);
            passengersLoaded -= 10;
            passengersLoaded = Mathf.Clamp(passengersLoaded, 0, 100);
            shipCanvas.GetComponent<ShipText>().SetShipText(passengersLoaded.ToString());
            setSpeed(originSpeed);
        }
    }

    public void ShipRefund(int refund)
    {
        shipRefund = refund;
    }
    public void SetPath(GameObject parent)
    {
        myParentPath = parent;
        childItems = myParentPath.transform.childCount;
        despawnPoint = myParentPath.transform.GetChild(childItems - 1).position;
    }

    /*
    void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.gameObject.tag == "Enemy")
        {
           
          
        }
       
    }
    */

    public void DestroyMe()
    {
        Instantiate(Resources.Load("ShipDownParticle"), transform.position, Quaternion.identity);
        IncreaseAwareness(passengersLoaded);
        Destroy(shipCanvas);
        Destroy(gameObject);
    }
    public void loadPassengers(int passengers)
    {
        passengersLoaded = passengers;
    
    }

    public void IncreaseAwareness(int passengers)
    {
        myManager.AddAwareness(passengers * awarenessPerDeath);
    }

    public void setSpeed(float newSpeed)
    {
        
        originSpeed = newSpeed;
        calcSpeed = originSpeed;
        // float calcSpeed = Mathf.Lerp(0.1f, 1, newSpeed/800);
        speed = calcSpeed* LoadedColorNSpeed();
    }

    public float LoadedColorNSpeed()
    {
        float speedCoeff = 0;
        if (passengersLoaded < yellowThreshold)
        {
            speedCoeff = greenWeight;
            GetComponent<MeshRenderer>().material.color = Color.green;
        }
        else if (passengersLoaded >= yellowThreshold && passengersLoaded < redThreshold)
        {
            speedCoeff = yellowWeight;
            GetComponent<MeshRenderer>().material.color = Color.yellow;
        }
        else if (passengersLoaded >= redThreshold)
        {
            speedCoeff = redWeight;
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
       
        return speedCoeff;
    }
    
}
