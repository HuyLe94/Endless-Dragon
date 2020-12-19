using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat 
{
    //public Combat combat;
    public float playerDmg = 10;
    public float enemyDmg = 10;
    public float bossDmg;
    public float playerHP = 100;
    public int maxHP = 100;
    public int shield = 0;
    public int maxShield = 30;
    public void takeDmg(float hp, float atk, float shield)
    {
        hp = atk - shield - hp;
    }

}
