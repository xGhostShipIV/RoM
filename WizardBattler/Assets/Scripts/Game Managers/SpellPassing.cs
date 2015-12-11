using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellPassing : MonoBehaviour {

    public List<string> playerSpells;
    
	// Use this for initialization
	void Start () {
        playerSpells = new List<string>();

        playerSpells.Add("Barrier");
        playerSpells.Add("Awe");
        playerSpells.Add("GraspingHands");
        playerSpells.Add("RadiantFlare");
        playerSpells.Add("Shock");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
