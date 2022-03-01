using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healths : MonoBehaviour
{
    public Texture2D healthsRed;
    public Texture2D healthsBlack;
    public void ImageChange()
    {
        Transform[] temp = GetComponentsInChildren<Transform>();
        List<Transform> childs = new List<Transform>(temp);
        childs.RemoveAt(0);
        childs.Reverse();
        foreach (Transform child in childs)
        {
            if (child.GetComponent<Image>().mainTexture.name == "healthRed")//смена картинки при получение урона игроком
            {
                Sprite sprite = Sprite.Create(healthsBlack,new Rect(child.GetComponent<Image>().sprite.rect),Vector2.zero);
                child.GetComponent<Image>().sprite = sprite;
                return;
            }
        }
    }


}
