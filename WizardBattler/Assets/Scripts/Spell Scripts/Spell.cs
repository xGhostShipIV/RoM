using UnityEngine;
using PDollarGestureRecognizer;
using System.Collections;

public class Spell{

    public string SpellName;

    //public CheckBox spellCheckBox;
    public Point[] targetPoints;

    //variables to track time
    protected float elapsedTime;

    protected float timeToCast;
    protected float TIME_TO_CAST
    {
        get { return timeToCast - (timeToCast * caster.Haste); }
    }

    //float to track time for spell cooldowns
    public float coolDownTimer;

    //Actual cooldown stuff
    protected float coolDown;
    protected float COOLDOWN
    {

        get { return coolDown - (coolDown * caster.CooldownReduction); }
    }
    //The amount to subract from casters mana bar
    public float manaCost;

    //Brand new damage system!
    public Damage damage;

    protected Vector3 spellPosition;

    //A sprite that will contain an image for the cast bar
    public Sprite thumbnail;

    protected UIScript uis;

    //A reference to the player who will be casting the spell
    protected Player caster;

    public Spell(Player _caster)
    {
        spellPosition = Vector3.zero;

        caster = _caster;
        uis = GameObject.Find("UI Prefab").GetComponent<UIScript>();
    }

    public virtual Spell OnCast(Player _target)
    {
        return this;
    }

    public virtual bool IsOnCooldown()
    {
        return (coolDownTimer < COOLDOWN);
    }

    public bool ManageCoolDown()
    {
        //Cooldown is finished
        if(coolDownTimer >= COOLDOWN)
        {
            coolDownTimer = COOLDOWN;
            return false;
        }
        else
        {
            coolDownTimer += Time.deltaTime;
            return true;
        }
    }

    public virtual Spell CancelSpell(Player _victim)
    {
        return null;
    }

}