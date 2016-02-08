using UnityEngine;
/*
    All different actions that can be done in combat.
    Used to communitace TurnBasedCombat with UIStateController
*/
public enum PossibleAction {

	ATTACK,
    ABILITY_1, ABILITY_2, ABILITY_3, ABILITY_4,
    POTION_1, POTION_2, POTION_3, POTION_4,
    MEDICINE_1, MEDICINE_2, MEDICINE_3, MEDICINE_4
    // EQUIPMENT should be handled somehow, but we need to design it first!
}
