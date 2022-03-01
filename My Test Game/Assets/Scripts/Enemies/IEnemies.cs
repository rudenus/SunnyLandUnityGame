using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEnemies : MonoBehaviour
{
    public interface IEnemie
    {
        public void PatruleRegion();
        public void KillThis();
    }
}
