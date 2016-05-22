using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShipText : MonoBehaviour {

    Text myText;
    Slider spotSLider;

    void Start()
    {
        myText = transform.FindChild("QTEButton").GetChild(0).GetComponent<Text>();
        myText.text = transform.parent.GetComponent<ship>().PassengersLoaded.ToString();
        spotSLider = transform.FindChild("SpotBar").GetComponent<Slider>();
    }


    void Update()
    {

    }

    public void SetShipText(string aText)
     {
        myText.text = aText;
      }

    public void SetSliderValue(float Value)
    {
       spotSLider.value = Value;
    }

    public void SetSliderActInAct(bool set)
    {
        spotSLider.gameObject.SetActive(set);
    }

}
