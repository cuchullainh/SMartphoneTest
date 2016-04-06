using UnityEngine;
using System.Collections;

public class PlaneOffsetShift : MonoBehaviour {

    Material myMat;
    float a = 0;
    float b = 0;

    public float offSetSPeedA = 2;
    public float offSetSPeedB = 2;
   


    void Start ()
    {
        myMat = GetComponent<Renderer>().material;
      
    }
	
	
	void Update ()
    {
        a += offSetSPeedA * Time.deltaTime;
        b += offSetSPeedB * Time.deltaTime;

        // myMat.mainTextureOffset = new Vector2(a,b);
        myMat.SetTextureOffset("_MainTex", new Vector2(a, b));
    }
}
