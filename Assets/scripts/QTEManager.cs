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
    GameObject qteActivate;

    bool markCHoice1 = false;
    bool markCHoice2 = false;
    bool markCHoice3 = false;

    bool qteIsHit = false;

  //  bool intitia
  
    void Start ()
    {
        myCanvas = GameObject.Find("Canvas");
        qTEChoices = myCanvas.transform.FindChild("QTEManager").FindChild("QTECHoices").gameObject;
        qteChoice1 = qTEChoices.transform.GetChild(0).gameObject;
        qteChoice2 = qTEChoices.transform.GetChild(1).gameObject;
        qteChoice3 = qTEChoices.transform.GetChild(2).gameObject;
        qteActivate = myCanvas.transform.FindChild("QTEManager").FindChild("QTEActivate").gameObject;

        //   uiPosition = myCamera.WorldToScreenPoint(gameObject.transform.position);
        DebugBox = GameObject.Find("DebugBox").gameObject.GetComponent<Text>();
    }
	
	 public void StartQTevent()
    {
       
    }

    public void ShowQTEChoices()
    {
        if (qteIsHit == true)
        {
            qTEChoices.SetActive(true);

            if (EventSystem.current.IsPointerOverGameObject(myFIngerID))
            {  
                //DebugBox.text = Vector2.Distance(myTouch.position, qteChoice1.transform.position).ToString();

                if (Vector2.Distance(myTouch.position, qteChoice1.transform.position) <= 75)
                {
                    markCHoice1 = true;
                }
                if (Vector2.Distance(myTouch.position, qteChoice2.transform.position) <= 75)
                {
                    markCHoice2 = true;
                }
                if (Vector2.Distance(myTouch.position, qteChoice3.transform.position) <= 75)
                {
                    markCHoice3 = true;
                }
               
            }
        }

        return;
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
        ShowQTEChoices();

        if (Input.touchCount > 0)
        {
            myTouch = Input.GetTouch(0);
            myFIngerID = myTouch.fingerId;
        }

        else
        {
            oHarborIsTouched = false;
            myFIngerID = 0;
            qteIsHit = false;

            if (markCHoice1 == true)
            {
                if (Vector2.Distance(myTouch.position, qteChoice1.transform.position) <= 75)
                {
                  //  DebugBox.text = "1";
                    markCHoice1 = false;
                }
            }
            if (markCHoice2 == true)
            {
                if (Vector2.Distance(myTouch.position, qteChoice2.transform.position) <= 75)
                {
                  //  DebugBox.text = "2";
                    markCHoice2 = false;
                }
            }
            if (markCHoice3 == true)
            {
                if (Vector2.Distance(myTouch.position, qteChoice3.transform.position) <= 75)
                {
                   // DebugBox.text = "3";
                    markCHoice3 = false;
                }
            }

            qteIsHit = false;
            HideQTEChoices();
        }

        if (Input.touchCount > 0)
        {
            if (EventSystem.current.IsPointerOverGameObject(myFIngerID))
            {

               // DebugBox.text = Vector2.Distance(myTouch.position, qteActivate.transform.position).ToString();

                if (Vector2.Distance(myTouch.position, qteActivate.transform.position) <= 75)
                {
                    qteIsHit = true;
                   
                }

                return;
            }

            else
            {
              
            }
        }
    }
}
