using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchAreaHarbor : MonoBehaviour {


    Text DebugBox; 
    
   
    Touch myTouch;
    int myFIngerID;
    bool oHarborIsTouched = false;
   // bool iWasTouchedLastUpdate = false;
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

    GameObject currentRoute;

    //GameObject targetHarbor;
    //GameObject targetHarbor1;
    //GameObject targetHarbor2;

    bool targetHarborMarked = false;
    //bool targetHarborMarked1 = false;
    //bool targetHarborMarked2 = false;

    bool isPassengersLoaded = false;

   // bool markOriginHarbor = false;

    public GameObject connRoute0 = null;
    public GameObject connRoute1 = null;
    public GameObject connRoute2 = null;

    float  setShipSpawnSPeed = 0.3f;

    GameManager myManager;
    
    int passengersLoaded;

    float routeIncrease = 10;

    Text myText;
    //bool Ship0 = true;
    float ship0Abnutzung = 0;
    bool ship0Destroyed = false;
    float ship0CooldownTimer = 0;
    float ship0cooldownIntervall = 20;
    bool ship0inAction = false;
    Slider ship0AbnutzungBar;

    Text myText2;
   // bool Ship2 = true;
    float ship2Abnutzung = 0;
    bool ship2Destroyed = false;
    float ship2CooldownTimer = 0;
    float ship2cooldownIntervall = 20;
    bool ship2inAction = false;
    Slider ship2AbnutzungBar;

    Text myText3;
   // bool Ship3 = true;
    float ship3Abnutzung = 0;
    bool ship3Destroyed = false;
    float ship3CooldownTimer = 0;
    float ship3cooldownIntervall = 20;
    bool ship3inAction = false;
    Slider ship3AbnutzungBar;

    void ShowShipText()
    {
       
        if (ship0inAction == true)
        {
            myText.text = "Ship1 rdy in " + Mathf.RoundToInt(ship0cooldownIntervall - ship0CooldownTimer) + "sek";
        }
        else
        {
            myText.text = "Ship1 is Rdy";
        }
        ship0AbnutzungBar.value = ship0Abnutzung;

      
        ship0AbnutzungBar.value = ship0Abnutzung;
        if (ship2inAction == true)
        {
            myText2.text = "Ship2 rdy in " + Mathf.RoundToInt(ship2cooldownIntervall - ship2CooldownTimer) + "sek";
        }
        else
        {
            myText2.text = "Ship2 is Rdy";
        }

      
        ship0AbnutzungBar.value = ship0Abnutzung;
        if (ship3inAction == true)
        {
            myText3.text = "Ship3 rdy in " + Mathf.RoundToInt(ship3cooldownIntervall - ship3CooldownTimer) + "sek";
        }
        else
        {
            myText3.text = "Ship3 is Rdy";
        }
    }

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

     public void setAbnutzung0(float abn)
    {
        ship0Abnutzung = abn;
    }

    public void setAbnutzung2(float abn)
    {
        ship2Abnutzung = abn;
    }

    public void setAbnutzung3(float abn)
    {
        ship3Abnutzung = abn;
    }

    void Start ()
    {
        myManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        myText = transform.FindChild("Text1").GetComponent<Text>();
        ship0AbnutzungBar = transform.FindChild("ShipStatus1").GetComponent<Slider>();

        myText2 = transform.FindChild("Text2").GetComponent<Text>();
        ship2AbnutzungBar = transform.FindChild("ShipStatus2").GetComponent<Slider>();

        myText3 = transform.FindChild("Text3").GetComponent<Text>();
        ship3AbnutzungBar = transform.FindChild("ShipStatus3").GetComponent<Slider>();

        moneyPerPassenger = (int)myManager.MoneyPerPassenger;
        myCamera = Camera.main;
        uiPosition = myCamera.WorldToScreenPoint(gameObject.transform.position);

        DebugBox = GameObject.Find("DebugBox").gameObject.GetComponent<Text>();


        // targetHarbor = transform.parent.parent.FindChild("TargetHarbors").GetChild(0).gameObject;
        //targetHarbor1 = transform.parent.parent.FindChild("TargetHarbors").GetChild(1).gameObject;
        // targetHarbor = GameObject.Find("TargetHarbors").transform.GetChild(0).gameObject;

    }

    void SpawnShip()
    {
        incRoutePoints(routeIncrease);
        myManager.earnMoney(moneyPerPassenger * passengersLoaded);
        GameObject go = (GameObject)Instantiate(Resources.Load("Ship"), transform.parent.parent.FindChild("ShipSpawn").position, Quaternion.identity);
        go.GetComponent<ship>().SetPath(currentRoute);
        go.GetComponent<ship>().loadPassengers(passengersLoaded);
        go.GetComponent<ship>().SetDefaultSpeed(setShipSpawnSPeed);
        go.GetComponent<ship>().ShipRefund(shipPawn);
        go.GetComponent<ship>().SetAbnutzung(ship0Abnutzung);
        go.GetComponent<ship>().SetOriginHarbor(gameObject);
        myManager.ShipSpawned(go);
        passengersLoaded = 0;
        myManager.spentMoney(shipPawn);
        isPassengersLoaded = false;
        targetHarborMarked = false;

        if (ship0inAction == false)
        {
            ship0inAction = true;
        }
        else if(ship0inAction == true && ship2inAction == false)
        {

            ship2inAction = true;
        }
        else if (ship0inAction == true && ship2inAction == true && ship3inAction == false)
        {

            ship3inAction = true;
        }

    }
    void ShipCoolDown()
    {
        if (ship0inAction == true)
        {
            ship0CooldownTimer += Time.deltaTime;
            if (ship0CooldownTimer >= ship0cooldownIntervall)
            {
                ship0CooldownTimer = 0;
                ship0inAction = false;
            }
        }

        if (ship2inAction == true)
        {
            ship2CooldownTimer += Time.deltaTime;
            if (ship2CooldownTimer >= ship2cooldownIntervall)
            {
                ship2CooldownTimer = 0;
                ship2inAction = false;
            }
        }

        if (ship3inAction == true)
        {
            ship3CooldownTimer += Time.deltaTime;
            if (ship3CooldownTimer >= ship0cooldownIntervall)
            {
                ship3CooldownTimer = 0;
                ship3inAction = false;
            }
        }
    }

    void MarkTargetHarbor()
    {

        if (isPassengersLoaded == true && isSliding == true && oHarborIsTouched == true)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                // DebugBox.text = hit.transform.name;

                if (hit.transform.name == "TargetHarbor")
                {
                    targetHarborMarked = true;
                    currentRoute = transform.parent.parent.FindChild("Route").gameObject;
                }
                else if (hit.transform.name == "TargetHarbor (1)")
                {
                    targetHarborMarked = true;
                    currentRoute = transform.parent.parent.FindChild("Route2").gameObject;
                }
                //else if (hit.transform.name == "TargetHarbor")
                //{
                //    targetHarborMarked = true;
                //    currentRoute = transform.parent.parent.FindChild("Route").gameObject;
                //}
            }

            else
            {
                targetHarborMarked = false;
            }
            //    if (Vector2.Distance(myTouch.position, targetHarbor.transform.position) <= 50)
            //{    
            //}
        }
    }
	void Update ()
    {
        if (!isCHarging)
        {
            ShowShipText();
        }
        //if (targetHarborMarked && isPassengersLoaded && iWasTouchedLastUpdate != oHarborIsTouched)
        //{
        //    SpawnShip();
        //    passengersLoaded = 0;
        //    iWasTouchedLastUpdate = false;
        //}

        ShipCoolDown();
        MarkTargetHarbor();

        if (passengersLoaded >= 10)
        {
            isPassengersLoaded = true;
        }

          //  slidedDistance = 0;

        if (isCHarging == true)
        {
            passengers += 1 * Time.deltaTime * 20;
            int passengersInt = (int)Mathf.Clamp(passengers, 0, 100);
            passengersLoaded = passengersInt;

         

            if (ship0inAction == false)
            {
                myText.text = passengersInt.ToString() + " passengers loaded";
            }
            else if (ship0inAction == true && ship2inAction == false)
            {
                myText2.text = passengersInt.ToString() + " passengers loaded";
            }
            else if (ship0inAction == true && ship2inAction == true && ship3inAction == false)
            {
                myText3.text = passengersInt.ToString() + " passengers loaded";
            }
        }

        if (oHarborIsTouched == true && isSliding == false)
        {
            gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0.3f);
            isCHarging = true;
          //  iWasTouchedLastUpdate = true;
        }

        //else if (isSliding == true && oHarborIsTouched == true)
        //{
        //    //timer += Time.deltaTime;
        //    isCHarging = false;
        //}
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
            oHarborIsTouched = false;

            if (targetHarborMarked && isPassengersLoaded && (ship0inAction == false|| ship2inAction == false || ship3inAction == false))
            {
                SpawnShip();
                passengersLoaded = 0;
               // iWasTouchedLastUpdate = false;
            }

            timer = 0;
         
            hasMoved = false;
            isCHarging = false;
            passengers = 0;
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
            isSliding = false;
            initialiseSlide = false;
            
            passengersLoaded = 0;
            targetHarborMarked = false;
            isPassengersLoaded = false;
            //  markOriginHarbor = false;

            myFIngerID = 0;
           
        }

        if (Input.touchCount > 0 )
        {
            if (EventSystem.current.IsPointerOverGameObject(myFIngerID))
            {
               
                    if (Vector2.Distance(myTouch.position,uiPosition) <= 50 )
                {
                    oHarborIsTouched = true;
                //    markOriginHarbor = true;
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
            //if (timer <= 0.4f)
            //{
            //    endSLidePoint = myTouch.position;
            //    slidedDistance = Vector3.Distance(startSLidePoint, endSLidePoint);
            //}
        }
    }

}
