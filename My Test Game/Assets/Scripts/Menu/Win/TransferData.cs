using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TransferData : MonoBehaviour
{
    public GameObject Top10;
    public GameObject MenuPlayAgain;
    public void SaveScore()
    {
        if (!PlayerPrefs.HasKey("scores")) return;
        int score = PlayerPrefs.GetInt("scores");
        string name = GameObject.Find("InputFieldName").GetComponent<TMP_InputField>().text;
        if (name == "") return;
        if(ScoresList.SaveData(name, score))
        {
            MenuPlayAgain.SetActive(true);
            Top10.SetActive(false);
        }
       
    }
    private void Start()
    {
        if (!PlayerPrefs.HasKey("scores"))
        {
            return;
        }
        int scores = PlayerPrefs.GetInt("scores");
        Dictionary<string, int> top10 = ScoresList.GetTop10();//получаем 10 лучших игроков из списка
        int badScore = 0;
        if (top10.Count()>=10)
        {
            badScore = top10.Last().Value;
        }
        if (badScore <= scores)
        {
            MenuPlayAgain.SetActive(false);
            Top10.SetActive(true);
        }
        else
        {
            MenuPlayAgain.SetActive(true);
            Top10.SetActive(false);
        }
    }
}
