using UnityEngine;
using System.Collections;

public class EnemyStrategies  {


    /*
        Simplest of strategies, attacks a random character.
    */
    public static Entity randomStrategy(BaseCharacterClass[] characters, BaseEnemy[] enemies)
    {
        return characters[Random.Range(0, characters.Length - 1)];
    }

    /*
        Attacks based on presence
    */
    public static Entity purePresenceStrategy(BaseCharacterClass[] characters, BaseEnemy[] enemies)
    {
        int totalPresence = 0;
        foreach (BaseCharacterClass c in characters) {
            totalPresence += c.presence;
        }

        int random = Random.Range(0, totalPresence);
        int presenceAccumulator = 0;
        foreach (BaseCharacterClass c in characters)
        {
            int characterPresence = c.presence;
            if (random <= characterPresence + presenceAccumulator)
            {
                return c;
            }
            presenceAccumulator += characterPresence;
        }
        // Should never reach this case!
        return null;
    }

    /*
        For testing purposes. Attacks the unit with highest presence
    */
    public static Entity biggestPresenceStrategy(BaseCharacterClass[] characters, BaseEnemy[] enemies)
    {
        BaseCharacterClass target = characters[0];
        for (int i = 1; i < characters.Length; i++)
        {
            if (target.presence < characters[i].presence)
            {
                target = characters[i];
            }
        }
        return target;
    }
}
