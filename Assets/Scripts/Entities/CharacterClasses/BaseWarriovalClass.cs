using UnityEngine;
using System.Collections;

public class BaseWarriovalClass : BaseCharacterClass {

    public BaseWarriovalClass()
    {
        characterClass = CharacterClass.WARRIOVAL;
        dexterity = 17;
        strength = 17;
        faith = 5;
        intellect = 3;
        defense = 15;
        speed = 3;

        presence = 10;

        health = 18;
    }

}
