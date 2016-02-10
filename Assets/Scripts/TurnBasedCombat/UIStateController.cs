using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;
using System;

public class UIStateController : MonoBehaviour {

    const int NUMBER_BUTTONS_PER_MENU = 4;

    // Singleton patter. Perhaps not the smart choice, only used once as singleton...
    public static UIStateController Instance;

    // Controls the states and information to forward to TurnBasedCombat upon completition.
    private MenuState currentState;
    private PossibleAction p;

    //Turn based combat;
    public GameObject battleController;
    private TurnBasedCombat combat;

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
    public Text characterName;
    public Text allies;
    public Text enemies;

    // This is the only menu that will be visible 
    private GameObject currentMenu;
    // The character is necessary to change the text according to its abilities
    private BaseCharacterClass allyTurnReady;

    // Use this for initialization
    void Start () {
        // Start singleton pattern
        Instance = this;

        // Activate first menu.
        currentMenu = waiting;
        currentState = MenuState.WAITING;
        currentMenu.SetActive(true);
        characterName.text = "";

        combat = battleController.GetComponent<TurnBasedCombat>();
	}
	
	/*
        Will be used to handle Key input for traversing the different menus.
    */
	void Update () {
        // Note how there's no UI button to go back on a menu.
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

    /*
    Changes the text of the UI to reflect the current character abilities and overall items.
    It also updates the text to show possible targets.
    */
    public void personaliseMenuToCharacter(BaseCharacterClass c)
    {
        this.allyTurnReady = c;
        characterName.text = allyTurnReady.getName();
        changeMenuText();
    }

    /*
        Changes the text of the UI to reflect the current character abilities and overall items.
        It also updates the text to show possible targets.
    */
    private void changeMenuText()
    {
        // Change text to reflect the names of the enemies on choose target.
        BaseEnemy[] enemies = combat.enemies;

        for (int i = 0; i < NUMBER_BUTTONS_PER_MENU; ++i)
        {
            Transform button = chooseTarget.transform.Find("Button" + (i + 1));
            button.GetComponentInChildren<Text>().text = enemies[i].getName();
        }

    }

    /* Functions that deal menu movement through keys */
    private void handleQPress()
    {
        switch (currentState)
        {

            case MenuState.ATTACK:
                //Target will be the first enemy / ally
                attemptFinishTurnAndSetActions(0, PossibleAction.ATTACK);
                break;
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

    // Used at the last step of the menu. Both ability and target have been selected
    private void attemptFinishTurnAndSetActions(int playerTargetIndex, PossibleAction posAc) 
    {
        if (combat.isValidTarget(playerTargetIndex, posAc))
        {
            combat.playerTargetIndex = playerTargetIndex;
            combat.performAction(posAc);
            characterName.text = "";
            changeMenuState(MenuState.WAITING);
        }

    }

    private void handleWPress()
    {
        switch (currentState)
        {

            case MenuState.ATTACK:
                attemptFinishTurnAndSetActions(1, PossibleAction.ATTACK);
                break;
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
                attemptFinishTurnAndSetActions(2, PossibleAction.ATTACK);
                break;
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
                attemptFinishTurnAndSetActions(3, PossibleAction.ATTACK);
                break;
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

    /*
        This function will also be used to see the current character
        has passed out. In such case, it's turn will be finished.
    */
    public void updateCharacterAndEnemiesText()
    {
        if (allyTurnReady != null && allyTurnReady.isDead()) {
            // Character died, sad life.
            attemptFinishTurnAndSetActions(-1, PossibleAction.CHARACTER_DIED);
        }

        StringBuilder alliesBuilder = new StringBuilder();
        foreach (Entity e in combat.alliedCharacters)
        {
            alliesBuilder.Append(e.getName() + ": " + e.health + "   ");
        }
        allies.text = alliesBuilder.ToString();

        StringBuilder enemisBuilder = new StringBuilder();
        foreach (Entity e in combat.enemies)
        {
            enemisBuilder.Append(e.getName() + ": " + e.health + "   ");
        }
        enemies.text = enemisBuilder.ToString();
    }
}
