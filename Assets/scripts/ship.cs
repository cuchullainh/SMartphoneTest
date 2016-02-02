using UnityEngine;
using System.Collections;

public class ship : MonoBehaviour {
    int nexWayPoint = 0;
    GameObject myParentPath;
	void Start ()
    {
	
	}
	
	
	void Update ()
    {
        if (Vector3.Distance(transform.position, myParentPath.transform.GetChild(nexWayPoint).position) <= 0.1f)
        {
            nexWayPoint++;
        }
        transform.LookAt(myParentPath.transform.GetChild(nexWayPoint).position);
        transform.Translate(0,0, 0.3f * Time.deltaTime);
	}

    public void SetPath(GameObject parent)
    {
        myParentPath = parent;
    }
}
