﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/*
    Most abstract class that encapsulates units such as enemies and characters. Contains the most Basic 
    information about units, which is their health, so they can be targeted.
*/

public abstract class Entity
{

    // Speed dictates the readiness at the beginning of a battle.
    public float speed { get; set; }
    public int health { get; set; }

    protected string name;

    abstract public string getName();

    public bool isDead()
    {
        return health <= 0;
    }

    // TODO: put interesting stuff in this function
    /*
        Reduces health by the input amount. Health cannot go below 0.
    */
    public void takeDamage(int damage)
    {
        health = (damage < health) ? (health - damage) : 0;
    }


}
