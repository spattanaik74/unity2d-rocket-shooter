using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] int damage = 100;


    public int Getdamage()
    {
        return damage;
    }
    public void Hit()
    {
        Destroy(gameObject);
    }

}
