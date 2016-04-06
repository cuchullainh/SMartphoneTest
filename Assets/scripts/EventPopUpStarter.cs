using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EventPopUpStarter : MonoBehaviour {

    GameObject myPopUpEvent;
    Image currentPopUPImage;
    Text currentPopUpText;
    int currEvent;
    GameObject eventEffects;
   

	void Start ()
    {
        myPopUpEvent = transform.FindChild("TestEvent").gameObject;
        currentPopUPImage = gameObject.transform.FindChild("TestEvent").GetComponent<Image>();
        currentPopUpText = gameObject.transform.FindChild("TestEvent").FindChild("EventText").GetComponent<Text>();
        eventEffects = GameObject.Find("EventEffects");


    }
	

	void Update ()
    {
	
	}

    public void StartGameEvent()
    {
       float rng = Random.Range(0, 100) / 100;
        int irng = Mathf.RoundToInt(rng);
        currEvent = irng;


        switch (irng)
        {
            case 0:
                //storm
                currentPopUPImage.sprite = Resources.Load<Sprite>("rainPic");
                currentPopUPImage.color = new Color(1, 1, 1, 0.5f);
                currentPopUpText.text = "A Storm is coming, drown chances are tripled, speed is halved, enemy sight radius reduced by 30%";
                eventEffects.transform.FindChild("Storm").gameObject.SetActive(true);

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
        eventEffects.transform.FindChild("Storm").gameObject.SetActive(false);

        switch (currEvent)
        {
            case 0:
                //storm
              

                break;

            case 1:
                //sunny

                break;
        }
    }
}
