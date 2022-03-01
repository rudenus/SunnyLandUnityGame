using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IEnemies;

public class InputParametrs : MonoBehaviour
{
    public IEnemie enemie;//������ �� ����������, ����������� ����������� � ListEnemie
    public static Type GetInheritParametr(Transform trans)//������� ��� ���������� � ���������� ������������� �� inputParametrs
    {
        Type type = null;
        foreach (var component in trans.GetComponents<Component>())
        {
            if (component is InputParametrs && component.GetType() != typeof(InputParametrs))
            {
                type = component.GetType();
            }
        }
        return type;
    } 
}
