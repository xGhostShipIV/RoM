using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EntireSpellDictionary {

    public Dictionary<string, Spell> spellDictionary;

	public EntireSpellDictionary()
    {
        spellDictionary = new Dictionary<string, Spell>();

        spellDictionary.Add("FireBallBasic", new FireBallBasic(new Player()));
        spellDictionary.Add("WaterBoltBasic", new WaterBoltBasic(new Player()));
        spellDictionary.Add("RockBasic", new RockBasic(new Player()));
    }
}
