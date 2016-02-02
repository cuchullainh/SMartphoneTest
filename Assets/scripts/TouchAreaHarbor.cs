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
    float percent = 0;
    bool isSliding = false;
    Vector3 startSLidePoint;
    Vector3 endSLidePoint;
    bool initialiseSlide = false;
    float timer = 0;
    bool lastPhaseSliding = false;


    void Start ()
    {
        myText = transform.GetChild(0).GetComponent<Text>();
     
    }

    void SpawnShip()
    {
        myText.text = "ShipStart";
        GameObject go = (GameObject)Instantiate(Resources.Load("Ship"), transform.parent.parent.FindChild("ShipSpawn").position, Quaternion.identity);
        go.GetComponent<ship>().SetPath(transform.parent.parent.FindChild("Route").gameObject);
    }
	void Update ()
    {
        if (isSliding != lastPhaseSliding && isSliding == false)
        {
            SpawnShip();
            lastPhaseSliding = false;
           
        }

        if (isCHarging == true)
        {
            percent += 1 * Time.deltaTime * 15;
            int percentInt = (int)Mathf.Clamp(percent, 0, 100);
            myText.text = percentInt.ToString() + " %";
         
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
            percent = 0;
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
            isSliding = false;
            initialiseSlide = false;
        }

        if (EventSystem.current.IsPointerOverGameObject(myFIngerID) && Input.touchCount > 0)
        {

            iWasTouched = true;
            //  gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0.3f);
            return;
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
            if (timer <= 0.5f)
            {
                endSLidePoint = myTouch.position;
                lastPhaseSliding = true;
            }
        }
    }
     
}
