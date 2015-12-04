using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellPassing : MonoBehaviour {

    public List<Spell> playerSpells;
    
	// Use this for initialization
	void Start () {
        playerSpells = new List<Spell>();

        playerSpells.Add(new FireBallBasic(new Player()));
        playerSpells.Add(new WaterBoltBasic(new Player()));
        playerSpells.Add(new RockBasic(new Player()));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetSpells(List<Spell> _spells)
    {
        playerSpells = _spells;
    }
}
