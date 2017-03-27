using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchView : MonoBehaviour {

    [SerializeField]
    Camera MainCamera;
    [SerializeField]
    Camera LSideCamera;
    [SerializeField]
    Camera RSideCamera;
    [SerializeField]
    Camera FrontCamera;
    int i = 1;
    // Use this for initialization
    void Start () {
        MainCamera.GetComponent<Camera>().enabled = true;
        LSideCamera.GetComponent<Camera>().enabled = false;
        RSideCamera.GetComponent<Camera>().enabled = false;
        FrontCamera.GetComponent<Camera>().enabled = false;
    }

    public void ChangeCam()
    {
            i++;
            if (i == 1)
            {
                MainCamera.GetComponent<Camera>().enabled = true;
                LSideCamera.GetComponent<Camera>().enabled = false;
                RSideCamera.GetComponent<Camera>().enabled = false;
                FrontCamera.GetComponent<Camera>().enabled = false;
            }

            if (i == 2)
            {
                MainCamera.GetComponent<Camera>().enabled = false;
                LSideCamera.GetComponent<Camera>().enabled = true;
                RSideCamera.GetComponent<Camera>().enabled = false;
                FrontCamera.GetComponent<Camera>().enabled = false;
            }

            if (i == 3)
            {
                MainCamera.GetComponent<Camera>().enabled = false;
                LSideCamera.GetComponent<Camera>().enabled = false;
                RSideCamera.GetComponent<Camera>().enabled = false;
                FrontCamera.GetComponent<Camera>().enabled = true;
            }

            if (i == 4)
            {
                MainCamera.GetComponent<Camera>().enabled = false;
                LSideCamera.GetComponent<Camera>().enabled = false;
                RSideCamera.GetComponent<Camera>().enabled = true;
                FrontCamera.GetComponent<Camera>().enabled = false;
                i = 0;
            }
            
    }
	
	// Update is called once per frame
	void Update () {
        //ChangeCam();
	}
}
