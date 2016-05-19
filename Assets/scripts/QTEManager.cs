using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QTEManager : MonoBehaviour {

    Text DebugBox;

    Text myText;
    Touch myTouch;
    int myFIngerID;
    bool oHarborIsTouched = false;
    Vector2 uiPosition;

    GameObject myCanvas;
    GameObject qTEChoices;
    GameObject qteChoice1;
    GameObject qteChoice2;
    GameObject qteChoice3;

  //  bool intitia

  

    void Start ()
    {
        myCanvas = GameObject.Find("Canvas");
        qTEChoices = myCanvas.transform.FindChild("QTEManager").FindChild("QTECHoices").gameObject;
        qteChoice1 = qTEChoices.transform.GetChild(0).gameObject;
        qteChoice2 = qTEChoices.transform.GetChild(1).gameObject;
        qteChoice3 = qTEChoices.transform.GetChild(2).gameObject;

        //   uiPosition = myCamera.WorldToScreenPoint(gameObject.transform.position);
        DebugBox = GameObject.Find("DebugBox").gameObject.GetComponent<Text>();
    }
	
	 public void StartQTevent()
    {
       
    }

    public void ShowQTEChoices()
    {
       
        qTEChoices.SetActive(true);
    }

    public void HideQTEChoices()
    {
        qTEChoices.SetActive(false);
    }

    void Update ()
    {
	
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
            myFIngerID = 0;
           
        }

        if (Input.touchCount > 0)
        {
            if (EventSystem.current.IsPointerOverGameObject(myFIngerID))
            {

                ShowQTEChoices();
              


                //if (Vector2.Distance(myTouch.position, uiPosition) <= 50)
                //{
                //    
                //}

                return;
            }

            else
            {
              //  HideQTEChoices();
            }
        }


      
    }
}
