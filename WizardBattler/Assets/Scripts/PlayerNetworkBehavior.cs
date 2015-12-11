using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class PlayerNetworkBehavior : NetworkBehaviour
{
    int netId = -1;
    Player self;

    [SyncVar]
    int p1spellIndex = -1;
    [SyncVar]
    int p2spellIndex = -1;

    [SyncVar]
    int randomSeed = -1;


    [SyncVar]
    string p1Spell1;
    [SyncVar]
    string p1Spell2;
    [SyncVar]
    string p1Spell3;
    [SyncVar]
    string p1Spell4;
    [SyncVar]
    string p1Spell5;

    [SyncVar]
    string p2Spell1;
    [SyncVar]
    string p2Spell2;
    [SyncVar]
    string p2Spell3;
    [SyncVar]
    string p2Spell4;
    [SyncVar]
    string p2Spell5;


    // Use this for initialization
    void Start()
    {
        if (isLocalPlayer)
        {
            transform.position = GameObject.FindWithTag("spawn1").transform.position;
            gameObject.tag = "player1";
        }
        else
        {
            transform.position = GameObject.FindWithTag("spawn2").transform.position;
            transform.Rotate(new Vector3(0, 1, 0), 180, Space.Self);

            gameObject.tag = "player2";
        }

        netId = (int)gameObject.GetComponent<NetworkIdentity>().netId.Value;


        if (netId == 3)
        {
            CmdRandomSeed(Random.Range(0, 100));
        }

        Random.seed = System.DateTime.Now.Minute;
    }

    public void Init()
    {
        EntireSpellDictionary esd = new EntireSpellDictionary();

        if (isLocalPlayer)
        {
            GameObject.Find("NetworkManager").GetComponent<NetworkManagerHUD>().enabled = false;

            self = gameObject.GetComponent<Player>();
            self.SpellBook = new List<Spell>();

            foreach (string s in GameObject.Find("NetworkManager").GetComponent<SpellPassing>().playerSpells)
            {
                esd.GetSpell(s, self);
            }

            for (int i = 0; i < 5; i++) CmdSetString(i, self.SpellBook[i].SpellName);
        }
        else
        {

            netId = (int)gameObject.GetComponent<NetworkIdentity>().netId.Value;

            self = gameObject.GetComponent<Player>();
            self.SpellBook = new List<Spell>();

            //if (randomSeed == -1)
            //{
            //    CmdRandomSeed(Random.Range(0, 100));
            //    Random.seed = randomSeed;
            //}
            //else Random.seed = randomSeed;

            if (netId == 3)
            {
                esd.GetSpell(p1Spell1, self);
                esd.GetSpell(p1Spell2, self);
                esd.GetSpell(p1Spell3, self);
                esd.GetSpell(p1Spell4, self);
                esd.GetSpell(p1Spell5, self);
            }
            else if (netId == 4)
            {
                esd.GetSpell(p2Spell1, self);
                esd.GetSpell(p2Spell2, self);
                esd.GetSpell(p2Spell3, self);
                esd.GetSpell(p2Spell4, self);
                esd.GetSpell(p2Spell5, self);
            }
        }

    }
    // Update is called once per frame
    void Update()
    {

        if (!isLocalPlayer)
        {
            if (netId == 3)
            {
                if (p1spellIndex >= 0)
                {
                    CastSpellAtIndex(p1spellIndex);
                    p1spellIndex = -1;
                    CmdSpellMagic(-1);
                }
            }
            else if (netId == 4)
            {
                if (p2spellIndex >= 0)
                {
                    CastSpellAtIndex(p2spellIndex);
                    p2spellIndex = -1;
                    CmdSpellMagic(-1);
                }
            }
        }
    }

    public void SetSpellIndex(int _index)
    {
        if (isLocalPlayer)
        {
            CmdSpellMagic(_index);
        }
    }

    [Command]
    public void CmdRandomSeed(int _seed)
    {
        randomSeed = _seed;
    }

    [Command]
    void CmdSpellMagic(int _index)
    {
        if (netId == 3)
            p1spellIndex = _index;
        else if (netId == 4) p2spellIndex = _index;
    }

    [Command]
    void CmdSetString(int _stringToSet, string _spellName)
    {
        switch(_stringToSet)
        {
            case 0:

                if(netId == 3)
                {
                    p1Spell1 = _spellName;
                }
                else
                {
                    p2Spell1 = _spellName;
                }

                break;

            case 1:
                if (netId == 3)
                {
                    p1Spell2 = _spellName;
                }
                else
                {
                    p2Spell2 = _spellName;
                }
                break;

            case 2:
                if (netId == 3)
                {
                    p1Spell3 = _spellName;
                }
                else
                {
                    p2Spell3 = _spellName;
                }
                break;
            case 3:
                if (netId == 3)
                {
                    p1Spell4 = _spellName;
                }
                else
                {
                    p2Spell4 = _spellName;
                }
                break;
            case 4:
                if (netId == 3)
                {
                    p1Spell5 = _spellName;
                }
                else
                {
                    p2Spell5 = _spellName;
                }
                break;

            default:
                break;
        }
    }

    public void CastSpellAtIndex(int _index)
    {
        if (self.activeSpell == null)
        {
            self.activeSpell = self.SpellBook[_index];
            self.Mana -= self.SpellBook[_index].manaCost;
            self.uis.setPlayerTwoMana(self.Mana);
        }
    }
}
