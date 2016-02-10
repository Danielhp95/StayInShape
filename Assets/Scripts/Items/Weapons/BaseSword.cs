using UnityEngine;
using System.Collections;

public class BaseSword : BaseWeapon {

    public BaseSword(string name, int attackDamage, int attackCastTime)
    {
        this.type = WeaponType.SWORD;
        this.itemName = name;
        this.attackDamage = attackDamage;
        this.attackCastTime = attackCastTime;
    }

}
