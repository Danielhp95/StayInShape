using UnityEngine;
using System.Collections;

public class BaseSword : BaseWeapon {

    public BaseSword(string name, int attackDamage, int attackCastTime)
    {
        this.type = WeaponType.SWORD;
        this.name = name;
        this.attackDamage = attackDamage;
        this.attackCastTime = attackCastTime;
    }

}
