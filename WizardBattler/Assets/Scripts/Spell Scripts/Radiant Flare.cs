using UnityEngine;
using PDollarGestureRecognizer;
using System.Collections;

public class RadiantFlare : Spell
{

    public RadiantFlare(Player _caster)
        : base(_caster)
    {
        SpellName = "RadiantFlare";

        targetPoints = new Point[]
        {
            new Point(0,0,0),
            new Point(0,-3,0),
            new Point(1, -2.5f, 0),
            new Point(1, -1.5f, 0)
        };

        manaCost = 20.0f;

        timeToCast = 2.0f;
        elapsedTime = 0.0f;

        coolDown = 6.0f;
        coolDownTimer = coolDown;

        damage = new Damage(15, false, DAMAGE_TYPE.DAMAGE_TYPE_LIFE, new Blind(caster, caster.otherPlayer, EFFECT_TYPE.TYPE_DEBUFF, 50.0f, 5f));

        thumbnail = Resources.Load<Sprite>("waterboltthumbnail");
    }

    public override Spell OnCast(Player _target)
    {
        uis.HandleCastBarInit(caster.playerIndex, thumbnail);

        if (elapsedTime >= TIME_TO_CAST)
        {
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
