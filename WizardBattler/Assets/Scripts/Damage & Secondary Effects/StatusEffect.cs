using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum EFFECT_TYPE
{
    TYPE_BUFF,
    TYPE_DEBUFF
}

public class StatusEffect
{

    //A reference to the player who is applying the effect
    protected Player caster;

    //A reference to the player that this effect is attached too
    protected Player subject;

    //A reference to the UI so icons can be managed
    public UIScript uis;

    //A reference to the blip on the UI to show duration
    public Image UIElement;

    //An icon to represent the effect
    protected Sprite icon;

    //A number between 1 & 100 to determine if the effect is successfully added
    protected float chanceToApply;

    //A variable to track elapsedTime
    public float elapsedTime;

    //An enumeration to represent whether or not the effect is a buff or debuff
    public EFFECT_TYPE type;

    //Variables to represent overall duration of the status effect
    //Property returns value modified by the casters stats
    protected float baseDuration;
    public float Duration
    {
        get { return baseDuration + (baseDuration * caster.Focus); }
        set { baseDuration = value; }
    }

    public StatusEffect(Player _caster, Player _subject, EFFECT_TYPE _type, float _chanceToApply)
    {
        caster = _caster;
        subject = _subject;

        elapsedTime = 0;

        chanceToApply = _chanceToApply;
        type = _type;

        uis = GameObject.Find("UI Prefab").GetComponent<UIScript>();
    }

    //Picks a random number between 1 & 100. If its less than the % chance to apply it will
    //attempt to add the effect to the players list of effects.
    public void Apply()
    {
        int i = Random.Range(0, 101);

        if (i < chanceToApply)
        {
            //If the type is a debuff, roll again and check against the subjects fortitude
            //if you roll less apply debuff
            if (type == EFFECT_TYPE.TYPE_DEBUFF)
            {
                if (Random.Range(0, 101) > subject.Fortitude)
                {
                    subject.statusManager.Add(this);
                }
            }
            else subject.statusManager.Add(this);
        }
    }

    //Manages the timers and calls appropriate time related methods of the effect
    public bool Update()
    {
        if (elapsedTime >= Duration)
        {
            OnFallOff();

            //Make the UIElement disappear when the effect falls off
            UIElement.enabled = false;
            UIElement = null;

            return false;
        }
        else
        {
            //Update the images fill amount
            UIElement.fillAmount = 1 - (elapsedTime / Duration);

            //Debug.Log(UIElement.fillAmount);

            OnTick();
            elapsedTime += Time.deltaTime;
            return true;
        }
    }

    //Will be called IMMEDIATELY when the effect is applied to a character
    //This is only called ONCE. Do not use for events that happen per tick
    public virtual void OnApply()
    {
        elapsedTime = 0;

        if(type == EFFECT_TYPE.TYPE_BUFF)
        {
            UIElement = uis.GetNextAvailableBuff(subject.playerIndex);
        }
        else
        {

            UIElement = uis.GetNextAvailableDebuff(subject.playerIndex);
        }

        UIElement.sprite = icon;
        UIElement.fillAmount = 1;
        UIElement.enabled = true;
    }

    //Will be called IMMEDIATELY and only ONCE when the effect is determined to have
    //run its course. Undo OnApply effects and remove from player here
    public virtual void OnFallOff()
    {

    }

    //Will be called each second that the effect is applied
    public virtual void OnTick()
    {
    }
}
