using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EnemyShip : MonoBehaviour {

    int nexWayPoint;
    GameObject myParentPath;
    Vector3 despawnPoint;
    float speed = 1;
    float baseSpeedKoefficient = 0.1f;
    int childItems;
    bool backWard = false;
    EnemyCOntroller myController;
    List<GameObject> closeShips = new List<GameObject>();
    Text debugFIeld;
    float spotTimer = 0;
    float spotIncBase = 0.025f;
    Rigidbody2D myRig;




    void Start ()
    {
        myRig = GetComponent<Rigidbody2D>();
      //  myRig.WakeUp(); 
        myController = GameObject.Find("EnemyController").gameObject.GetComponent<EnemyCOntroller>();
        debugFIeld = GameObject.Find("DebugBox").GetComponent<Text>();
    }
	

	void Update ()
    {
        spotPlayers();
        if (backWard == false)
        {
            if (Vector3.Distance(transform.position, myParentPath.transform.GetChild(nexWayPoint).position) <= 0.1f)
            {
                if (nexWayPoint < childItems - 1)
                    nexWayPoint++;
            }
            if (Vector3.Distance(transform.position, despawnPoint) <= 0.1f)
            {
                DecreaseEnemyShipCounter();
                Destroy(gameObject);
            }

            transform.LookAt(myParentPath.transform.GetChild(nexWayPoint).position);
         //   transform.Translate(0, 0, speed * baseSpeedKoefficient * Time.deltaTime);
        }
        else
        {
        
            if (Vector3.Distance(transform.position, myParentPath.transform.GetChild(nexWayPoint).position) <= 0.1f)
            {
                if (nexWayPoint > 0)
                {
                    nexWayPoint--;
                }
                   
            }
            if (Vector3.Distance(transform.position, despawnPoint) <= 0.1f)
            {
                DecreaseEnemyShipCounter();
                Destroy(gameObject);
            }

            transform.LookAt(myParentPath.transform.GetChild(nexWayPoint).position);
         //   transform.Translate(0, 0, speed * baseSpeedKoefficient *Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        myRig.MovePosition(transform.position + transform.forward * speed * baseSpeedKoefficient *  Time.deltaTime);
    }
    public void SetPath(GameObject parent)
    {
        myParentPath = parent;
       
    }
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
      
    }
    public void SetDirection(bool newDIrect)
    {
        childItems = myParentPath.transform.childCount;
        backWard = newDIrect;
        if (backWard == false)
        {
            nexWayPoint = 0;
            despawnPoint = myParentPath.transform.GetChild(childItems - 1).position;
         
        }
        else
        {
            nexWayPoint = myParentPath.transform.childCount-1;
            despawnPoint = myParentPath.transform.GetChild(0).position;
          
        }

    }


    void DecreaseEnemyShipCounter()
    {
        myController.ShipCounter--;
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            closeShips.Add(other.gameObject);
          //  debugFIeld.text = closeShips.Count.ToString();
        }

    }
    void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            closeShips.Remove(other.gameObject);
          //  debugFIeld.text = closeShips.Count.ToString();
        }

    }
    void spotPlayers()
    {
       
            spotTimer += Time.deltaTime;
            if (spotTimer >= 0.1f)
            {
                spotTimer = 0;
                
                for (int a = 0; a < closeShips.Count; a++)
                {

                    if (closeShips[a].GetComponent(typeof(ship)))
                    {
                        closeShips[a].GetComponent<ship>().SpottedValue += spotIncBase * (closeShips[a].GetComponent<ship>().PassengersLoaded);
                    }
                    else
                    {
                        closeShips.Remove(closeShips[a]);
                    }
                }
            }
        
    }
}



  

       
       
	

   

