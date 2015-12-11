using UnityEngine;
using System.Collections;

public class CDR : StatusEffect
{

    private float intensity;
    public float Intensity
    {
        get { return intensity + (intensity * caster.BuffIntesity); }
    }

    public CDR(Player _caster, Player _subject, EFFECT_TYPE _type, float _chanceToApply, float _intensity, float _duration)
        : base(_caster, _subject, _type, _chanceToApply)
    {
        baseDuration = _duration;
        intensity = _intensity;

        icon = Resources.Load<Sprite>("waterboltthumbnail");
    }

    public override void OnApply()
    {
        base.OnApply();

        subject.bonusCooldownReduction += Intensity;
    }

    public override void OnFallOff()
    {
        base.OnFallOff();

        subject.bonusCooldownReduction -= Intensity;
    }
}
