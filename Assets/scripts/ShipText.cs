using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShipText : MonoBehaviour {

    Text myText;

    void Start()
    {
        myText = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        myText.text = transform.parent.GetComponent<ship>().PassengersLoaded.ToString();
    }


    void Update()
    {

    }

    public void SetShipText(string aText)
     {
        myText.text = aText;
      }



}
