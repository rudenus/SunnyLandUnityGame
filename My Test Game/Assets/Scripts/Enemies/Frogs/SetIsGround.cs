using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Frog;

public class SetIsGround : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Earth" || collision.tag == "Platform")
        {
            FrogClass frog = gameObject.transform.parent.GetComponent<InputFrog>().enemie as FrogClass;
            frog.SetIsGrounded();
        }
    }
}
