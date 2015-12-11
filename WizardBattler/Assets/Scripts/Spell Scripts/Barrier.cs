using UnityEngine;
using PDollarGestureRecognizer;
using System.Collections;

public class Barrier : Spell
{
    public Barrier(Player _caster)
        : base(_caster)
    {
        SpellName = "Barrier";

        targetPoints = new Point[36];

        float theta = 0;

        for (int i = 0; i < targetPoints.Length; i++)
        {
            targetPoints[i] = new Point(Mathf.Cos(theta), Mathf.Sin(theta), 0);
            theta += Mathf.PI * 2 / targetPoints.Length;
        }

        elapsedTime = 0.0f;
        manaCost = 25.0f;

        coolDown = 10.0f;
        coolDownTimer = coolDown;

        timeToCast = 0.75f;

        thumbnail = Resources.Load<Sprite>("waterboltthumbnail");
    }

    public override Spell OnCast(Player _target)
    {
        uis.HandleCastBarInit(caster.playerIndex, thumbnail);

        if (elapsedTime >= TIME_TO_CAST)
        {
            Shield shield = new Shield(caster, caster, EFFECT_TYPE.TYPE_BUFF, 100, 5.0f);
            shield.Apply();

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
