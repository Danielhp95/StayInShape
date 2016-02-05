﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIStateController : MonoBehaviour {

    public static UIStateController Instance;

    private MenuState currentState;

    /*
        This canvas comprise the UI for the battle.

    */
    public GameObject waiting;
    public GameObject initial;
    public GameObject chooseTarget;
    public GameObject special;
    public GameObject abilities;
    public GameObject items;
    public GameObject potions;
    public GameObject throwables;
    public GameObject medicines;
    public GameObject equipment;

    private GameObject currentMenu;

    // Use this for initialization
    void Awake () {
        Instance = this;
        currentMenu = waiting;
        currentState = MenuState.WAITING;
        currentMenu.SetActive(true);
	}
	
	/*
        Will be used to handle Key input for traversing the different menus.
    */
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            MenuState previous = getPreviousState(currentState);
            changeMenuState(previous);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            handleQPress();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            handleWPress();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            handleEPress();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            handleRPress();
        }
    }

    /* Functions that deal menu movement through keys */
    private void handleQPress()
    {
        switch (currentState)
        {

            case MenuState.ATTACK:
            case MenuState.SPECIAL:
            case MenuState.ABILITIES:
            case MenuState.ITEMS:
                changeMenuState(MenuState.ITEMS_POTIONS);
                break;
            case MenuState.INITIAL:
                changeMenuState(MenuState.ATTACK);
                break;
            case MenuState.ITEMS_POTIONS:
            case MenuState.ITEMS_THROWABLE:
            case MenuState.ITEMS_MEDICINE:
            case MenuState.ITEMS_EQUIPMENT:
                break;
            default:
                break;

        }

    }

    private void handleWPress()
    {
        switch (currentState)
        {

            case MenuState.ATTACK:
            case MenuState.SPECIAL:
            case MenuState.ABILITIES:
            case MenuState.ITEMS:
                changeMenuState(MenuState.ITEMS_THROWABLE);
                break;
            case MenuState.INITIAL:
                changeMenuState(MenuState.SPECIAL);
                break;
            case MenuState.ITEMS_POTIONS:
            case MenuState.ITEMS_THROWABLE:
            case MenuState.ITEMS_MEDICINE:
            case MenuState.ITEMS_EQUIPMENT:
                break;
            default:
                break;

        }
    }

    private void handleEPress()
    {
        switch (currentState)
        {

            case MenuState.ATTACK:
            case MenuState.SPECIAL:
            case MenuState.ABILITIES:
            case MenuState.ITEMS:
                changeMenuState(MenuState.ITEMS_MEDICINE);
                break;
            case MenuState.INITIAL:
                changeMenuState(MenuState.ABILITIES);
                break;
            case MenuState.ITEMS_POTIONS:
            case MenuState.ITEMS_THROWABLE:
            case MenuState.ITEMS_MEDICINE:
            case MenuState.ITEMS_EQUIPMENT:
                break;
            default:
                break;

        }
    }

    private void handleRPress()
    {
        switch (currentState)
        {

            case MenuState.ATTACK:
                // ATTACKS 4th enemy
            case MenuState.SPECIAL:
                //changeMenuState(MenuState.SPECIAL);
                break;
            case MenuState.ABILITIES:
               // changeMenuState(MenuState.ABILITIES);
                break;
            case MenuState.ITEMS:
                changeMenuState(MenuState.ITEMS_EQUIPMENT);
                break;
            case MenuState.INITIAL:
                changeMenuState(MenuState.ITEMS);
                break;
            case MenuState.ITEMS_POTIONS:
            case MenuState.ITEMS_THROWABLE:
            case MenuState.ITEMS_MEDICINE:
            case MenuState.ITEMS_EQUIPMENT:
                break;
            default:
                break;

        }
    }

    /* Called through clicks, or key presses in the UI, changes the menu 
            - newMenu: s in {"attacks", "special", "abilities", "items",
                             "potions", "throwables", "medicines", "equipment", "initial", "waiting"}
        
        */
    public void enableMenu(string newMenu)
    {
        MenuState newState;
        switch (newMenu)
        {
            case "initial":
                newState = MenuState.INITIAL;
                break;
            case "attack":
                newState = MenuState.ATTACK;
                break;
            case "special":
                newState = MenuState.SPECIAL;
                break;
            case "abilities":
                newState = MenuState.ABILITIES;
                break;
            case "items":
                newState = MenuState.ITEMS;
                break;
            case "potions":
                newState = MenuState.ITEMS_POTIONS;
                break;
            case "throwables":
                newState = MenuState.ITEMS_THROWABLE;
                break;
            case "medicines":
                newState = MenuState.ITEMS_MEDICINE;
                break;
            case "equipment":
                newState = MenuState.ITEMS_EQUIPMENT;
                break;
            case "waiting":
                newState = MenuState.WAITING;
                break;
            default:
                Debug.Log("Unvalid menu transition: " + newMenu);
                newState = 0;
                break;
        }
        changeMenuState(newState);
    }

    private void changeMenuState(MenuState newState)
    {
        currentMenu.SetActive(false);
        currentState = newState;
        switch (newState)
        {
            case MenuState.INITIAL:
                initial.SetActive(true);
                currentMenu = initial;
                break;
            case MenuState.ATTACK:
                chooseTarget.SetActive(true);
                currentMenu = chooseTarget;
                break;
            case MenuState.SPECIAL:
                special.SetActive(true);
                currentMenu = special;
                break;
            case MenuState.ABILITIES:
                abilities.SetActive(true);
                currentMenu = abilities;
                break;
            case MenuState.ITEMS:
                items.SetActive(true);
                currentMenu = items;
                break;
            case MenuState.ITEMS_POTIONS:
                potions.SetActive(true);
                currentMenu = potions;
                break;
            case MenuState.ITEMS_THROWABLE:
                throwables.SetActive(true);
                currentMenu = throwables;
                break;
            case MenuState.ITEMS_MEDICINE:
                medicines.SetActive(true);
                currentMenu = medicines;
                break;
            case MenuState.ITEMS_EQUIPMENT:
                equipment.SetActive(true);
                currentMenu = equipment;
                break;
            case MenuState.WAITING:
                waiting.SetActive(true);
                currentMenu = waiting;
                break;
            default:
                break;
        }
    }


    // Used when the user wants to backtrack on the menu
    private MenuState getPreviousState(MenuState m)
    {
        MenuState previous;
        switch (m)
        {
            case MenuState.ATTACK:
            case MenuState.SPECIAL:
            case MenuState.ABILITIES:
            case MenuState.ITEMS:
            case MenuState.INITIAL:
                previous = MenuState.INITIAL;
                break;
            case MenuState.ITEMS_POTIONS:
            case MenuState.ITEMS_THROWABLE:
            case MenuState.ITEMS_MEDICINE:
            case MenuState.ITEMS_EQUIPMENT:
                previous = MenuState.ITEMS;
                break;
            default:
                previous = m;
                break;
        }
        return previous;
    }
}