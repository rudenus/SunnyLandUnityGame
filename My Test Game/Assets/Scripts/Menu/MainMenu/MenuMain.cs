using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMain : MonoBehaviour
{
    public GameObject menu;
    public GameObject scoresMenu;
    public GameObject textPrefab;
    public GameObject parentText;//пустышка для удобства дальнейшей работы
    public void Play()
    {
        SceneManager.LoadScene(0);
    }
    public void Scores()
    {
        menu.SetActive(false);
        scoresMenu.SetActive(true);
    }
    public void ReturnToMenu()
    {
        scoresMenu.SetActive(false);
        menu.SetActive(true);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void LoadData()
    {
        foreach (Transform child in parentText.transform)
        {
            Destroy(child.gameObject);
        }
        float marginLeft = -5;
        float marginUp = -5;
        float marginEnterText = 7;
        Dictionary<string, int> listData = ScoresList.GetTop10();
        if (listData == null) return;
        int y = 9;
        foreach(var data in listData)
        {

            Vector3 pos = new Vector3(marginLeft, y + marginUp,0f);//создание первого textMesh для имени игрока
            GameObject go = Instantiate(textPrefab, pos, Quaternion.identity);
            go.GetComponent<TextMeshProUGUI>().text = data.Key;
            go.transform.SetParent(parentText.transform);
            go.transform.localScale = new Vector3(1f, 1f, 1f);

            pos = new Vector3(marginLeft + marginEnterText, y + marginUp,0f);//создание второго textMesh для очков игрока
            go = Instantiate(textPrefab, pos, Quaternion.identity);
            go.GetComponent<TextMeshProUGUI>().text = data.Value.ToString();
            go.transform.SetParent(parentText.transform);
            go.transform.localScale = new Vector3(1f, 1f, 1f);
            y -= 1;
        }
        return;
    }
}
