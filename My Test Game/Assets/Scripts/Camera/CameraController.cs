using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;

public class CameraController : MonoBehaviour
{
    private Transform playerTr;
    public float speedMove;

    private void Start()
    {
        playerTr = PlayerClass.GetPlayer().GetTransform();
        Vector3 posCam = playerTr.position;
        posCam.z -= 10;
        transform.position = posCam;
    }
    private void FixedUpdate()
    {
        Vector3 target = playerTr.position;
        target.z -= 10;
        transform.position = target;
        if (playerTr)
        {
            transform.position = Vector3.Lerp(transform.position, target, speedMove);//плавное передвижение камеры за игроком
        }
    }
}
