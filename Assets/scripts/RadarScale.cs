using UnityEngine;
using System.Collections;

public class RadarScale : MonoBehaviour {

    float min = 0;
    float max = 0.75f;
    float scale = 0;
    float timer = 0;
    float intervall = 1f;

	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;
        if (timer >= intervall)
        {
            timer = 0;
        }
        scale = Mathf.Lerp(min,max,timer);
        gameObject.transform.localScale = new Vector3(scale, scale, scale);
	}
}
