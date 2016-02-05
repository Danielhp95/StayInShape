using UnityEngine;
using System.Collections;

public class BasePaladiskClass : BaseCharacterClass {

	public BasePaladiskClass()
    {
        characterClass = CharacterClass.PALADISK;
        dexterity = 10;
        strength = 14;
        faith = 16;
        intellect = 8;
        defense = 15;
        speed = 5;

        presence = 14;

        health = 20;
    }
}
