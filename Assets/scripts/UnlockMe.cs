using UnityEngine;
using System.Collections;

public class UnlockMe : MonoBehaviour {

    
    int costToUnlock = 10000;
    GameManager myManager;
   

    void Start ()
    {
        myManager = GameObject.Find("GameManager").GetComponent<GameManager>();
      
    }
	

	void Update ()
    {
	
	}

    public void Unlock()
    {
      
        if (myManager.Money > costToUnlock)
        {
            transform.FindChild("Unlocked").gameObject.SetActive(true);
            transform.FindChild("Locked").gameObject.SetActive(false);
            myManager.spentMoney(costToUnlock);
           
        }
    }
}
