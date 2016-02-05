using UnityEngine;
using Priority_Queue;
using System;

public class TurnBasedCombat : MonoBehaviour {

    private BattleState currentState;
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
    private Entity playerTarget;


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
                }
               
                //Debug.Log(ally.getName() + "'s turn. HE DOES NOTHING");
                
                //turnQueue.Enqueue(new QueuedEntity(allyTurnReady), allyTurnReady.speed);
                //currentState = BattleState.WAITING;
                break;
            case BattleState.ENEMYTURN:
                // Enemy's turn starts
                QueuedEntity enemyqe = turnQueue.Dequeue();
                BaseEnemy enemy = (BaseEnemy)enemyqe.entity;

                // To ensure that dead units are not targeted, filter them out.
                Entity target   = enemy.strategy(Array.FindAll(alliedCharacters, e => !e.isDead()),
                                                 Array.FindAll(enemies, e => !e.isDead()));


                Debug.Log(enemy.getName() + " Attacks " + target.getName());
                target.takeDamage(enemy.attack);

                turnQueue.Enqueue(enemyqe, enemy.speed);
                currentState = BattleState.WAITING;
                break;
            case BattleState.CALCDAMAGE:
                // Calculates dmg to target(s)
                break;
            case BattleState.ADDSTATUSEFFECT:
                // Adds special status such as poison.
                break;
            case BattleState.WIN:
                /* All enemies have been defeated, we store the relevant information in GameInformation
                   and we get back to the map */
                //GameInformation.alliedCharacters = alliedCharacters;
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


    /*
        Checks if any Entity is ready to play a turn
        and changes the battle state accordingly.
    */
    private void checkForTurn()
    {
        QueuedEntity head = turnQueue.peek();
        if (head.Priority <= 0)
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
        foreach (Entity e in entities)
        {
            Debug.Log("HEALTH of " + e.getName() + ": " + e.health);
        }
        currentState = newState;
    }
}
