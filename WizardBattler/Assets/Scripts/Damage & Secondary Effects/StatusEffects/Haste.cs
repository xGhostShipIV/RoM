using UnityEngine;
using System.Collections;

public class Haste : StatusEffect {

    private float intensity;
    public float Intensity
    {
        get { return intensity + (intensity * caster.BuffIntesity); }
    }

    public Haste(Player _caster, Player _subject, EFFECT_TYPE _type, float _chanceToApply, float _intensity, float _duration) : base(_caster, _subject, _type, _chanceToApply)
    {
        baseDuration = _duration;
        intensity = _intensity;
    }

    public override void OnApply()
    {
        base.OnApply();

        subject.bonusHaste += Intensity;
    }

    public override void OnFallOff()
    {
        base.OnFallOff();

        subject.bonusHaste -= Intensity;
    }
}
