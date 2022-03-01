using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;

public class InputPar : MonoBehaviour
{
    private float inputRun;
    // Start is called before the first frame update
    public float speed;
    public float jumpForce;
    private PlayerClass player;
    public GameObject bullet;
    void Awake()
    {
        player = PlayerClass.GetPlayer(GetComponent<Rigidbody2D>(),GetComponent<Animator>(), speed, jumpForce,bullet);
    }
    void Update()
    {
        inputRun = Input.GetAxis("Horizontal");
        player.Move(inputRun);
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            player.Jump();
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            player.Shoot();
        }
    }
}
