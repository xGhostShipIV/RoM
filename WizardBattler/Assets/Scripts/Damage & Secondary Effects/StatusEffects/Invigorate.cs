using UnityEngine;
using System.Collections;

public class Invigorate : StatusEffect {

    float mp5;
    float Mp5
    {
        get { return mp5 + (mp5 * caster.BuffIntesity); }
    }

    public Invigorate(Player _caster, Player _subject, EFFECT_TYPE _type, float _chanceToApply, float _duration, float _mp5) : base(_caster, _subject, _type, _chanceToApply)
    {
        mp5 = _mp5;
        baseDuration = _duration;

        icon = Resources.Load<Sprite>("waterboltthumbnail");
    }

    public override void OnApply()
    {
        base.OnApply();

        caster.bonusManaRegen += Mp5;
    }

    public override void OnFallOff()
    {
        base.OnFallOff();

        caster.bonusManaRegen -= Mp5;
    }
	
}
