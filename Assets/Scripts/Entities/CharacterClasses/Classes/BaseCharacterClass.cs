using UnityEngine;
using System.Collections;
using System;

/*
    Base class for allied characters. It should have enough information to
    delegate all attacks and decisions such as talent tree specifications
    to it's child classes.
    
    This class is used throughout
    TurnBasedCombat in order to differentiate between enemis and allies.
*/
public abstract class BaseCharacterClass : Entity{

    public CharacterClass characterClass { get; set;}


    /* Combat stats */
    protected int dexterity { get; set; }
    protected int strength { get; set; }
    protected int faith { get; set; }
    protected int intellect { get; set; }
    protected int defense { get; set; }
    public int presence { get; protected set; }


    /*  Leveling stats */
    public int level { get; private set; }
    public int totalXP { get; set; }
    public int nextLevelXP;

    /* Equipment */
    public BaseWeapon weapon { get; protected set; }

    /* Miscelaneous */
    public string characterName { get { return characterClass.ToString(); } }

   

    public override string getName()
    {
        return characterClass.ToString();
    }



    /*
        TODO, make this perhaps class specific.

        Calculates basic damage to deal to a unit which
        may also take into account the unit type.
    */
    public int basicAttack(Entity e)
    {
        return weapon.attackDamage;
    }

    /* Levelling functions */
    /* This functions is called at the end of every battle.
       It implicitly checks if the character has leveled up
       and if so deals with it accordingly. */
    public void addXP(int earnedXP)
    {
        totalXP += earnedXP;
        if (totalXP >= nextLevelXP)
        {
            levelUp();
        }
    }

    //TODO: add code for leveling up
    private void levelUp() { }

}

public enum CharacterClass
{
    PALADISK, PRIESTMA, TRIANGICIAN, WARRIOVAL
}
