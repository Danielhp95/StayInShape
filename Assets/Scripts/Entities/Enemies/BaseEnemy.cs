using UnityEngine;
using System.Collections;

/*
    Base class for an enemy.

    TODO: CHange attack to something more interesting.

    An strategy is a function pointer (a delegate) which takes all entities in the battle,
    upon which the enemy decides which one should target.
*/
public delegate Entity Strategy(BaseCharacterClass[] characters, BaseEnemy[] enemies);

public class BaseEnemy : Entity {

    public int attack { set; get; }
    public Strategy strategy;

    public int xp { get; set; }


    public BaseEnemy(string name, int attack, Strategy strategy) 
    {
        this.health = 20;
        this.name = name;
        this.attack = attack;
        this.strategy = strategy;
        this.speed = Random.Range(9, 15); //TODO: CHANGE THIS
        this.xp = 20;
    }

    override public string getName()
    {
        return this.name;
    }
             
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               
}

