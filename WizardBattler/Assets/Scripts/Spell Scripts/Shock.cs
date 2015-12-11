using UnityEngine;
using PDollarGestureRecognizer;
using System.Collections;
using System.Collections.Generic;

public class Shock : Spell
{
    LineRenderer lr;
    List<Vector3> points;
    float iterator = 0.0f;

    public Shock(Player _caster)
        : base(_caster)
    {
        SpellName = "Shock";

        targetPoints = new Point[]
        {
            new Point(0,0,0),
            new Point(1.5f, -1, 0),
            new Point(0.5f, -2,0),
            new Point(2, -3, 0)
        };

        manaCost = 20;

        timeToCast = 2.5f;
        elapsedTime = 0.0f;

        coolDown = 5.5f;
        coolDownTimer = coolDown;

        damage = new Damage(15, false, DAMAGE_TYPE.DAMAGE_TYPE_AIR, new Stun(caster, caster.otherPlayer, EFFECT_TYPE.TYPE_DEBUFF, 33.33f, 2.5f));

        spellPosition = caster.otherPlayer.gameObject.transform.position + new Vector3(0, 5, 0);

        points = new List<Vector3>();
        points.Add(spellPosition);
        //points.Add(spellPosition);
    }

    public override Spell OnCast(Player _target)
    {
        uis.HandleCastBarInit(caster.playerIndex, thumbnail);
        lr = Camera.main.GetComponent<MouseDraw>().lr;

        if (elapsedTime >= TIME_TO_CAST)
        {
            if (iterator < 1)
            {
                float x = Random.Range(-0.5f, 0.5f);
                caster.pnb.CmdRandomSeed(Random.seed);

                Vector3 lerped = Vector3.Lerp(spellPosition, caster.otherPlayer.gameObject.transform.position, iterator);
                lerped.x += x;
                points.Add(lerped);
                lr.SetVertexCount(points.Count);

                lr.SetPosition(points.Count - 2, points[points.Count - 2]);
                lr.SetPosition(points.Count - 1, points[points.Count - 1]);

                iterator += 0.075f;
            }
            else
            {
                _target.onDamage(damage);
                elapsedTime = 0.0f;

                uis.HandleCastBarToggle(caster.playerIndex);

                points.Clear();
                lr.SetVertexCount(0);

                spellPosition = caster.otherPlayer.gameObject.transform.position + new Vector3(0, 5, 0);
                iterator = 0.0f;

                points.Add(spellPosition);

                coolDownTimer = 0.0f;
                caster.spellsOnCooldown.Add(this);

                //Because the player is updating its "activeSpell" when the spell
                //is done casting null is returned so that the spell is no longer
                //continuing to update
                return null;
            }
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
