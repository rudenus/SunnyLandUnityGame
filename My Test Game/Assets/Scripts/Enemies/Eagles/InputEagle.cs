using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Eagle;

public class InputEagle : InputParametrs
{
    // Start is called before the first frame update
    
    public float speed;
    public float arealDiameter;
    private void Awake()
    {
        enemie = new EagleClass(GetComponent<Rigidbody2D>(), speed, arealDiameter);
    }
}
