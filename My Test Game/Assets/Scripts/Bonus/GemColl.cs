using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;

public class GemColl : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            PlayerClass.GetPlayer().BonusRegister();
        }
    }
}
