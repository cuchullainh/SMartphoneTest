using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchAreaHarbor : MonoBehaviour {


    Text DebugBox; 
    
    Text myText;
    Touch myTouch;
    int myFIngerID;
    bool iWasTouched = false;
    bool iWasTouchedLastUpdate = false;
    bool colorSwitch = false;
    bool hasMoved = false;
    bool isCHarging = false;
    float passengers = 0;
    bool isSliding = false;
    Vector3 startSLidePoint;
    Vector3 endSLidePoint;
    bool initialiseSlide = false;
    float timer = 0;
    float slidedDistance;
    int moneyPerPassenger;
    int shipPawn = 5000;
    Vector2 uiPosition;
    Camera myCamera;

    public GameObject connRoute0 = null;
    public GameObject connRoute1 = null;
    public GameObject connRoute2 = null;

    float  setShipSpawnSPeed = 0.3f;

    GameManager myManager;
    

  
    int passengersLoaded;

    float routeIncrease = 10;

    void incRoutePoints(float rpoints)
    {
        if (connRoute0 != null)
        {
            connRoute0.GetComponent<EnemyPath>().UsagePoints += rpoints;
           
        }
        if (connRoute1 != null)
        {
            connRoute1.GetComponent<EnemyPath>().UsagePoints += rpoints;
           
        }
        if (connRoute2 != null)
        {
            connRoute2.GetComponent<EnemyPath>().UsagePoints += rpoints;
          
        }
    }

    void Start ()
    {
        myManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        myText = transform.GetChild(0).GetComponent<Text>();
       
        moneyPerPassenger = (int)myManager.MoneyPerPassenger;
        myCamera = Camera.main;
        uiPosition = myCamera.WorldToScreenPoint(gameObject.transform.position);

        DebugBox = GameObject.Find("DebugBox").gameObject.GetComponent<Text>();

       
    }

    void SpawnShip()
    {
        incRoutePoints(routeIncrease);
        myManager.earnMoney(moneyPerPassenger * passengersLoaded);
        GameObject go = (GameObject)Instantiate(Resources.Load("Ship"), transform.parent.parent.FindChild("ShipSpawn").position, Quaternion.identity);
        go.GetComponent<ship>().SetPath(transform.parent.parent.FindChild("Route").gameObject);
        go.GetComponent<ship>().loadPassengers(passengersLoaded);
        go.GetComponent<ship>().SetDefaultSpeed(setShipSpawnSPeed);
        go.GetComponent<ship>().ShipRefund(shipPawn);
        myManager.ShipSpawned(go);
        passengersLoaded = 0;
        myManager.spentMoney(shipPawn);
    }
	void Update ()
    {
        // myText.text = slidedDistance.ToString();
       

        if ( iWasTouchedLastUpdate != iWasTouched)
        {
            if (passengersLoaded >= 10)
            { 
                SpawnShip();
                passengersLoaded = 0;
                iWasTouchedLastUpdate = false;
            }
            slidedDistance = 0;
        }

        if (isCHarging == true)
        {
            passengers += 1 * Time.deltaTime * 20;
            int passengersInt = (int)Mathf.Clamp(passengers, 0, 100);
            myText.text = passengersInt.ToString() + " passengers loaded";
            passengersLoaded = passengersInt;
        }
        if (iWasTouched == true && isSliding == false)
        {
            gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0.3f);
            isCHarging = true;
            iWasTouchedLastUpdate = true;
           
        }
        else if (isSliding == true)
        {
            timer += Time.deltaTime;
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
        }
    }

    void OnGUI()
    {
        if (Input.touchCount > 0)
        {
            myTouch = Input.GetTouch(0);
            myFIngerID = myTouch.fingerId;
        }
        else
        {
            timer = 0;
            iWasTouched = false;
            hasMoved = false;
            isCHarging = false;
            passengers = 0;
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
            isSliding = false;
            initialiseSlide = false;
       
            myFIngerID = 0;
           
        }

        if (Input.touchCount > 0 )
        {
            if (EventSystem.current.IsPointerOverGameObject(myFIngerID))
            {
               
              
                    if (Vector2.Distance(myTouch.position,uiPosition) <= 50 )
                {
                    iWasTouched = true;
                }

                return;
            }
        }
     

        if (myTouch.phase == TouchPhase.Moved)
        {
            
            if (initialiseSlide == false)
            {

                isSliding = true;
                startSLidePoint = myTouch.position;
                initialiseSlide = true;
                isCHarging = false;
            }
            if (timer <= 0.4f)
            {
                endSLidePoint = myTouch.position;
                slidedDistance = Vector3.Distance(startSLidePoint, endSLidePoint);
            }
        }
    }
     
}

/*

    this area is old slider stuff

    using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchAreaHarbor : MonoBehaviour {


    Text DebugBox; 
    
    Text myText;
    Touch myTouch;
    int myFIngerID;
    bool iWasTouched = false;
    bool colorSwitch = false;
    bool hasMoved = false;
    bool isCHarging = false;
    float passengers = 0;
    bool isSliding = false;
    Vector3 startSLidePoint;
    Vector3 endSLidePoint;
    bool initialiseSlide = false;
    float timer = 0;
    float slidedDistance;
    int moneyPerPassenger;
    int shipPawn = 5000;
    Vector2 uiPosition;
    Camera myCamera;

    GameManager myManager;
    

   // int costPerPassenger = 50;
    int passengersLoaded;

    

    void Start ()
    {
         DebugBox = GameObject.Find("DebugBox").gameObject.GetComponent<Text>();
        myManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        myText = transform.GetChild(0).GetComponent<Text>();
        // InvokeRepeating("SpawnShip", 0, 0.5f);
        moneyPerPassenger = (int)myManager.MoneyPerPassenger;
        myCamera = Camera.main;
        uiPosition = myCamera.WorldToScreenPoint(gameObject.transform.position);
    }

    void SpawnShip()
    {
     //   myText.text = "ShipStart";
        myManager.earnMoney(moneyPerPassenger * passengersLoaded);
        GameObject go = (GameObject)Instantiate(Resources.Load("Ship"), transform.parent.parent.FindChild("ShipSpawn").position, Quaternion.identity);
        go.GetComponent<ship>().SetPath(transform.parent.parent.FindChild("Route").gameObject);
        go.GetComponent<ship>().loadPassengers(passengersLoaded);
        go.GetComponent<ship>().setSpeed(slidedDistance);
        go.GetComponent<ship>().ShipRefund(shipPawn);
        passengersLoaded = 0;
        myManager.spentMoney(shipPawn);
    }
	void Update ()
    {
        // myText.text = slidedDistance.ToString();
       

        if ( isSliding == false && slidedDistance >= 75)
        {
            if (passengersLoaded >= 10)
            { 
                SpawnShip();
                passengersLoaded = 0;
            }
            slidedDistance = 0;
        }

        if (isCHarging == true)
        {
            passengers += 1 * Time.deltaTime * 20;
            int passengersInt = (int)Mathf.Clamp(passengers, 0, 100);
            myText.text = passengersInt.ToString() + " passengers loaded";
            passengersLoaded = passengersInt;
        }
        if (iWasTouched == true && isSliding == false)
        {
            gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0.3f);
            isCHarging = true;
        }
        else if (isSliding == true)
        {
            timer += Time.deltaTime;
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
        }
    }

    void OnGUI()
    {
        if (Input.touchCount > 0)
        {
            myTouch = Input.GetTouch(0);
            myFIngerID = myTouch.fingerId;
        }
        else
        {
            timer = 0;
            iWasTouched = false;
            hasMoved = false;
            isCHarging = false;
            passengers = 0;
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
            isSliding = false;
            initialiseSlide = false;

            myFIngerID = 0;
           
        }

        if (Input.touchCount > 0 )
        {
            if (EventSystem.current.IsPointerOverGameObject(myFIngerID))
            {
               
               // DebugBox.text =  "transformpos = " + uiPosition + "tpos = " + myTouch.position;
                    if (Vector2.Distance(myTouch.position,uiPosition) <= 50 )
                {
                    iWasTouched = true;
                }

                return;
            }
        }
     

        if (myTouch.phase == TouchPhase.Moved)
        {
            
            if (initialiseSlide == false)
            {

                isSliding = true;
                startSLidePoint = myTouch.position;
                initialiseSlide = true;
                isCHarging = false;
            }
            if (timer <= 0.4f)
            {
                endSLidePoint = myTouch.position;
                slidedDistance = Vector3.Distance(startSLidePoint, endSLidePoint);
            }
        }
    }
     
}

    */