using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EventPopUpStarter : MonoBehaviour {

    GameObject myPopUpEvent;
    Image currentPopUPImage;
    Text currentPopUpText;
    int currEvent = 0;
    GameObject eventEffects;
    bool eventCurrActive = false;

    public int CurrEvent
    {
        get
        {
            return currEvent;
        }
    }

    public bool EventCurrActive
    {
        get
        {
            return eventCurrActive;
        }

    }

    public delegate void ingameEvent(int a,bool b);
    public static event ingameEvent gameEventStarted;

    public delegate void ingameEventEnd();
    public static event ingameEventEnd gameEventEnded;

    void Start ()
    {
        myPopUpEvent = transform.FindChild("TestEvent").gameObject;
        currentPopUPImage = gameObject.transform.FindChild("TestEvent").GetComponent<Image>();
        currentPopUpText = gameObject.transform.FindChild("TestEvent").FindChild("EventText").GetComponent<Text>();
        eventEffects = GameObject.Find("EventEffects");

        InvokeRepeating("BroadcastEventStatus", 1, 0.5f);
    }
  

	void Update ()
    {
       
	}

    public void BroadcastEventStatus()
    {
        if (gameEventStarted != null)
        {
            gameEventStarted(currEvent,eventCurrActive);
        }
    }

    public void StartGameEvent()
    {
       float rng = Random.Range(0, 200) / 100;
        int irng = Mathf.RoundToInt(rng);
        currEvent = irng;
        eventCurrActive = true;

        if (gameEventStarted != null)
        {
            gameEventStarted(irng,eventCurrActive);
        }

        switch (irng)
        {
            case 0:
                //storm
                currentPopUPImage.sprite = Resources.Load<Sprite>("rainPic");
                currentPopUPImage.color = new Color(1, 1, 1, 0.5f);
                currentPopUpText.text = "A Storm is coming, drown chances are tripled, speed is halved, enemy sight radius is halved";
                eventEffects.transform.FindChild("Storm").gameObject.SetActive(true);
                eventEffects.transform.FindChild("NormalWeather").gameObject.SetActive(false);

                break;

            case 1:
                //sunny
                eventEffects.transform.FindChild("SunShine").gameObject.SetActive(true);
                eventEffects.transform.FindChild("NormalWeather").gameObject.SetActive(false);

                break;
        }

        myPopUpEvent.SetActive(true);
    }
    public void DestroyGameEvent()
    {
        if (gameEventEnded != null)
        {
            gameEventEnded();
        }

        myPopUpEvent.SetActive(false);
       
        eventCurrActive = false;
        eventEffects.transform.FindChild("NormalWeather").gameObject.SetActive(true);

        switch (currEvent)
        {
            case 0:
                //storm

                eventEffects.transform.FindChild("Storm").gameObject.SetActive(false);

                break;

            case 1:
                //sunny
                eventEffects.transform.FindChild("SunShine").gameObject.SetActive(false);

                break;
        }
    }
}
