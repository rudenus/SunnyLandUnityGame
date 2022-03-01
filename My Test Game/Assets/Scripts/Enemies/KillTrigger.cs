using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;

public class KillTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            PlayerClass.GetPlayer().Jump();
            PlayerClass.GetPlayer().KillEnemie();
            Type type = InputParametrs.GetInheritParametr(transform.parent);//узнаем тип наследника InputParametrs
            if (type != null)
            {
                var inputPar = transform.parent.GetComponent(type.Name);//и берем компонент этого типа
                (inputPar as InputParametrs).enemie.KillThis();
            }
        }
    }
}
