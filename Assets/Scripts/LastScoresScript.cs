using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Threading;

public class LastScoresScript : MonoBehaviour
{
    public TMP_Text Score_List;
    //public Database my_db;
    private string path;
    // Start is called before the first frame update
    void Start()
    {
        path = Application.persistentDataPath + "/my_db.txt";

        /*string[] final_scores = getLastScores();
        string all_scores = "No.  Date   Score\n";
        int i = 1;
        foreach (string st in final_scores)
        {
            if (st == null)
                break;
            string no = i.ToString();
            string temp_string = st;
            string[] tokens = temp_string.Split();
            int len = tokens.Length;
            all_scores = all_scores + no + "\t" + tokens[0] + "\t" + tokens[1] + "\n";
            i++;
        }*/
        string score_txt = "";
        List<int> scores = new List<int>();
        using (StreamReader sr = File.OpenText(path))
        {
            string score="";
            while ((score = sr.ReadLine()) != null) {
                scores.Add(Int32.Parse(score));
            }

        }
        scores.Reverse();

        List<int> top10 = new List<int>();
        foreach (int score in scores) {
            if (score != 0) {
                top10.Add(score);
            }
            if (top10.Count == 10) break;
        }
        top10.Sort();
        top10.Reverse();

        foreach (int score in top10) 
        {
            if(score!=0)
                score_txt += score+"\n" ;
        }
        Score_List.SetText(score_txt);
        //Thread.Sleep(10000);
        //BackToMainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BackToMainMenu()
    {

        SceneManager.LoadScene("MainMenu");
    }
}
