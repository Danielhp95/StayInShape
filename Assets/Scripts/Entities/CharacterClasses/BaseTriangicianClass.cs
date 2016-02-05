using UnityEngine;
using System.Collections;

public class BaseTriangicianClass : BaseCharacterClass {

    public BaseTriangicianClass()
    {
        characterClass = CharacterClass.TRIANGICIAN;
        dexterity = 7;
        strength = 5;
        faith = 10;
        intellect = 24;
        defense = 9;
        speed = 3;

        presence = 6;

        health = 12;
    }
}
