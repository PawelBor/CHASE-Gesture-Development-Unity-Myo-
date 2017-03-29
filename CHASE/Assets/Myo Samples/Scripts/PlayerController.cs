using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;
using System;
using UnityEngine.SceneManagement;
using System.Globalization;

public class PlayerController : MonoBehaviour
{
    public JointOrientation JointObject = null;

    public GameObject myo = null;
    
    private bool sameMovement = false;
    private Pose _lastPose = Pose.Unknown;
    private Rigidbody rb;
    public float speed;
    public float xGap;
    public float yGap;
    public int countVictory;
    public Text countText;
    public Text timerText;

    public Text pointsText;
    public Text endTime;

    Camera MainCamera;
    Camera LSideCamera;
    Camera RSideCamera;
    Camera FrontCamera;

    private float startTime;
    private int count;

    public SwitchView switchView;
    public HighScoreManager hsm;
    public Canvas ScoreboardCanvas;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        startTime = 0f;
        SetCountText();
        timerText.text = "";
        
        ScoreboardCanvas.enabled = false;
    }

    void FixedUpdate()
    {
        ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>();
        //LoadOnClick load = GetComponent<LoadOnClick>();
        // Check if the pose has changed since last update.
        // The ThalmicMyo component of a Myo g
        //ame object has a pose property that is set to the
        // currently detected pose (e.g. Pose.Fist for the user making a fist). If no pose is currently
        // detected, pose will be set to Pose.Rest. If pose detection is unavailable, e.g. because Myo
        // is not on a user's arm, pose will be set to Pose.Unknown.
        if (thalmicMyo.pose != _lastPose)
        {
            _lastPose = thalmicMyo.pose;
            if (thalmicMyo.pose == Pose.WaveOut)
            {
                sameMovement = true;
            }
            else if(thalmicMyo.pose == Pose.WaveIn)
            {
                switchView.ChangeCam();
            }
            /*else if (thalmicMyo.pose == Pose.DoubleTap)
            {
                SceneManager.LoadScene("menu");
            }*/
        }
        else
        {
            sameMovement = false;
        }



        var JointObject = GameObject.Find("Stick");
        float x = JointObject.transform.rotation.eulerAngles.x;
        float y = JointObject.transform.rotation.eulerAngles.y;
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        float moveUp = 0;

        if (GameObject.FindGameObjectWithTag("LSideCamera").GetComponent<Camera>().enabled == true)
        {
            if (0 + xGap < x && x < 180)
            {
                moveVertical = -1;
            }
            else if (180 < x && x < 360 - xGap)
            {
                moveVertical = 1;
            }
            if (0 + yGap < y && y < 180)
            {
                moveHorizontal = 1;
            }
            else if (180 < y && y < 360 - yGap)
            {
                moveHorizontal = -1;
            }
            if (sameMovement)
            {
                moveUp = 30;
            }

            Vector3 movement = new Vector3(moveHorizontal, moveUp, moveVertical);

            rb.AddForce(movement * speed);
            if (transform.position.y < -100)
            {
                movement = new Vector3(0, 0, 0);
            }
        }

        if (GameObject.FindGameObjectWithTag("FrontCamera").GetComponent<Camera>().enabled == true)
        {
            if (0 + xGap < x && x < 180)
            {
                moveHorizontal = -1;
            }
            else if (180 < x && x < 360 - xGap)
            {
                moveHorizontal = 1;
            }
            if (0 + yGap < y && y < 180)
            {
                moveVertical = -1;
            }
            else if (180 < y && y < 360 - yGap)
            {
                moveVertical = 1;
            }
            if (sameMovement)
            {
                moveUp = 30;
            }

            Vector3 movement = new Vector3(moveHorizontal, moveUp, moveVertical);

            rb.AddForce(movement * speed);
            if (transform.position.y < -100)
            {
                movement = new Vector3(0, 0, 0);
            }
        }

        if (GameObject.FindGameObjectWithTag("RSideCamera").GetComponent<Camera>().enabled == true)
        {
            if (0 + xGap < x && x < 180)
            {
                moveVertical = 1;
            }
            else if (180 < x && x < 360 - xGap)
            {
                moveVertical = -1;
            }
            if (0 + yGap < y && y < 180)
            {
                moveHorizontal = -1;
            }
            else if (180 < y && y < 360 - yGap)
            {
                moveHorizontal = 1;
            }
            if (sameMovement)
            {
                moveUp = 30;
            }

            Vector3 movement = new Vector3(moveHorizontal, moveUp, moveVertical);

            rb.AddForce(movement * speed);
            if (transform.position.y < -100)
            {
                movement = new Vector3(0, 0, 0);
            }
        }

        if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled == true)
        {
            if (0 + xGap < x && x < 180)
            {
                moveHorizontal = 1;
            }
            else if (180 < x && x < 360 - xGap)
            {
                moveHorizontal = -1;
            }
            if (0 + yGap < y && y < 180)
            {
                moveVertical = 1;
            }
            else if (180 < y && y < 360 - yGap)
            {
                moveVertical = -1;
            }
            if (sameMovement)
            {
                moveUp = 30;
            }

            Vector3 movement = new Vector3(moveHorizontal, moveUp, moveVertical);

            rb.AddForce(movement * speed);
            if (transform.position.y < -100)
            {
                movement = new Vector3(0, 0, 0);
            }
        }

        if (count < countVictory)
        {
            startTime += Time.deltaTime;
            setTimerText();
        }

        if (transform.position.y < -1)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name.ToString());
        }
    }


    //Function Collider, if game object tag(in Prefabs Unity) == String Collector, set the status of that object to false making it deactivated
   void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collector"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();

        }

        if (other.gameObject.CompareTag("Finisher"))
        {
            Time.timeScale = 0;
            ScoreboardCanvas.enabled = true;
            timerText.enabled = false;
            countText.enabled = false;
            pointsText.text = pointsText.text + "  " + count.ToString();
            endTime.text = endTime.text + "  " + timerText.text.ToString();
            
        }
    }

    public void Save()
    {
        Scene scene = SceneManager.GetActiveScene();
        Double timer = Convert.ToDouble(endTime.text.ToString());
        hsm.InsertScores(scene.name.ToString(), count, timer);
    }

    public void ReplayScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name.ToString());
        Time.timeScale = 1;
    }

    public void MenuScene()
    {
        SceneManager.LoadScene("menu");
        Time.timeScale = 1;
    }


    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }

    void setTimerText()
    {
        timerText.text = Math.Round(startTime, 1, MidpointRounding.AwayFromZero).ToString();
        
    }
    

}
