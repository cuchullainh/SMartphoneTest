using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    Text Highscore;
    Text globalAwareNess;
    float money = 100000;
    float awareness = 0;
    Slider highScoreBar;
    Slider awareNessBar;
    float maxMoney = 1000000;
    float maxAwareNess = 1000;
    float moneyPerPassenger = 100;
    float awarenessPerPassengerDead = 1;

    bool eventIsActive = false;
    float eventTimer = 0;
    float eventPopUpCoolDown = 30;
    float currentEventTimer = 0;
    float currentEventPopUpTime = 10;
    EventPopUpStarter eventStarter;
  

    public float MoneyPerPassenger
    {
        get
        {
            return moneyPerPassenger;
        }

        set
        {
            moneyPerPassenger = value;
        }
    }

    public float AwarenessPerPassengerDead
    {
        get
        {
            return awarenessPerPassengerDead;
        }

        set
        {
            awarenessPerPassengerDead = value;
        }
    }

    public float Awareness
    {
        get
        {
            return awareness;
        }

       
    }

    public float MaxAwareNess
    {
        get
        {
            return maxAwareNess;
        }

        set
        {
            maxAwareNess = value;
        }
    }

    public float Money
    {
        get
        {
            return money;
        }

       
    }

    void Start ()
    {
        
        Highscore = GameObject.Find("HighScore").GetComponent<Text>();
        globalAwareNess = GameObject.Find("GlobalAwareness").GetComponent<Text>();
        Highscore.text = "Money " + money.ToString();
        globalAwareNess.text = "GlobalAwareness " + awareness.ToString();
        highScoreBar = GameObject.Find("HighScoreBar").GetComponent<Slider>();
        awareNessBar = GameObject.Find("GlobalAwareNessBar").GetComponent<Slider>();
        highScoreBar.value = money / maxMoney;
        awareNessBar.value = awareness / maxAwareNess;
        eventStarter = GameObject.Find("EventPopUpField").GetComponent<EventPopUpStarter>();
 

      
    }
	
	
	void Update ()
    {
      
        if (eventIsActive == false)
        {
            eventTimer += Time.deltaTime;
            if (eventTimer > eventPopUpCoolDown)
            {
                eventTimer = 0;
                EventProcc();
            }
        }
        else
        {
            currentEventTimer += Time.deltaTime;
            if (currentEventTimer >= currentEventPopUpTime)
            {
                currentEventTimer = 0;
                eventStarter.DestroyGameEvent();
                eventIsActive = false;
            }
        }
	}

    public void spentMoney(int value)
    {
        money -= value;
        Highscore.text = "Money " + money.ToString();
        highScoreBar.value = money / maxMoney;
    }

    public void earnMoney(int value)
    {
        money += value;
        Highscore.text = "Money " + money.ToString();
        highScoreBar.value = money / maxMoney;
    }
    public void AddAwareness(int value)
    {
        awareness += value;
        globalAwareNess.text = "GlobalAwareness " + awareness.ToString();
        awareNessBar.value = awareness / maxAwareNess;
    }

    public void ReduceAwareness(int value)
    {
        awareness -= value;
        globalAwareNess.text = "GlobalAwareness " + awareness.ToString();
        awareNessBar.value = awareness / maxAwareNess;
    }

    void StartGameEvent()
    {

    }

    void EventProcc()
    {
        float rand = Random.Range(0, MaxAwareNess);
        if (rand < Awareness)
        {
            eventStarter.StartGameEvent();
            eventIsActive = true;
        }

    }
}
