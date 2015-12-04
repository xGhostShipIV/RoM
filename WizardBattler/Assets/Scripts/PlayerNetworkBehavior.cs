using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerNetworkBehavior : NetworkBehaviour
{
    Player self;

    [SyncVar]
    int spellIndex;


    [SyncVar]
    string spell1;

    [SyncVar]
    string spell2;

    [SyncVar]
    string spell3;

	// Use this for initialization
	void Start ()
    {
        if (isLocalPlayer)
        {
            transform.position = GameObject.FindWithTag("spawn1").transform.position;
            gameObject.tag = "player1";

            GameObject.Find("NetworkManager").GetComponent<NetworkManagerHUD>().enabled = false;

            self = gameObject.GetComponent<Player>();


            self.SpellBook = GameObject.Find("NetworkManager").GetComponent<SpellPassing>().playerSpells;

            //CmdSetString(spell1, self.SpellBook[0].SpellName);
            //CmdSetString(spell1, self.SpellBook[1].SpellName);
            //CmdSetString(spell1, self.SpellBook[2].SpellName);

        }
        else
        {
            transform.position = GameObject.FindWithTag("spawn2").transform.position;
            transform.Rotate(new Vector3(0, 1, 0), 180, Space.Self);

            gameObject.tag = "player2";

            self = gameObject.GetComponent<Player>();
        }

        
        spellIndex = -1;
	}
	
	// Update is called once per frame
	void Update () {

        if (spellIndex >= 0)
            CastSpellAtIndex();

        //if(!isLocalPlayer)
        //{
        //    if(self.SpellBook.Count < 3)
        //    {

        //    }
        //}
	}

    public void SetSpellIndex(int _index)
    {
        if(isLocalPlayer)
        {
            CmdSpellMagic(_index);
        }
    }

    [Command]
    void CmdSpellMagic(int _index)
    {
        spellIndex = _index;
    }

    [Command]
    void CmdSetString(string _stringToSet, string _spellName)
    {
        _stringToSet = _spellName;
    }

    public void CastSpellAtIndex()
    {
        if(!isLocalPlayer)
        {
            if(self.activeSpell == null)
            {
                self.activeSpell = self.SpellBook[spellIndex];
                self.Mana -= self.SpellBook[spellIndex].manaCost;
                self.uis.setPlayerTwoMana(self.Mana);
            }

            spellIndex = -1;
        }
    }
}
