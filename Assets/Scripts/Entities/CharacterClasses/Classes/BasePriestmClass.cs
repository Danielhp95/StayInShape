using UnityEngine;
using System.Collections;

public class BasePriestmClass : BaseCharacterClass {

    public BasePriestmClass()
    {
        characterClass = CharacterClass.PRIESTMA;
        dexterity = 5;
        strength = 18;
        faith = 20;
        intellect = 15;
        defense = 10;
        speed = 4;

        presence = 7;

        health = 15;

        weapon = new BaseSword("Sword", 10, 5);
    }
}
