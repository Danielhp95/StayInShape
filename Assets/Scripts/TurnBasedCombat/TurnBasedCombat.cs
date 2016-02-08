﻿using UnityEngine;
using Priority_Queue;
using System;
using System.Text;

public class TurnBasedCombat : MonoBehaviour {

    public BattleState currentState;
    private FastPriorityQueue<QueuedEntity> turnQueue;
    private BattleGenerator battleGenerator; // Handles battle initialization

    /* Arrays containing the allied characters and the enmies.
       Note that the allied character array will be passed to GameInformation
       without modification at the end of the battle. This is so because characters
       do not regain health or energt (mana, stamina) after the end of the battle.
    */
    private BaseCharacterClass[] alliedCharacters;
    private BaseEnemy[] enemies;


    //Used during a player's turn
    private BaseCharacterClass allyTurnReady;
    public int playerTargetIndex { get; set; }


	// Use this for initialization
	void Start () {
        currentState = BattleState.START;
        battleGenerator = new BattleGenerator();
        this.turnQueue = battleGenerator.generateBattle();
        this.alliedCharacters = battleGenerator.alliedCharacters;
        this.enemies = battleGenerator.enemies;
        currentState = BattleState.WAITING;
        
    }
	
	/* Update is called once per frame
       The function works like this:
            - The turnQueue is queried to see if someone's turn should begin
            - Depending on the state of the battle, a turn is executed, nothing happens...etc
            - Finally, the waiting times are updated according to Time.Delta
    */
	void Update () {
        //The current battle state determines what actions can be taken.
        checkForTurn();
        checkEndOfBattle();
	    switch(currentState)
        {
            case BattleState.START:
                // Start of the battle.
                UIStateController.Instance.enableMenu("waiting");
                break;
            case BattleState.WAITING:
                // Idle phase, nothing happens.
                /* DIFFERENTS EFFECTS COUlD BE APPLIED:
                        - Check if buffs / debuffs end (speed up, poison...)
                        - Apply effects such as poison
                        - Regeneration (Perhaps armor fell off and is being regenerated)
                */
                break;
            case BattleState.PLAYERTURN:
                // Player turn starts and 
                // Start GUI showing the "initial" menu state.
                if (allyTurnReady == null)
                {
                    QueuedEntity allyqe = turnQueue.Dequeue();
                    allyTurnReady = (BaseCharacterClass)allyqe.entity;
                    UIStateController.Instance.enableMenu("initial");
                    UIStateController.Instance.personaliseMenuToCharacter(allyTurnReady);
                }
                
                //Debug.Log(ally.characterName() + "'s turn. HE DOES NOTHING");
                
                //
                //currentState = BattleState.WAITING;
                break;
            case BattleState.ENEMYTURN:
                // Enemy's turn starts
                QueuedEntity enemyqe = turnQueue.Dequeue();
                BaseEnemy enemy = (BaseEnemy)enemyqe.entity;

                // To ensure that dead units are not targeted, filter them out.
                if (enemy == null)
                {
                    Debug.Log("null enemy");
                }
                Entity target   = enemy.strategy(Array.FindAll(alliedCharacters, e => !e.isDead()),
                                                 Array.FindAll(enemies, e => !e.isDead()));


                Debug.Log(enemy.getName() + " Attacks " + target.getName() + " for " + enemy.attack + " damage");
                target.takeDamage(enemy.attack);
                printHealths(alliedCharacters);

                turnQueue.Enqueue(new QueuedEntity(enemy), enemy.speed);
                currentState = BattleState.WAITING;
                break;
            case BattleState.CALCDAMAGE:
                // Calculates dmg to target(s)
                BattleAbilityUseHandler abilityHandler = new BattleAbilityUseHandler(ref allyTurnReady, playerTargetIndex,
                                                                                      ref alliedCharacters, ref enemies);
                abilityHandler.calculateDamage();
                int castingTIme = abilityHandler.calculateCastingTime();
                this.playerTargetIndex = -1;

                turnQueue.Enqueue(new QueuedEntity(allyTurnReady), castingTIme);
                this.allyTurnReady = null;
                currentState = BattleState.WAITING;
                printHealths(enemies);
                break;
            case BattleState.ADDSTATUSEFFECT:
                // Adds special status such as poison.
                break;
            case BattleState.WIN:
                /* All enemies have been defeated, we store the relevant information in GameInformation
                   and we get back to the map */
                //GameInformation.alliedCharacters = alliedCharacters;
                Debug.Log("GAME WON");
                break;
            case BattleState.LOSE:
                // All allied characters have been defeated.
                Debug.Log("GAME LOST");
                break;
            default:
                break;
        }

        // Update the turnque in order to reflect time passing.
        updateTurnQueue(Time.deltaTime);

    }

    /* TODO: CHANGE THIS SO IT WORKS WITH ABILITIES

        return value indicates if action could have been carried out
    */
    public bool performAction(PossibleAction posAc)
    {
        if (posAc == PossibleAction.ATTACK)
        {
            currentState = BattleState.CALCDAMAGE;
            return true;
        }
        return false;
    }

    /*
        Checks if any Entity is ready to play a tudrn
        and changes the battle state accordingly.
        NOTE: makes use of the property that the queue
              contains either characters or enemies.
    */
    private void checkForTurn()
    {
        QueuedEntity head = turnQueue.peek();
        if (head.Priority <= 0 && !(currentState == BattleState.CALCDAMAGE ||
                                    currentState == BattleState.ENEMYTURN)) 
        {
            currentState = (head.entity is BaseCharacterClass) ? BattleState.PLAYERTURN : BattleState.ENEMYTURN;
        }
    }

    /*
        Key method. Updates the turn of every entity in the battle by the time elapsed 
        between frames, whichever reaches 0 (zero) first will 
    */
    private void updateTurnQueue(float elapsedFrameTime)
    {
        turnQueue.reducePriorities(elapsedFrameTime);
    }

    /*
      Try to make it so that this function gets called when health of an entity is reduced on any way.

      If all alliedCharacters are dead: game over.
      If all the enemies are dead: battle won.
    */
    private void checkEndOfBattle()
    {
        checksIfDead(alliedCharacters, BattleState.LOSE);
        checksIfDead(enemies, BattleState.WIN);

    }

    private void checksIfDead(Entity[] entities, BattleState newState)
    {
        foreach (Entity e in entities)
        {
            if (!e.isDead())
            {
                return;
            }
        }
        currentState = newState;
    }


    // DEBUGGING
    public void printHealths(Entity[] e)
    {
        StringBuilder builder = new StringBuilder();
        foreach (Entity en in e)
        {
            builder.Append(en.getName() + ": " + en.health + " ");
        }
        Debug.Log(builder.ToString());
    }

}
