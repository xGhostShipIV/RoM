using UnityEngine;
using System.Collections;

public class Silence : StatusEffect {

    public Silence(Player _caster, Player _subject, EFFECT_TYPE _type, float _chanceToApply, float _duration) : base(_caster, _subject, _type, _chanceToApply)
    {
        baseDuration = _duration;
        icon = Resources.Load<Sprite>("rockthumbnail");
    }

    public override void OnApply()
    {
        base.OnApply();

        if (subject.activeSpell != null) subject.activeSpell = subject.activeSpell.CancelSpell(subject);
        subject.Silenced = true;
    }

    public override void OnFallOff()
    {
        base.OnFallOff();

        subject.Silenced = false;
    }
}
