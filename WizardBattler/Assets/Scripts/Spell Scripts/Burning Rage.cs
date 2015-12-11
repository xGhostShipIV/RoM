using UnityEngine;
using PDollarGestureRecognizer;
using System.Collections;

public class BurningRage : Spell
{

    public BurningRage(Player _caster)
        : base(_caster)
    {
        SpellName = "BurningRage";

        targetPoints = new Point[]
        {
            new Point(1.5f,0,0),
            new Point(0, 2.0f,0),
            new Point(1, 1.5f,0),
            new Point(1.5f, 3.0f,0),
            new Point(2,1.5f,0),
            new Point(3, 2.0f,0),
            new Point(1.5f, 0,0)
        };

        manaCost = 20.0f;

        elapsedTime = 0.0f;
        timeToCast = 1.5f;

        coolDown = 5.0f;
        coolDownTimer = coolDown;

        thumbnail = Resources.Load<Sprite>("fireballthumbnail");
    }

    public override Spell OnCast(Player _target)
    {
        uis.HandleCastBarInit(caster.playerIndex, thumbnail);

        if (elapsedTime >= TIME_TO_CAST)
        {
            Burn burn = new Burn(caster, caster, EFFECT_TYPE.TYPE_DEBUFF, 100, 4.0f, 4.5f);
            burn.Apply();

            Haste haste = new Haste(caster, caster, EFFECT_TYPE.TYPE_BUFF, 100, 25, 6.0f);
            haste.Apply();

            CDR cdr = new CDR(caster, caster, EFFECT_TYPE.TYPE_BUFF, 100, 25, 6.0f);
            cdr.Apply();

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
