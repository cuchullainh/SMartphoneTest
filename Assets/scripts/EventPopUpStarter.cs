using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EventPopUpStarter : MonoBehaviour {

    GameObject myPopUpEvent;
    Image currentPopUPImage;
    Text currentPopUpText;

	void Start ()
    {
        myPopUpEvent = transform.FindChild("TestEvent").gameObject;
        currentPopUPImage = gameObject.transform.FindChild("TestEvent").GetComponent<Image>();
        currentPopUpText = gameObject.transform.FindChild("TestEvent").FindChild("EventText").GetComponent<Text>();
            
    }
	

	void Update ()
    {
	
	}

    public void StartGameEvent()
    {
        myPopUpEvent.SetActive(true);
    }
    public void DestroyGameEvent()
    {
        myPopUpEvent.SetActive(false);
    }
}
