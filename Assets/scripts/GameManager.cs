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

    float DPSTimer = 0;
    int DPS = 500;
    float securityTimer = 0;
    int FPS = 2;

    GameObject frontierSecColor;
 
   
   
    float yellowThreshold = 0.4f;
    float redThreshold = 0.75f;

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
        globalAwareNess.text = "FrontierSecurity " + awareness.ToString();
        highScoreBar = GameObject.Find("HighScoreBar").GetComponent<Slider>();
        awareNessBar = GameObject.Find("GlobalAwareNessBar").GetComponent<Slider>();
        highScoreBar.value = money / maxMoney;
        awareNessBar.value = awareness / maxAwareNess;
        eventStarter = GameObject.Find("EventPopUpField").GetComponent<EventPopUpStarter>();

        frontierSecColor = awareNessBar.transform.FindChild("Fill Area").FindChild("Fill").gameObject;
        frontierSecColor.GetComponent<Image>().color = Color.green;


    }
	
	
	void Update ()
    {
        DollarPerSecond();
        SecurityPerSecond();

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
        if (Money <= 0)
        {
            Application.LoadLevel(1);
        }
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
        globalAwareNess.text = "FrontierSecurity " + awareness.ToString();
        awareNessBar.value = awareness / maxAwareNess;
        AwarenessColor();
        if (Awareness >= MaxAwareNess)
        {
            Application.LoadLevel(1);
        }

    }

    void AwarenessColor()
    {
        if (Awareness/MaxAwareNess < yellowThreshold)
        {

            frontierSecColor.GetComponent<Image>().color = Color.green;
        }
        else if (Awareness / MaxAwareNess >= yellowThreshold && Awareness / MaxAwareNess < redThreshold)
        {

            frontierSecColor.GetComponent<Image>().color = Color.yellow;
        }
        else if (Awareness / MaxAwareNess >= redThreshold)
        {

            frontierSecColor.GetComponent<Image>().color = Color.red;
        }
    }
    public void ReduceAwareness(int value)
    {
        awareness -= value;
        awareness = Mathf.Clamp(awareness, 0, MaxAwareNess);
        globalAwareNess.text = "FrontierSecurity " + awareness.ToString();
        awareNessBar.value = awareness / maxAwareNess;
        AwarenessColor();
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

    void DollarPerSecond()
    {
        DPSTimer += Time.deltaTime;
        if (DPSTimer >= 1)
        {
            DPSTimer = 0;
            spentMoney(DPS);
        }
    }

    void SecurityPerSecond()
    {
        securityTimer += Time.deltaTime;
        if (securityTimer >= 1)
        {
            securityTimer = 0;
            ReduceAwareness(FPS);
        }
    }


}
