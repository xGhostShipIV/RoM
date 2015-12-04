using UnityEngine;
using PDollarGestureRecognizer;
using System.Collections;

public class RockBasic : Spell
{
    public GameObject rockPrefab;
    private GameObject actualRock;

    public RockBasic(Player _caster) : base(_caster)
    {
        SpellName = "RockBasic";

        targetPoints = new Point[]
        {
            new Point(0,0,0),
            new Point(5, 5,0),
            new Point(10, 0,0),
            new Point(15, 5,0)
        };

        manaCost = 10.0f;

        damage = new Damage(10, false, DAMAGE_TYPE.DAMAGE_TYPE_EARTH, new Stun(caster, caster.otherPlayer, EFFECT_TYPE.TYPE_DEBUFF, 20, 1.0f));

        elapsedTime = 0.0f;
        timeToCast = 0.5f;

        coolDown = 3.0f;
        coolDownTimer = coolDown;

        thumbnail = Resources.Load<Sprite>("rockthumbnail");
        rockPrefab = Resources.Load<GameObject>("rockPrefab");
    }

    public override Spell OnCast(Player _target)
    {
        uis.HandleCastBarInit(caster.playerIndex, thumbnail);

        if (elapsedTime >= TIME_TO_CAST)
        {
            if (actualRock.activeInHierarchy)
            {
                Vector3 direction = caster.otherPlayer.transform.position - actualRock.transform.position;

                if (direction.magnitude < 1.0f)
                {
                    actualRock.SetActive(false);
                }
                else
                {
                    actualRock.transform.position += direction.normalized;
                }

            }
            else
            {
                _target.onDamage(damage);
                elapsedTime = 0.0f;

                uis.HandleCastBarToggle(caster.playerIndex);

                GameObject.Destroy(actualRock);

                coolDownTimer = 0.0f;
                caster.spellsOnCooldown.Add(this);

                return null;
            }
        }
        else
        {
            if(actualRock == null)
            {
                actualRock = GameObject.Instantiate(rockPrefab);

                if (caster.playerIndex == 0) spellPosition = caster.transform.position + new Vector3(3.0f, 0, 0);
                else spellPosition = caster.transform.position - new Vector3(3.0f, 0, 0);

                actualRock.transform.position = spellPosition - new Vector3(0, 4.0f, 0);
            }

            actualRock.transform.position = Vector3.Lerp(actualRock.transform.position, spellPosition, elapsedTime / TIME_TO_CAST);
            elapsedTime += Time.deltaTime;

            uis.FillCastBar(caster.playerIndex, elapsedTime, TIME_TO_CAST);
        }

        return this;
    }

    public override Spell CancelSpell(Player _victim)
    {
        uis.HandleCastBarToggle(_victim.playerIndex);
        GameObject.Destroy(actualRock);
        return null;
    }
}
