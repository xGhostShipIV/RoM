using UnityEngine;
using PDollarGestureRecognizer;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    #region FieldsAndProperties

    //A reference to the UI so that the player can update it
    //When values change
    public UIScript uis;

    public PlayerNetworkBehavior pnb;

    #region PlayerStats


    public bool Stunned;
    public bool Immune;

    //Tracks current health
    float currentHealth;

    //Amount of health gained per second
    //Will probably only come from buffs
    public float healthRegen;

    //Variables for max health and mana
    private float health;
    public float Health { get; set; }

    //As it stands max mana is always 100
    private float mana;
    public float Mana
    {
        get { return mana; }
        set { mana = value; }
    }

    //Probably will be an adjustable amount.
    //Mana regenerated per second
    private float manaRegen;

    //Bonus regen provided by buffs
    public float bonusManaRegen;

    public float ManaRegen
    {
        get { return manaRegen + bonusManaRegen; }
        set { manaRegen = value; }
    }

    //Focus represents a bonus to status(Buff/Debuff) duration
    //Will work as a % duration bonus
    private float focus;

    //Bonus focus gained from buffs
    public float bonusFocus;

    public float Focus
    {
        get { return (focus + bonusFocus) / 100.0f; }
        set { focus = value; }
    }

    //Malice represents bonus damage to DoT abilities
    //Will work as a percentage damage boost
    private float malice;
    public float bonusMalice;

    public float Malice
    {
        get { return (malice + bonusMalice) / 100.0f; }
        set { malice = value; }
    }

    //Represents a % increase in the bonuses provided from buffs
    //Needs a new name. A cooler name
    private float buffIntensity;
    public float bonusBuffIntensity;

    public float BuffIntesity
    {
        get { return (buffIntensity + bonusBuffIntensity) / 100.0f; }
        set { buffIntensity = value; }
    }

    //Pretty self explanatory. Will as well be a % time decrease on spell cooldowns
    private float cooldownReduction;
    public float bonusCooldownReduction;

    public float CooldownReduction
    {
        get { return (cooldownReduction + bonusCooldownReduction) / 100.0f; }
        set { cooldownReduction = value; }
    }

    //Represents a decrease in cast time of all prepared spells
    //Works as a % change in cast time
    //Will also be used with negative numbers to debuff enemy cast times
    private float haste;
    public float bonusHaste;

    public float Haste
    {
        get { return (haste + bonusHaste) / 100.0f; }
        set { haste = value; }
    }

    //Represents a % chance to avoid the application of a debuff against the player
    private float fortitude;
    public float bonusFortitude;

    public float Fortitude
    {
        get { return (fortitude + bonusFortitude) / 100.0f; }
        set { fortitude = value; }
    }


    /**************************************************************************************************************************/
    /***********************                        Damage Resistances               ******************************************/
    /**************************************************************************************************************************/

    //A % damage reduction against damage type fire
    private float fireResistance;
    public float bonusFResistance;

    public float FireResistance
    {
        get { return (fireResistance + bonusFResistance) / 100.0f; }
        set { fireResistance = value; }
    }

    //A % damage reduction against damage type air
    private float airResistance;
    public float bonusAResistance;

    public float AirResistance
    {
        get { return (airResistance + bonusAResistance) / 100.0f; }
        set { airResistance = value; }
    }

    //A % damage reduction against damage type earth
    private float earthResistance;
    public float bonusEResistance;

    public float EarthResistance
    {
        get { return (earthResistance + bonusEResistance) / 100.0f; }
        set { earthResistance = value; }
    }

    //A % damage reduction against damage type water
    private float waterResistance;
    public float bonusWResistance;

    public float WaterResistance
    {
        get { return (waterResistance + bonusWResistance) / 100.0f; }
        set { waterResistance = value; }
    }

    //A % damage reduction against damage type life
    private float lifeResistance;
    public float bonusLResistance;

    public float LifeResistance
    {
        get { return (lifeResistance + bonusLResistance) / 100.0f; }
        set { lifeResistance = value; }
    }

    //A % damage reduction against damage type death
    private float deathResistance;
    public float bonusDResistance;

    public float DeathResistance
    {
        get { return (deathResistance + bonusDResistance) / 100.0f; }
        set { deathResistance = value; }
    }

    #endregion

    //A collection to contain all of the players spells
    public List<Spell> SpellBook;

    //A list to track and update the timers for the spells that are on cooldown
    public List<Spell> spellsOnCooldown;

    //This is from the $P library and will be used to store symbol positions
    private Gesture[] spellSymbols;

    //In the update the active spell, if not null, will do stuff
    public Spell activeSpell;

    //Test spells
    public FireBallBasic spell;
    public WaterBoltBasic spell2;
    public RockBasic spell3;

    //A reference to this characters opponent
    public Player otherPlayer;

    //An object to manage all of the players current status effects
    public StatusManager statusManager;

    //An interger representation of being player 1 or 2
    public int playerIndex;

    public Sprite CharacterSprite;

    #endregion

    #region Methods

    /**************************************************************************************************************************************/
    /********************************************************* Methods ********************************************************************/
    /**************************************************************************************************************************************/

    //Called whenever damage to the player is dealt. Deals with all resistances
    //and application of secondary effects
    public void onDamage(Damage _damageRecieved)
    {
        //make a modifiable copy of the damages amount
        float damageToDeal = _damageRecieved.damageAmount;

        //A switch to check the elemental type of the damage and apply
        //appropriate damage resistances to said number
        switch(_damageRecieved.damageType)
        {
            case DAMAGE_TYPE.DAMAGE_TYPE_AIR:
                damageToDeal -= (damageToDeal * AirResistance);
                break;
            case DAMAGE_TYPE.DAMAGE_TYPE_EARTH:
                damageToDeal -= (damageToDeal * EarthResistance);
                break;
            case DAMAGE_TYPE.DAMAGE_TYPE_FIRE:
                damageToDeal -= (damageToDeal * FireResistance);
                break;
            case DAMAGE_TYPE.DAMAGE_TYPE_WATER:
                damageToDeal -= (damageToDeal * WaterResistance);
                break;
            case DAMAGE_TYPE.DAMAGE_TYPE_LIFE:
                damageToDeal -= (damageToDeal * LifeResistance);
                break;
            case DAMAGE_TYPE.DAMAGE_TYPE_DEATH:
                damageToDeal -= (damageToDeal * DeathResistance);
                break;
        }

        if(_damageRecieved.statusEffect != null)
        {
            _damageRecieved.statusEffect.Apply();
        }

        //Subtract damage from health
        currentHealth -= damageToDeal;

        //Set the UI health bars
        if (playerIndex == 0) uis.setPlayerOneHealth(currentHealth / health);
        else uis.setPlayerTwoHealth(currentHealth / health);

        //If the damage interupts, then you know.. interupt them
        if (_damageRecieved.interups && activeSpell != null) activeSpell = activeSpell.CancelSpell(this);
    }

    //Used by the GameStartUp script to Initialize all aspects of the player class.
    public void init(int _playerIndex, int _health)
    {
        pnb = GetComponent<PlayerNetworkBehavior>();

        playerIndex = _playerIndex;

        health = _health;
        currentHealth = health;

        mana = 100;
        manaRegen = 1.5f;

        statusManager = new StatusManager();

        SpellBook = new List<Spell>();
        spellsOnCooldown = new List<Spell>();

        spell = new FireBallBasic(this);
        SpellBook.Add(spell);
        spell2 = new WaterBoltBasic(this);
        SpellBook.Add(spell2);
        spell3 = new RockBasic(this);
        SpellBook.Add(spell3);

        Stunned = false;
        Immune = false;

        //Populates the Gesture array by passing it the points from all the spells
        //that exist in the spell book and assigns a string identifier to each
        spellSymbols = new Gesture[]
        {
            new Gesture(SpellBook[0].targetPoints, "spell1"),
            new Gesture(SpellBook[1].targetPoints, "spell2"),
            new Gesture(SpellBook[2].targetPoints, "spell3")
        };

        //Grab the UI script from the only Canvas in the level
        uis = GameObject.Find("UI Prefab").GetComponent<UIScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //check if the active spell has been set and if it has
        //update it accordingly
        if (activeSpell != null)
        {
            activeSpell = activeSpell.OnCast(otherPlayer);
        }

        statusManager.UpdateEffects();
        
        //This is just boring mana regen crap
        if (mana < 100)
        {
            mana += ManaRegen / (1.0f / Time.deltaTime);

            if (mana > 100) mana = 100;

            if (playerIndex == 0) uis.setPlayerOneMana(mana / 100.0f);
            else uis.setPlayerTwoMana(mana / 100.0f);
        }

        //Regens health if you got health regen
        if (healthRegen != 0)
            if (currentHealth < Health)
            {
                currentHealth += healthRegen / (1.0f / Time.deltaTime);

                if (currentHealth >= Health) currentHealth = Health;

                if (playerIndex == 0) uis.setPlayerOneHealth(currentHealth / 100.0f);
                else uis.setPlayerTwoHealth(currentHealth / 100.0f);
            }

        for (int i = spellsOnCooldown.Count - 1; i >= 0; i--)
        {
            if (!spellsOnCooldown[i].ManageCoolDown()) spellsOnCooldown.Remove(spellsOnCooldown[i]);
        }
        
    }

    /*
     * Will be used to detect whether or not the player is 
     * currently casting a spell. If they are they won't be
     * allowed to draw
    */
    public bool LockedOut()
    {
        return (activeSpell != null || Stunned == true);
    }

    public void checkDraw(Vector3[] _points)
    {
        /* All of this is using the $P stroke recognition library */

        //Translate the given 3D vectors into an array of Points as Points
        //is what $P uses for recognition
        Point[] _tempPoints = new Point[_points.Length];

        for (int i = 0; i < _tempPoints.Length; i++)
        {
            _tempPoints[i] = new Point(_points[i].x, _points[i].y, 0);
        }

        //Creates a gesture out of our new points so we can compare them to a pre-determined
        //set of other gestures
        Gesture candidate = new Gesture(_tempPoints);

        //Compares the canditate to our stored symbols and returns the string id
        //of the closest symbol to the candidate
        string spellHit = PointCloudRecognizer.Classify(candidate, spellSymbols);

        //Do a switch to check what the string is, and set the activeSpell to the
        //coresponding spell in the spell book
        switch (spellHit)
        {
            case "spell1":

                if (!SpellBook[0].IsOnCooldown() && mana >= SpellBook[0].manaCost)
                {
                    pnb.SetSpellIndex(0);
                    activeSpell = SpellBook[0];
                    mana -= SpellBook[0].manaCost;
                    uis.setPlayerOneMana(mana);
                }

                break;
            case "spell2":

                if (!SpellBook[1].IsOnCooldown() && mana >= SpellBook[1].manaCost)
                {
                    pnb.SetSpellIndex(1);
                    activeSpell = SpellBook[1];
                    mana -= SpellBook[1].manaCost;
                    uis.setPlayerOneMana(mana);
                }

                break;
            case "spell3":

                if (!SpellBook[2].IsOnCooldown() && mana >= SpellBook[2].manaCost)
                {
                    pnb.SetSpellIndex(2);
                    activeSpell = SpellBook[2];
                    mana -= SpellBook[2].manaCost;
                    uis.setPlayerOneMana(mana);
                }

                break;
            default:
                break;
        }
    }

    #endregion
}
