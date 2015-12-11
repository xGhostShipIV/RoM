using UnityEngine;
using PDollarGestureRecognizer;
using System.Collections;

public class GraspingHands : Spell {

    public GraspingHands(Player _caster) : base(_caster)
    {
        SpellName = "GraspingHands";

        targetPoints = new Point[]
        {
            new Point(0,0,0),
            new Point(0,-3,0),
            new Point(1.5f, -1, 0),
            new Point(3, -3, 0),
            new Point(3, 0, 0)
        };

        manaCost = 15;

        coolDown = 4.0f;
        coolDownTimer = coolDown;

        timeToCast = 4.5f;
        elapsedTime = 0.0f;
    }

    public override Spell OnCast(Player _target)
    {
        uis.HandleCastBarInit(caster.playerIndex, thumbnail);

        if (elapsedTime >= TIME_TO_CAST)
        {
            Pain p = new Pain(caster, caster.otherPlayer, EFFECT_TYPE.TYPE_DEBUFF, 66.66f, 4.0f, 5.0f);
            p.Apply();

            Haste h = new Haste(caster, caster.otherPlayer, EFFECT_TYPE.TYPE_DEBUFF, 100, -20, 6.0f);
            h.Apply();

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
