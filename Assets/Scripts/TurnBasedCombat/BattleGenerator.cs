using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using Priority_Queue;
using System;

/*
    This class will contain all the logic for setting up a battle.
    Setting up a battle includes the following steps:
        - Gathering player information
        - Creating enemies with respect to the player information
        - Setting up the priority queue for the players and the enemies
*/

public class BattleGenerator {

    // There will be a maximum of 4 allies and 4 enmies.
    private const int MAX_NUMBER_ENTITIES = 8;

    FastPriorityQueue<QueuedEntity> pq;

    public BaseCharacterClass[] alliedCharacters { get; set; }
    public BaseEnemy[] enemies { get; set; }

    public BattleGenerator()
    {
        pq = new FastPriorityQueue<QueuedEntity>(MAX_NUMBER_ENTITIES);
        alliedCharacters = GameInformation.alliedCharacters;
    }

    public FastPriorityQueue<QueuedEntity> generateBattle()
    {

        queueAlliedCharacters();
        queueAllEnemies();
        return pq;
    }

    /*
        Generates random enemies and queues them into the speed queue
    */
    private void queueAllEnemies()
    {
        int numberOfEnemies = 4;
        enemies = new BaseEnemy[numberOfEnemies];
        enemies[0]  = new BaseEnemy("Alan", 3, EnemyStrategies.biggestPresenceStrategy);
        enemies[1]  = new BaseEnemy("Pedro", 4, EnemyStrategies.randomStrategy);
        enemies[2]  = new BaseEnemy("Niko", 5, EnemyStrategies.purePresenceStrategy);
        enemies[3]  = new BaseEnemy("Andy", 7, EnemyStrategies.purePresenceStrategy);

        foreach (BaseEnemy e in enemies)
        {
            pq.Enqueue(new QueuedEntity(e), e.speed);
        }
    }

    /*
        Retrieves information from GameInformation and queues all characters
        into the speed queue.
    */
    private void queueAlliedCharacters()
    {
        foreach (BaseCharacterClass c in GameInformation.alliedCharacters)
        {
            pq.Enqueue(new QueuedEntity(c), c.speed);
        }
    }
}
