using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat 
{
    //public Combat combat;
    public float playerDmg;
    public float enemyDmg;
    public float bossDmg;
    public float playerHP = 100;
    public int maxHP;
    public int shield;
    public int maxShield;
    public void takeDmg(float hp, float atk, float shield)
    {
        hp = atk - shield - hp;
    }

}
