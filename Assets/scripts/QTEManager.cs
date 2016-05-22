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
    bool qteWasActivated = false;
    int eventType = 0;
    GameObject shipID;

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
	
	 public void StartQTevent(int eventTpe, GameObject originShip,Vector3 pos)
    {
        // 0 = enemyClose
       
        qteActivate.SetActive(true);
        qTEChoices.SetActive(true);
        //gameObject.transform.position = pos;
        qteWasActivated = true;
        eventType = eventTpe;
        SwitchToEventType();
        shipID = originShip;
    }

    void SwitchToEventType()
    {
        switch (eventType)
        {
            // encounter
            case 0:
                {
                   
                    qteChoice1.SetActive(true);
                    qteChoice2.SetActive(true);
                    qteChoice1.transform.GetChild(0).GetComponent<Text>().text = "über Bord werfen";
                    qteChoice2.transform.GetChild(0).GetComponent<Text>().text = "Bestechung";
                    qteChoice3.SetActive(false);

                    break;
                }

           // storm
            case 1:
                {
                    qteChoice1.SetActive(true);
                   
                    qteChoice1.transform.GetChild(0).GetComponent<Text>().text = "auf See warten";
                    qteChoice2.SetActive(false);
                    qteChoice3.SetActive(false);
                    break;
                }

        }
    }

    public void StopQTEevent()
    {
        qteActivate.SetActive(false);
        qTEChoices.SetActive(false);
        qteWasActivated = false;
    }

    public void ShowQTEChoices()
    {

        if (qteIsHit == true && qteWasActivated == true)
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
                
                    markCHoice1 = false;
                    StopQTEevent();
                    switch (eventType)
                    {

                        // encounter  
                        case 0:
                            {
                                // über bord
                                shipID.GetComponent<ship>().ReducePassengers(50);
                                break;
                            }

                        //storm
                        case 1:
                            {
                                //holdSpeed
                                shipID.GetComponent<ship>().HoldShip();

                                break;
                            }

                    }
                }
            }
            if (markCHoice2 == true)
            {
                if (Vector2.Distance(myTouch.position, qteChoice2.transform.position) <= 75)
                {
               
                    markCHoice2 = false;
                    StopQTEevent();
                    switch (eventType)
                    {
                        // encounter
                        case 0:
                            {
                               //bestechung

                                break;
                            }

                        // storm
                        case 1:
                            {

                                break;
                            }

                    }
                }
            }
            if (markCHoice3 == true)
            {
                if (Vector2.Distance(myTouch.position, qteChoice3.transform.position) <= 75)
                {
                 
                    markCHoice3 = false;
                    StopQTEevent();
                    switch (eventType)
                    {
                        // encounter
                        case 0:
                            {
                                //   qteActivate
                              

                                break;
                            }

                        // storm
                        case 1:
                            {

                                break;
                            }

                    }
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
