using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Threading;
using UnityEngine.SceneManagement;
using TMPro;
using System.Globalization;

public class GameManager : MonoBehaviour
{
    public GameObject ball; 
    int count = 0;
    int score = 0;
    GameObject[] pins;
    int[] score_array = new int[10];
    int curr_frame=0;
    string filepath;
    bool strike_flag = false;
    bool spare_flag = false;
    Vector3 initial_ball_position;
    Quaternion initial_ball_Rotation;
    Vector3[] initial_pins_position;
    Quaternion[] initial_pins_Rotation;
    private int overall_score = 0,curr_chance = 0,curr_throw = 1;
    public TMP_Text display_score;
    bool increment_frame=false;
    string fp;
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "Ball")
        {
            score = 0;
            count = 0;
            countPinsDown();
            if (curr_throw == 3)
            {
                curr_throw = 1;
            }
            increment_frame = false;
            scoreCalculator(curr_throw);
            if (count == 10)
            {
                curr_throw = 1;
            }
            else
            {
                curr_throw++;
            }
            Reset(curr_throw);
            findTotalScore();
            if (increment_frame)
                curr_frame++;
            if (curr_frame == 10)
            {
                fp= Application.persistentDataPath+"/my_db.txt";
                Debug.Log(fp);
                using (StreamWriter sw = File.AppendText(fp))
                {
                    //string time_date = DateTime.Now.ToShortDateString();
                    string player_score = overall_score.ToString();
                    //string data = time_date + "\t" + player_score;
                    sw.WriteLine(player_score);
                    SceneManager.LoadScene("MainMenu");
                }
            }
        }
    }
    

    void Reset(int num)
    {
        if(num==2 || count==10)
        {
            for (int i = 0; i < pins.Length; i++)
            {
                pins[i].SetActive(true);
                pins[i].transform.position = initial_pins_position[i];
                pins[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
                pins[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                pins[i].transform.rotation = initial_pins_Rotation[i];
            }
        }
        
        //ball = this.gameObject;
        ball.transform.position = initial_ball_position;
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        ball.transform.rotation = initial_ball_Rotation;

    }

    // Start is called before the first frame update
    void Start()
    {
        //ball = this.gameObject;
        initial_ball_position = ball.transform.position;
        initial_ball_Rotation = ball.transform.rotation;
        
        pins = GameObject.FindGameObjectsWithTag("pin");
        initial_pins_position = new Vector3[pins.Length];
        initial_pins_Rotation = new Quaternion[pins.Length];
        for(int i=0;i<pins.Length;i++)
        {
            initial_pins_position[i] = pins[i].transform.position;
            initial_pins_Rotation[i] = pins[i].transform.rotation;
        }
        for (int i = 0; i < score_array.Length; i++)
        {
            score_array[i] = 0;
        }
    }

    // Update is called once per frame
   
    void countPinsDown()
    {
        /*Stopwatch stopwatch = Stopwatch.StartNew();
        while (true)
        {
            //some other processing to do possible
            if (stopwatch.ElapsedMilliseconds >= 2000)
            {
                break;
            }
        }
        */
        //Debug.Log("In count pins down");
        for (int i = 0; i < pins.Length; i++)
        {
            if ((pins[i].transform.eulerAngles.x > 300 || pins[i].transform.eulerAngles.x < 230) && pins[i].activeSelf)
            {
                count++;
                pins[i].SetActive(false);
            }
        }
        //Debug.Log(count);
    }

    void scoreCalculator(int throw_num)
    {
        if (spare_flag)
        {
            score_array[curr_frame - 1] += count;
            spare_flag = false;
        }
        if (strike_flag)
        {
            score_array[curr_frame - 1] += count;
            if (throw_num % 2 == 0)
                strike_flag = false;
        }
        
        if (count == 10)
        {
            strike_flag = true;
            score_array[curr_frame] += count;
            increment_frame = true;
        }
        else
        {
            score_array[curr_frame] += count;
            if (score_array[curr_frame] == 10)
                spare_flag = true;
            if (throw_num % 2 == 0)
            {
                increment_frame = true;
            }
        }
               
    }
    void findTotalScore()
    {
        overall_score = 0;
        for(int i=0;i<=curr_frame;i++)
        {
            overall_score += score_array[i];
        }
        
        display_score.text = overall_score.ToString();
    }
}
