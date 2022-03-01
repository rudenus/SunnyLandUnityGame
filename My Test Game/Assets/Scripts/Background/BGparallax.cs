using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGparallax : MonoBehaviour
{
    Transform following;
    float parollaxStreng = 0.8f;
    Vector2 startPos;
    Vector2 dist;
    private void Start()
    {
        startPos = transform.position;
        if (!following)
            following = GameObject.Find("Main Camera").transform;
    }
    private void Update()
    {
        dist = new Vector2(
        following.position.x * parollaxStreng,
        following.position.y * parollaxStreng);
        transform.position = new Vector2(dist.x+startPos.x, dist.y + startPos.y);//сдвиг фона в parollaxStreng раз меньше сдвига камеры
    }
}
