using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat 
{
    //public Combat combat;

    public int EnemyStatMultiplierl = 1;
    public void takeDmg(ref int hp, ref int dmg, ref int shield)
    {
        if(shield >= dmg)
        {
            shield = shield - dmg;
            dmg = 0;
        }
        else
        {
            dmg = dmg - shield;
            shield = 0;
        }
        hp = hp - dmg;
    }


}
