using UnityEngine;
using System.Collections;

public class EnemyShip : MonoBehaviour {

    int nexWayPoint;
    GameObject myParentPath;
    Vector3 despawnPoint;
    float speed = 1;
    int childItems;
    bool backWard = false;


    void Start ()
    {
	
	}
	

	void Update ()
    {
        if (backWard == false)
        {
            if (Vector3.Distance(transform.position, myParentPath.transform.GetChild(nexWayPoint).position) <= 0.1f)
            {
                if (nexWayPoint < childItems - 1)
                    nexWayPoint++;
            }
            if (Vector3.Distance(transform.position, despawnPoint) <= 0.1f)
            {
                Destroy(gameObject);
            }

            transform.LookAt(myParentPath.transform.GetChild(nexWayPoint).position);
            transform.Translate(0, 0, speed * Time.deltaTime);
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
                Destroy(gameObject);
            }

            transform.LookAt(myParentPath.transform.GetChild(nexWayPoint).position);
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
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

}



  

       
       
	

   

