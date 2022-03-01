using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;

public class BulletRun : MonoBehaviour
{
    private bool faceRight;
    public float speedBullet;
    private Rigidbody2D rb;
    void Start()
    {
        faceRight = PlayerClass.GetPlayer().isFaceRight();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (faceRight)
        {
            rb.velocity = new Vector2(speedBullet, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-1*speedBullet, rb.velocity.y);
        }
        if(transform.position.x<-100 || transform.position.x>100 || transform.position.y<-100 || transform.position.y > 100)
        {
            Destroy(gameObject);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bonus" || collision.gameObject.tag == "Untagged"))//объекты сквозь которые пролетает
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Enemies")
        {
            Type type = InputParametrs.GetInheritParametr(collision.transform);//получаем тип класса наследника inputParametrs
            if(type == null)
            {
                return;
            }
            InputParametrs inputPar = collision.GetComponent(type.Name) as InputParametrs;
            PlayerClass.GetPlayer().KillEnemie();
            inputPar.enemie.KillThis();
        }
    }
}
