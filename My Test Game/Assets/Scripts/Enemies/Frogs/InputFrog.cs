using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Frog;

public class InputFrog : InputParametrs//аналогично InputOpossum
{
    public float jumpForce;
    public float speed;

    private void Awake()
    {
        enemie = new FrogClass(GetComponent<Rigidbody2D>(),GetComponent<Animator>(), jumpForce, speed);
    }
}
