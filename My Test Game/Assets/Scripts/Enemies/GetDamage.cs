using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;

public class GetDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
            PlayerClass.GetPlayer().HealthRemove();
    }
}
