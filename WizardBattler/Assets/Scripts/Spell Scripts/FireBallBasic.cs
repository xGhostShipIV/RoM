using UnityEngine;
using PDollarGestureRecognizer;
using UnityEngine.UI;
using System.Collections;

public class FireBallBasic : Spell
{

    public GameObject fireBallPrefab;
    private GameObject fireBall;

    public FireBallBasic(Player _caster) : base(_caster)
    {
        SpellName = "FireBallBasic";

        targetPoints = new Point[]
        {
            new Point(0, 0, 0),
            new Point(5, 0, 0),
            new Point(10, 0, 0),
            new Point(10, -5, 0),
            new Point(10, -10, 0),
            new Point(5, -10, 0),
            new Point(0, -10, 0),
            new Point(0, -5, 0)
        };

        manaCost = 10.0f;
        damage = new Damage(10, false, DAMAGE_TYPE.DAMAGE_TYPE_FIRE, new Burn(caster, caster.otherPlayer, EFFECT_TYPE.TYPE_DEBUFF, 100, 0.5f, 2.0f));

        elapsedTime = 0.0f;
        timeToCast = 3.0f;

        coolDown = 2.0f;
        coolDownTimer = coolDown;

        //Loads the sprite into memory as runtime
        thumbnail = Resources.Load<Sprite>("fireballthumbnail");

        fireBallPrefab = Resources.Load<GameObject>("fireBallBasic");
    }

    public override Spell OnCast(Player _target)
    {
        //If the cast bar is invisible make it visible and initilizes
        //base values to what is desired by the spell
        uis.HandleCastBarInit(caster.playerIndex, thumbnail);

        //Acts as a timer to decide whether or not the spell has been
        //fully cast. If it has apply damage and what not
        if (elapsedTime >= TIME_TO_CAST)
        {
            if (fireBall.activeInHierarchy)
            {
                Vector3 direction = caster.otherPlayer.transform.position - fireBall.transform.position;
 
                if(direction.magnitude < 1.0f)
                {
                    fireBall.SetActive(false);
                }
                else
                {
                    fireBall.transform.position += direction.normalized;
                }

            }
            else
            {
                _target.onDamage(damage);
                elapsedTime = 0.0f;

                uis.HandleCastBarToggle(caster.playerIndex);

                GameObject.Destroy(fireBall);

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
            if (fireBall == null)
            {
                fireBall = GameObject.Instantiate(fireBallPrefab);
                fireBall.transform.localScale = new Vector3(0, 0, 0);

                if (caster.playerIndex == 0) spellPosition = caster.transform.position + new Vector3(3.0f, 0, 0);
                else spellPosition = caster.transform.position - new Vector3(3.0f, 0, 0);

                fireBall.transform.position = spellPosition;
            }

            Vector3 scaleTime = new Vector3(elapsedTime / TIME_TO_CAST, elapsedTime / TIME_TO_CAST, elapsedTime / TIME_TO_CAST);
            fireBall.transform.localScale = scaleTime;

            elapsedTime += Time.deltaTime;

            //update the moving UI elements to reflect progression of the
            //spells casting
            uis.FillCastBar(caster.playerIndex, elapsedTime, TIME_TO_CAST);
        }

        return this;
    }

    public override Spell CancelSpell(Player _victim)
    {
        uis.HandleCastBarToggle(_victim.playerIndex);
        GameObject.Destroy(fireBall);
        return null;
    }
}
