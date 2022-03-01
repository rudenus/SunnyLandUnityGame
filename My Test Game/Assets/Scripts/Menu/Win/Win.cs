using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.HasKey("scores"))
        {
            string scores = PlayerPrefs.GetInt("scores").ToString();
            GameObject.Find("TextScoreInt").GetComponent<TextMeshProUGUI>().text = scores;
        }
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }
    public void Menu()
    {
        SceneManager.LoadScene(1);
    }
}
