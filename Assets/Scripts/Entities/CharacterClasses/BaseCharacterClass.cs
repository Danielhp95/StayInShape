using UnityEngine;
using System.Collections;
using System;

public abstract class BaseCharacterClass : Entity{

    public CharacterClass characterClass { get; set;}

    /* Combat stats */
    protected int dexterity { get; set; }
    protected int strength { get; set; }
    protected int faith { get; set; }
    protected int intellect { get; set; }
    protected int defense { get; set; }
    // Presence is a parameter usde by the enemy that determines
    // how likely a character is to be targeted.
    protected int presence { get; set; }


    /*  Leveling stats */
    public int level;
    public int totalXP { get; set; }
    public int nextLevelXP;

    public enum CharacterClass
    {
        PALADISK, PRIESTMA, TRIANGICIAN, WARRIOVAL
    }

    public int getPresence()
    {
        return this.presence;
    }

    public override string getName()
    {
        return characterClass.ToString();
    }

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
