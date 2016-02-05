using UnityEngine;
using System.Collections;

/* This class will hold all the information regarding the players and their state.
   This won't be destroyed on load */

public class GameInformation : MonoBehaviour {


    // Throughout the game there will only be 4 allied characters.
    private const int NUMBER_OF_CHARACTERS = 4;
    public static BaseCharacterClass[] alliedCharacters;

	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(gameObject);
        alliedCharacters = new BaseCharacterClass[NUMBER_OF_CHARACTERS];

        // TODO: CHANGE THIS WHEN IMPLEMENTING CHARACTER CREATION
        alliedCharacters[0] = new BasePaladiskClass();
        alliedCharacters[1] = new BasePriestmClass();
        alliedCharacters[2] = new BaseWarriovalClass();
        alliedCharacters[3] = new BaseTriangicianClass();

    }


}
