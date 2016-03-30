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
       float rng = Random.Range(0, 100) / 100;
        int irng = Mathf.RoundToInt(rng);
        
        switch (irng)
        {
            case 0:
                //storm

                break;

            case 1:
                //sunny

                break;
        }

        myPopUpEvent.SetActive(true);
    }
    public void DestroyGameEvent()
    {
        myPopUpEvent.SetActive(false);
    }
}
