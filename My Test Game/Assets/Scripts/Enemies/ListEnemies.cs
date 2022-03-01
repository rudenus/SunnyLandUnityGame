using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IEnemies;

public class ListEnemies : MonoBehaviour
{
    public List<IEnemie> enemies = new List<IEnemie>();
    private void Start()
    {
        Transform[] temp = GetComponentsInChildren<Transform>();//массив всех дочерних объектов объекта Enemies
        List<Transform> childs = new List<Transform>(temp);
        foreach (Transform child in childs)
        {
            Type type = InputParametrs.GetInheritParametr(child);//если в объекте присутствует компенент наследуемый от InputParametr
            if (type!=null)
            {
                var inputPar = child.GetComponent(type.Name);
                enemies.Add((inputPar as InputParametrs).enemie);
            }
        }
    }
    private void FixedUpdate()
    {
        foreach(var enemie in enemies)//это позволяет гибко обращаться со всеми врагами
        {
            enemie.PatruleRegion();
        }
    }
}
