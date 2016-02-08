using UnityEngine;
/*
   This class manages auto attacks and the use of abilities for all clases.
*/
using System;

class BattleAbilityUseHandler {

    private BaseCharacterClass character;
    private BaseCharacterClass[] alliedCharacters;
    private BaseEnemy[] enemies;
    // We store the index because the target could be an ally or an enemy
    private int playerTargetIndex;

    public BattleAbilityUseHandler(ref BaseCharacterClass character, int playerTargetIndex, ref BaseCharacterClass[] alliedCharacters, ref BaseEnemy[] enemies)
    {
        this.character = character;
        this.playerTargetIndex = playerTargetIndex;
        this.alliedCharacters = alliedCharacters;
        this.enemies = enemies;
    }

    public void calculateDamage()
    {
        int damage = character.basicAttack(enemies[playerTargetIndex]);
        enemies[playerTargetIndex].takeDamage(damage);
        Debug.Log(enemies[playerTargetIndex].getName() + " took: " + damage);
    }

    internal int calculateCastingTime()
    {
        return character.weapon.attackCastTime;
    }
}