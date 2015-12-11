using UnityEngine;
using PDollarGestureRecognizer;
using System.Collections;

public class Awe : Spell {

    public Awe(Player _caster) : base(_caster)
    {
        SpellName = "Awe";

        targetPoints = new Point[]
        {
            new Point(0,0,0),
            new Point(3,0,0),
            new Point(2.5f, -1, 0),
            new Point(2, -2, 0),
            new Point(1, -3,0)
        };

        manaCost = 25.0f;

        timeToCast = 0.75f;
        elapsedTime = 0.0f;

        coolDown = 4.0f;
        coolDownTimer = coolDown;

        damage = new Damage(4, false, DAMAGE_TYPE.DAMAGE_TYPE_AIR, null);
    }

    public override Spell OnCast(Player _target)
    {
        uis.HandleCastBarInit(caster.playerIndex, thumbnail);

        if (elapsedTime >= TIME_TO_CAST)
        {
            if (_target.Stunned) damage.damageAmount += 16;

            _target.onDamage(damage);
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
