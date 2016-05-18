using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShipText : MonoBehaviour {

    Text myText;
    Slider abnutzungsSlider;

    void Start()
    {
        myText = transform.FindChild("QTEButton").GetChild(0).GetComponent<Text>();
        myText.text = transform.parent.GetComponent<ship>().PassengersLoaded.ToString();
        abnutzungsSlider = transform.FindChild("AbnutzungsSlider").GetComponent<Slider>();
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
       abnutzungsSlider.value = Value;
    }

}
