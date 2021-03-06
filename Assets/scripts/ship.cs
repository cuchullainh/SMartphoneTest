﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ship : MonoBehaviour {

    int nexWayPoint = 0;
    GameObject myParentPath;
    int childItems;
    Vector3 despawnPoint;
    int passengersLoaded;
    GameManager myManager;
 
    float speed;
    int awarenessPerDeath;
    int shipRefund = 0;
    float greenWeight = 1;
    float yellowWeight = 0.6f;
    float redWeight = 0.2f;
    float yellowThreshold = 50;
    float redThreshold = 75;
    float calcSpeed = 0;
    float originSpeed = 0;
    Rigidbody2D myRig;

    Canvas shipCanvas;

    bool oneTimeSet = false;

    float spottedValue = 0;
    float maxSpottedValue = 100;
    bool spotted = false;
    float spotLastFrame = 0;
  


    float drownPercentage;
    float defautDrownPercentage;

    float drownTimer = 0;
    float drownCheckInterval = 2;


    Text debugFIeld;

    float abnutzung = 0;
    GameObject originHarbor = null;

    float dmgTimer = 0;
    float dmgInterval = 5;

    //AbnutzungManager myDmgManager;

    int imShipNumber = 0;

    bool enemyEncounterRunning = false;

    QTEManager qteManager;

    Vector3 uiPos;

    bool oneTImeEvent = false;
    bool canThrowEvent = false;

    bool shipIsHold = false;

    //  GameObject myTexture;
    float rotTimer = 0;

    void ResetSpotCheck()
    {
        if (spottedValue == 0)
        {
            shipCanvas.GetComponent<ShipText>().SetSliderActInAct(false);
            spotLastFrame = 0;
        }
    }

    void StartEnemyEncounterEvent()
    {
        if (spotLastFrame != spottedValue && (enemyEncounterRunning == false))
        {
            shipCanvas.GetComponent<ShipText>().SetSliderActInAct(true);
            enemyEncounterRunning = true;
            Vector2 uiPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            debugFIeld.text = uiPos.ToString();
            qteManager.StartQTevent(0,gameObject,uiPos);
        }
    }

    void DisableEnemyEncounterEvent()
    {
        enemyEncounterRunning = false;
    }

    public void setShipNumber(int shpnmbr)
    {
        imShipNumber = shpnmbr;
    }

    void AbntzngTimer()
    {
        dmgTimer += Time.deltaTime;
        
        if (dmgTimer >= dmgInterval)
        {
            
            dmgTimer = 0;
            
              float[] tempArray = new float[2];
             tempArray = CalcDmg(passengersLoaded, abnutzung, 100);
            
            ReducePassengers(Mathf.RoundToInt(tempArray[0]));
              abnutzung += Mathf.Clamp(tempArray[1],0,100);
            SendBackAbnutzung(imShipNumber);

            //debugFIeld.text = abnutzung.ToString();
        }
    }

    public void SetAbnutzung(float abn)
    {
        abnutzung = abn;
    }

    public void SetOriginHarbor(GameObject orgnharb)
    {
        originHarbor = orgnharb;
    }

    void OnEnable()
    {
        EventPopUpStarter.gameEventStarted += eventActive;
        EventPopUpStarter.gameEventEnded += SetToDefaultParameters;
    }

    void OnDisable()
    {
        EventPopUpStarter.gameEventStarted -= eventActive;
        EventPopUpStarter.gameEventEnded -= SetToDefaultParameters;
    }

    void SetToDefaultParameters()
    {
        drownPercentage = defautDrownPercentage;
        SetDefaultSpeed(originSpeed);
    }

    public void HoldShip()
    {
        shipIsHold = true;
    }

    void eventActive(int currEvent,bool eventActive)
    {
        if (eventActive == false)
        {
            oneTImeEvent = false;
            shipIsHold = false;
        }
        if (eventActive == true)
        {
            if (oneTImeEvent == false)
            {
                canThrowEvent = true;
                oneTImeEvent = true;
            }

            switch (currEvent)
            {
                case 0://storm
                       // drownPercentage = drownPercentage * 2;

                    if (shipIsHold == true)
                    {
                        SetTempSpeedCoEff(0f);
                    }
                    else
                    {
                        SetTempSpeedCoEff(0.5f);
                    }

                    if (canThrowEvent == true)
                    {
                        qteManager.StartQTevent(1, gameObject, uiPos);
                        canThrowEvent = false;
                    }
                   

                    break;

                case 1://heat/sun

                    break;
            }
        }  
    }

   

    void Drown()
    {
        drownTimer += Time.deltaTime;
        
        if (drownTimer >= drownCheckInterval)
        {
            drownTimer = 0;

            float rng = Random.Range(0, 1000) / 10;
           

            //if (rng <= drownPercentage)
            //{
            //    DestroyMe();
            //}
        }
    }

    public int PassengersLoaded
    {
        get
        {
            return passengersLoaded;
        }

       
    }

    public float SpottedValue
    {
        get
        {
            return spottedValue;
        }

        set
        {
            spottedValue = value;
            spottedValue = Mathf.Clamp(spottedValue, 0, maxSpottedValue);
            shipCanvas.GetComponent<ShipText>().SetSliderValue(spottedValue);
            if (spottedValue == maxSpottedValue)
            {
                DestroyMe();
            }
        }
    }
    //void setSpottedValue()
    //{
    //    shipCanvas.GetComponent<ShipText>().SetSliderValue(spottedValue);
    //}
     
    void Start ()
    { 
        myManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        awarenessPerDeath = (int)myManager.AwarenessPerPassengerDead;
        shipCanvas = transform.GetChild(0).GetComponent<Canvas>();
    //    myTexture = gameObject.transform.GetChild(1).gameObject;
        qteManager = GameObject.Find("Canvas").transform.FindChild("QTEManager").GetComponent<QTEManager>();

        shipCanvas.transform.eulerAngles = new Vector3(0, 0, 0);

        debugFIeld = GameObject.Find("DebugBox").GetComponent<Text>();
        SpottedValue = spottedValue;

      //  GameObject go2 = (GameObject)Instantiate(Resources.Load("MoneyIncome"), transform.position, Quaternion.identity);
     //   go2.GetComponent<MoneyIncome>().setText((1000).ToString());



    }

    void SendBackAbnutzung(int nmr)
    {
        if (imShipNumber == 1)
        {
            originHarbor.GetComponent<TouchAreaHarbor>().setAbnutzung0(abnutzung);
        }
        else if (imShipNumber == 2)
        {
            originHarbor.GetComponent<TouchAreaHarbor>().setAbnutzung2(abnutzung);
        }
        else if (imShipNumber == 3)
        {
            originHarbor.GetComponent<TouchAreaHarbor>().setAbnutzung3(abnutzung);
        }
    }
	
	void Update ()
    {
        StartEnemyEncounterEvent();
        ResetSpotCheck();
        // setSpottedValue();
        AbntzngTimer();

        if (oneTimeSet == false)
        {
            defautDrownPercentage = passengersLoaded / 20;
            drownPercentage = defautDrownPercentage;
            shipCanvas.transform.SetParent(null);
          //  myTexture.transform.SetParent(null);
            oneTimeSet = true;
        }

       // Drown();
  
        shipCanvas.transform.position = gameObject.transform.position;
     //   myTexture.transform.position = gameObject.transform.position;

        if (Vector3.Distance(transform.position, myParentPath.transform.GetChild(nexWayPoint).position) <= 0.5f)
        {
            if (nexWayPoint < childItems - 1)
                nexWayPoint++;
        }
        if (Vector3.Distance(transform.position, despawnPoint) <= 0.5f)
        {
            originHarbor.GetComponent<TouchAreaHarbor>().ShipNotUnderway(imShipNumber);
            SendBackAbnutzung(imShipNumber);
            myManager.shipDespawned(gameObject);
            myManager.earnMoney(shipRefund);
           // Destroy(myTexture);
            Destroy(shipCanvas);
            Destroy(gameObject);
        }

        Rotate();
      
        transform.Translate(0,0, speed * Time.deltaTime);
     //    transform.LookAt(myParentPath.transform.GetChild(nexWayPoint).position);
       

    }
    void Rotate()
    {
        rotTimer += Time.deltaTime;
        if (rotTimer >= 1)
        {
            Vector3 targetPoint = myParentPath.transform.GetChild(nexWayPoint).position;
           // targetPoint.z = -1;
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            // targetRotation = Quaternion.Euler(targetRotation.eulerAngles.x, 90, 0);
            // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
            transform.rotation = Quaternion.Euler(Mathf.LerpAngle(transform.rotation.eulerAngles.x, targetRotation.eulerAngles.x,Time.deltaTime), targetRotation.eulerAngles.y ,targetRotation.eulerAngles.z);
           

           //  transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 90, 0);
           
        }
        else
        {
            transform.LookAt(myParentPath.transform.GetChild(nexWayPoint).position);
        }

    }
    public void ReducePassengers(int psngr)
    {
        if (passengersLoaded > 0)
        {
            IncreaseAwareness(psngr);
            passengersLoaded -= psngr;
            passengersLoaded = Mathf.Clamp(passengersLoaded, 0, 1000);
            shipCanvas.GetComponent<ShipText>().SetShipText(passengersLoaded.ToString());
            SetDefaultSpeed(originSpeed);
        }
    }

    public void ShipRefund(int refund)
    {
        shipRefund = refund;
    }
    public void SetPath(GameObject parent)
    {
        myParentPath = parent;
        childItems = myParentPath.transform.childCount;
        despawnPoint = myParentPath.transform.GetChild(childItems - 1).position;
    }

    public void DestroyMe()
    {
        myManager.shipDespawned(gameObject);
        Instantiate(Resources.Load("ShipDownParticle"), transform.position, Quaternion.identity);
        IncreaseAwareness(passengersLoaded);
       // Destroy(myTexture);
        Destroy(shipCanvas);
        Destroy(gameObject);
    }
    public void loadPassengers(int passengers)
    {
        passengersLoaded = passengers;
    }

    public void IncreaseAwareness(int passengers)
    {
        myManager.AddAwareness(passengers * awarenessPerDeath);
    }

    public void SetDefaultSpeed(float newSpeed)
    {  
        originSpeed = newSpeed;
        calcSpeed = originSpeed;
       
        speed = calcSpeed* LoadedColorNSpeed();
    }
    public void SetTempSpeedCoEff(float newSpeed)
    {
        calcSpeed = originSpeed*newSpeed;

        speed = calcSpeed * LoadedColorNSpeed();
    }

    public float LoadedColorNSpeed()
    {
        float speedCoeff = 0;
        if (passengersLoaded < yellowThreshold)
        {
            speedCoeff = greenWeight;
            GetComponent<MeshRenderer>().material.color = Color.green;
        }
        else if (passengersLoaded >= yellowThreshold && passengersLoaded < redThreshold)
        {
            speedCoeff = yellowWeight;
            GetComponent<MeshRenderer>().material.color = Color.yellow;
        }
        else if (passengersLoaded >= redThreshold)
        {
            speedCoeff = redWeight;
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
       
        return speedCoeff;
    }

    public float[] CalcDmg(float pssngrs, float zustnd, float maxpssngers)
    {
        //debugFIeld.text = "hallo";

        float[] returnArray = new float[2];

        float minRoll = 0;
        float maxRoll = 0;

        if (zustnd <= 10)
        {
            minRoll = 1;
            maxRoll = 30;
        }
        else if (zustnd <= 20 && zustnd > 10)
        {
            minRoll = 1;
            maxRoll = 40;
        }
        else if (zustnd <= 30 && zustnd > 20)
        {
            minRoll = 11;
            maxRoll = 60;
        }
        else if (zustnd <= 40 && zustnd > 30)
        {
            minRoll = 21;
            maxRoll = 75;
        }
        else if (zustnd <= 50 && zustnd > 40)
        {
            minRoll = 41;
            maxRoll = 90;
        }
        else if (zustnd <= 60 && zustnd > 50)
        {
            minRoll = 51;
            maxRoll = 100;

        }
        else if (zustnd > 60)
        {
            minRoll = 71;
            maxRoll = 100;
        }

        if (((pssngrs / maxpssngers) > 1) && ((pssngrs / maxpssngers) <= 1.3f))
        {
            if (minRoll < 11)
            {
               // minRoll = 11;
            }
        }
        else if (((pssngrs / maxpssngers) > 1.3f) && ((pssngrs / maxpssngers) <= 1.6f))
        {
            if (minRoll < 31)
            {
               // minRoll = 31;
            }
        }
        else if (((pssngrs / maxpssngers) > 1.6f) && ((pssngrs / maxpssngers) <= 2f))
        {
            if (minRoll < 51)
            {
                //minRoll = 51;
            }
        }

        float roll = Random.Range(minRoll * 100, maxRoll * 100) / 100;

        float minDead = 0;
        float maxDead = 0;

        float zustndLoss = 0;


        if (roll > 0 && roll <= 10)
        {
            minDead = 0;
            maxDead = 0;
            zustndLoss = 0;

        }
        if (roll > 10 && roll <= 20)
        {
            minDead = 1;
            maxDead = 2;
            zustndLoss = 3;

        }

        if (roll > 20 && roll <= 50)
        {
            minDead = 2;
            maxDead = 4;
            zustndLoss = 5;
        }

        if (roll > 50 && roll <= 70)
        {
            minDead = 3;
            maxDead = 9;
            zustndLoss = 7;
        }
        if (roll > 70 && roll <= 90)
        {
            minDead = 5;
            maxDead = 25;
            zustndLoss = 15;
        }
        if (roll > 90 && roll <= 100)
        {
            minDead = 100;
            maxDead = 100;
            zustndLoss = 100;
        }

        float lostRoll = Random.Range(minDead * 100, maxDead * 100) / 100;

        float lostPassengersTotal = (pssngrs / 100) * lostRoll;

        returnArray[0] = Mathf.RoundToInt(lostPassengersTotal);
        returnArray[1] = Mathf.RoundToInt(zustndLoss);

        //  return lostPassengersTotal;
         return returnArray;
    }
}
