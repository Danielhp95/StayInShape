using UnityEngine;
using System.Collections;

/*
    Base weapon. Most abstract class used for weapons.
*/
public abstract class BaseWeapon : BaseItem {

    public int attackCastTime { get; protected set; }
    

    /*
        trueAttackDamage contains the real damage of the weapon. To spice up the game, damage from a weapon
        will not be constant and will vary a little, according to damageVariation throughout a battle. For this we
        use attackDamage for all practical purposes outside this class.
    */
    private float trueAttackDamage;
    public int attackDamage { get
        {
            return (int) Random.Range(trueAttackDamage * (1 - damageVariation), 
                                      trueAttackDamage * (1 + damageVariation));
        }
        protected set { trueAttackDamage = value; }
    }
    public WeaponType type { get; protected set; }

    /* Range on which the actual inflicted damage can differ from the attackDamage value 10% by default*/
    protected float damageVariation = 0.1f;
	public enum WeaponType
    {
        SWORD, MACE, SHIV, STAFF, SHIELD, ORB, GUN
    }
}
