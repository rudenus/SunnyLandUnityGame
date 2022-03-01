using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IEnemies;
using static Opossum;

public class InputOpossum : InputParametrs//��� ������ ������� ������ �������� ������������ InputParametrs, ���������� ��� ������������
{
    public float speed;

    private void Awake()//�������� � Awake �.�. � Start ��� �������� ������ � ���������
    {
        enemie = new OpossumClass(GetComponent<Rigidbody2D>(), speed);//������ ���������� IEnemies �� ������������� ������
    }
}
