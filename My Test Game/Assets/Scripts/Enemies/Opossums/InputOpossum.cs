using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IEnemies;
using static Opossum;

public class InputOpossum : InputParametrs//все классы входных данных €вл€ютс€ наследниками InputParametrs, необходимо дл€ полиморфизма
{
    public float speed;

    private void Awake()//создание в Awake т.к. в Start уже задаетс€ список с объектами
    {
        enemie = new OpossumClass(GetComponent<Rigidbody2D>(), speed);//объект интерфейса IEnemies из родительского класса
    }
}
