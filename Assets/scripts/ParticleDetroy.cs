using UnityEngine;
using System.Collections;

public class ParticleDetroy : MonoBehaviour {


	void Start ()
    {
        Invoke("DestroyMe",3);
	}
	
	
	void Update ()
    {
	
	}

    void DestroyMe()
    {
        Destroy(gameObject);
    }
}
