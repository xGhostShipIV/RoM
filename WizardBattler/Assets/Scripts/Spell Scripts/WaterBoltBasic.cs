using UnityEngine;
using PDollarGestureRecognizer;
using System.Collections;

public class WaterBoltBasic : Spell
{
    public GameObject waterBoltPrefab;
    private GameObject waterBolt;

    public WaterBoltBasic(Player _caster) : base(_caster)
    {
        SpellName = "WaterBoltBasic";

        targetPoints = new Point[]
        {
            new Point(0,0,0),
            new Point(5,-5,0),
            new Point(10,-10,0),
            new Point(5,-10,0),
            new Point(0,-10,0),
            new Point(5,-5,0),
            new Point(10,0,0)
        };

        manaCost = 30.0f;

        damage = new Damage(16, false, DAMAGE_TYPE.DAMAGE_TYPE_WATER, null);

        elapsedTime = 0.0f;
        timeToCast = 2.5f;

        coolDown = 3.0f;
        coolDownTimer = coolDown;

        thumbnail = Resources.Load<Sprite>("waterboltthumbnail");
        waterBoltPrefab = Resources.Load<GameObject>("waterBoltPrefab");
    }

    public override Spell OnCast(Player _target)
    {
        if(waterBolt != null)
        {
            waterBolt.transform.Rotate(Vector3.up, 12, Space.Self);
        }

        uis.HandleCastBarInit(caster.playerIndex, thumbnail);

        if (elapsedTime >= TIME_TO_CAST)
        {
            if (waterBolt.activeInHierarchy)
            {
                Vector3 direction = caster.otherPlayer.transform.position - waterBolt.transform.position;

                if (direction.magnitude < 1.0f)
                {
                    waterBolt.SetActive(false);
                }
                else
                {
                    waterBolt.transform.position += direction.normalized;
                }

            }
            else
            {
                _target.onDamage(damage);
                elapsedTime = 0.0f;

                uis.HandleCastBarToggle(caster.playerIndex);

                GameObject.Destroy(waterBolt);

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
            if (waterBolt == null)
            {
                waterBolt = GameObject.Instantiate(waterBoltPrefab);
                waterBolt.transform.localScale = new Vector3(0, 0, 0);

                if (caster.playerIndex == 0) spellPosition = caster.transform.position + new Vector3(3.0f, 0, 0);
                else spellPosition = caster.transform.position - new Vector3(3.0f, 0, 0);

                waterBolt.transform.position = spellPosition;
            }

            Vector3 scaleTime = new Vector3(elapsedTime / TIME_TO_CAST, elapsedTime / TIME_TO_CAST, elapsedTime / TIME_TO_CAST);
            waterBolt.transform.localScale = scaleTime;


            elapsedTime += Time.deltaTime;

            uis.FillCastBar(caster.playerIndex, elapsedTime, TIME_TO_CAST);
        }

        return this;
    }

    public override Spell CancelSpell(Player _victim)
    {
        uis.HandleCastBarToggle(_victim.playerIndex);
        GameObject.Destroy(waterBolt);
        return null;
    }
}
