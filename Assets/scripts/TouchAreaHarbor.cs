using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchAreaHarbor : MonoBehaviour {

    EventSystem eventSystem = EventSystem.current;
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

    GameManager myManager;
    

    int costPerPassenger = 50;
    int passengersLoaded;

    void Start ()
    {
        myManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        myText = transform.GetChild(0).GetComponent<Text>();
       // InvokeRepeating("SpawnShip", 0, 0.5f);
    }

    void SpawnShip()
    {
     //   myText.text = "ShipStart";
        myManager.spentMoney(costPerPassenger * passengersLoaded);
        GameObject go = (GameObject)Instantiate(Resources.Load("Ship"), transform.parent.parent.FindChild("ShipSpawn").position, Quaternion.identity);
        go.GetComponent<ship>().SetPath(transform.parent.parent.FindChild("Route").gameObject);
        go.GetComponent<ship>().loadPassengers(passengersLoaded);
        go.GetComponent<ship>().setSpeed(slidedDistance);
        passengersLoaded = 0;
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
          
           
        }

        if (EventSystem.current.IsPointerOverGameObject(myFIngerID) && Input.touchCount > 0)
        {

            iWasTouched = true;

            return;
        }
        else
        {

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
