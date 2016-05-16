using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour {

    GameObject Cameras;
    GameObject globalCam;
    GameObject leftCam;
    GameObject midCam;
    GameObject rightCam;
    GameObject CurrentCam;
    GameObject toGLobalCanvas;
    GameObject toLocalCanvas;

    Text DebugBox;
    bool isSwitching = false;
    float switchTimer = 0;
    int targetCam = 0; //0= global ,1 = left 2= mid 3 =right

    void Start ()
    {
        Cameras = GameObject.Find("Cameras");
        globalCam = Cameras.transform.FindChild("CameraPosGLobal").gameObject;
        leftCam = Cameras.transform.FindChild("CameraPosLeft").gameObject;
        midCam = Cameras.transform.FindChild("CameraPosMid").gameObject;
        rightCam = Cameras.transform.FindChild("CameraPosRight").gameObject;
        toGLobalCanvas = GameObject.Find("GLobal");
        toLocalCanvas = Cameras.transform.FindChild("SwitchButtonsCanvasTOPoint").FindChild("CamSwitchTrigger").gameObject;

        transform.position = globalCam.transform.position;
        GetComponent<Camera>().orthographicSize = globalCam.GetComponent<Camera>().orthographicSize;
        CurrentCam = globalCam;

        DebugBox = GameObject.Find("DebugBox").gameObject.GetComponent<Text>();

    }

    void Update ()
    {
        SwitchingCam();

    }

    void SwitchingCam()
    {
        if (isSwitching == true)
        {
            switch (targetCam)
                {
                case 0:
                    toGLobalCanvas.SetActive(false);
                    toLocalCanvas.SetActive(true);
                    transform.position = Vector3.Lerp(CurrentCam.transform.position,globalCam.transform.position,switchTimer);
                    GetComponent<Camera>().orthographicSize = Mathf.Lerp(CurrentCam.GetComponent<Camera>().orthographicSize, globalCam.GetComponent<Camera>().orthographicSize, switchTimer);
                    break;

                case 1:
                    toGLobalCanvas.SetActive(true);
                    toLocalCanvas.SetActive(false);
                    transform.position = Vector3.Lerp(CurrentCam.transform.position, leftCam.transform.position, switchTimer);
                    GetComponent<Camera>().orthographicSize = Mathf.Lerp(CurrentCam.GetComponent<Camera>().orthographicSize, leftCam.GetComponent<Camera>().orthographicSize, switchTimer);
                    break;

                case 2:
                    toGLobalCanvas.SetActive(true);
                    toLocalCanvas.SetActive(false);
                    transform.position = Vector3.Lerp(CurrentCam.transform.position, midCam.transform.position, switchTimer);
                    GetComponent<Camera>().orthographicSize = Mathf.Lerp(CurrentCam.GetComponent<Camera>().orthographicSize, midCam.GetComponent<Camera>().orthographicSize, switchTimer);
                    break;

                case 3:
                    toGLobalCanvas.SetActive(true);
                    toLocalCanvas.SetActive(false);
                    transform.position = Vector3.Lerp(CurrentCam.transform.position, rightCam.transform.position, switchTimer);
                    GetComponent<Camera>().orthographicSize = Mathf.Lerp(CurrentCam.GetComponent<Camera>().orthographicSize, rightCam.GetComponent<Camera>().orthographicSize, switchTimer);
                    break;

            }
            switchTimer += Time.deltaTime;
            if (switchTimer >= 1)
            {
                switch (targetCam)
                {
                    case 0:
                        CurrentCam = globalCam;
                        break;

                    case 1:
                        CurrentCam = leftCam;
                        break;

                    case 2:
                        CurrentCam = midCam;
                        break;

                    case 3:
                        CurrentCam = rightCam;
                        break;
                }
                        isSwitching = false;
                switchTimer = 0;

            }
        }

    }
   

    public void CameraSwitchGLobal()
    {
        //transform.position = globalCam.transform.position;
        // GetComponent<Camera>().orthographicSize = globalCam.GetComponent<Camera>().orthographicSize;
        //CurrentCam = globalCam;
        targetCam = 0;
        switchTimer = 0;
        isSwitching = true;
    }

    public void CameraSwitchLeft()
    {
        //transform.position = leftCam.transform.position;
        // GetComponent<Camera>().orthographicSize = leftCam.GetComponent<Camera>().orthographicSize;
       // CurrentCam = leftCam;
        targetCam = 1;
        switchTimer = 0;
        isSwitching = true;
    }

    public void CameraSwitchMid()
    {
        //transform.position = midCam.transform.position;
        //GetComponent<Camera>().orthographicSize = midCamp.GetComponent<Camera>().orthographicSize;
       // CurrentCam = midCam;
        targetCam = 2;
        switchTimer = 0;
        isSwitching = true;
    }

    public void CameraSwitchRight()
    {
        //transform.position = rightCam.transform.position;
        //GetComponent<Camera>().orthographicSize = rightCam.GetComponent<Camera>().orthographicSize;
       // CurrentCam = rightCam;
        targetCam = 3;
        switchTimer = 0;
        isSwitching = true;
    }
}
