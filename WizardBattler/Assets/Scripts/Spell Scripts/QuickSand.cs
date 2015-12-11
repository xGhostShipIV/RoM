using UnityEngine;
using PDollarGestureRecognizer;
using System.Collections;

public class QuickSand : Spell {

	public QuickSand(Player _caster) : base(_caster)
    {
        SpellName = "Quicksand";

        targetPoints = new Point[]
        {
            new Point(0,0,0),
            new Point(1, 2,0),
            new Point(2, 2.5f,0),
            new Point(3,2,0),
            new Point(2.9f,1.9f,0),
            new Point(2,0,0),
            new Point(1,1,0),
            new Point(2, 1.5f,0),
            new Point(2,1,0)
        };

        manaCost = 10.0f;

        elapsedTime = 0.0f;
        timeToCast = 3.0f;

        coolDown = 2.0f;
        coolDownTimer = coolDown;

        //Loads the sprite into memory as runtime
        thumbnail = Resources.Load<Sprite>("rockthumbnail");
    }

    public override Spell OnCast(Player _target)
    {
        uis.HandleCastBarInit(caster.playerIndex, thumbnail);

        if (elapsedTime >= TIME_TO_CAST)
        {
            Crush crush = new Crush(caster, caster.otherPlayer, EFFECT_TYPE.TYPE_DEBUFF, 100, 4, 3.5f);
            crush.Apply();

            Haste haste = new Haste(caster, caster.otherPlayer, EFFECT_TYPE.TYPE_DEBUFF, 100, -25, 4.0f);
            haste.Apply();

            elapsedTime = 0.0f;

            uis.HandleCastBarToggle(caster.playerIndex);


            coolDownTimer = 0.0f;
            caster.spellsOnCooldown.Add(this);

            //Because the player is updating its "activeSpell" when the spell
            //is done casting null is returned so that the spell is no longer
            //continuing to update
            return null;

        }
        else
        {
            elapsedTime += Time.deltaTime;

            uis.FillCastBar(caster.playerIndex, elapsedTime, TIME_TO_CAST);
        }

        return this;
    }

    public override Spell CancelSpell(Player _victim)
    {
        uis.HandleCastBarToggle(_victim.playerIndex);
        return null;
    }
}
