using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EntireSpellDictionary {

    public List<string> spellList;

	public EntireSpellDictionary()
    {
        spellList = new List<string>();

        spellList.Add("FireBallBasic");
        spellList.Add("fleetingWaters");
        spellList.Add("RockBasic");
    }

    public void GetSpell(string _id, Player _player)
    {
        switch(_id)
        {
            case "FireBallBasic":
                _player.SpellBook.Add(new FireBallBasic(_player));
                break;
            case "WaterBoltBasic":
                _player.SpellBook.Add(new WaterBoltBasic(_player));
                break;
            case "RockBasic":
                _player.SpellBook.Add(new RockBasic(_player));
                break;
            case "fleetingWaters":
                _player.SpellBook.Add(new FleetingWaters(_player));
                break;
            case "BurningRage":
                _player.SpellBook.Add(new BurningRage(_player));
                break;
            case "Quicksand":
                _player.SpellBook.Add(new QuickSand(_player));
                break;
            case "Barrier":
                _player.SpellBook.Add(new Barrier(_player));
                break;
            case "RadiantFlare":
                _player.SpellBook.Add(new RadiantFlare(_player));
                break;
            case "GraspingHands":
                _player.SpellBook.Add(new GraspingHands(_player));
                break;
            case "Shock":
                _player.SpellBook.Add(new Shock(_player));
                break;
            case "Awe":
                _player.SpellBook.Add(new Awe(_player));
                break;
            default:
                break;
        }
    }
}
