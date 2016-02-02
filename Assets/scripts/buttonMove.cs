using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class buttonMove : MonoBehaviour {

    EventSystem eventSystem = EventSystem.current;

    Touch myTouch;
    int myFIngerID;
    bool iWasTouched = false;
    bool colorSwitch = false;
    bool hasMoved = false;

    void Start ()
    {

        gameObject.GetComponent<Image>().color = new Color(0, 0, 0);
    }
	

	void Update ()
    {
      
        if (Input.touchCount > 0)
        {
            myTouch = Input.GetTouch(0);
            myFIngerID = myTouch.fingerId;
        }

        else
        {
            iWasTouched = false;
            hasMoved = false;
        }

        if (EventSystem.current.IsPointerOverGameObject(myFIngerID) && Input.touchCount > 0)
        {
          
            iWasTouched = true;
          
            return;
        }

        gameObject.transform.GetChild(0).GetComponent<Text>().text = iWasTouched.ToString();

        if (iWasTouched == true && myTouch.phase == TouchPhase.Moved)
        {
            hasMoved = true;
            gameObject.transform.position = myTouch.position;

        }

    }


    public void IWasTouched()
    {
        if (hasMoved == false)
        {
            if (colorSwitch == false)
            {
                gameObject.GetComponent<Image>().color = new Color(1, 1, 1);
                colorSwitch = true;
            }
            else
            {
                gameObject.GetComponent<Image>().color = new Color(0, 0, 0);
                colorSwitch = false;
            }
        }
    }
}
