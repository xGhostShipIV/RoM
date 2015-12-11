using UnityEngine;
using System.Collections;

public class Moist : StatusEffect {

    private float howMoist;
    public float HowMoist
    {
        get { return howMoist + (howMoist * caster.Malice); }
    }
    public Moist(Player _caster, Player _subject, EFFECT_TYPE _type, float _intensity, float _chanceToApply, float _duration)
        : base(_caster, _subject, _type, _chanceToApply)
    {
        baseDuration = _duration;
        howMoist = _intensity;

        icon = Resources.Load<Sprite>("fireballthumbnail");
    }

    public override void OnApply()
    {
        base.OnApply();

        subject.AirResistance -= HowMoist;
    }

    public override void OnFallOff()
    {
        base.OnFallOff();

        subject.AirResistance += HowMoist;

    }
}
