using UnityEngine;
using System.Collections;

public class introManager : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        print(Handheld.PlayFullScreenMovie("introMov.mov", Color.black, FullScreenMovieControlMode.Minimal, FullScreenMovieScalingMode.AspectFill));

//        Handheld.PlayFullScreenMovie("introMov.mov", Color.black, FullScreenMovieControlMode.Minimal, FullScreenMovieScalingMode.AspectFill);
	}
	
	// Update is called once per frame
	void Update ()
    {
  //      print(Handheld.PlayFullScreenMovie("introMov.mov", Color.black, FullScreenMovieControlMode.Minimal, FullScreenMovieScalingMode.AspectFill);
	
	}
}
