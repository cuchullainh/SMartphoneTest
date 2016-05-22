using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoneyIncome : MonoBehaviour {

    Text myText;

	void Start ()
    {
        myText = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        Destroy(gameObject,2);
	}
	
	
	void Update ()
    {
        gameObject.transform.Translate(0, 0.75f * Time.deltaTime, 0);
	}

    public void setText(string txt)
    {
        myText.text = txt;

    }
}
