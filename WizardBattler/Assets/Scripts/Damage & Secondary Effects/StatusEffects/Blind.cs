using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Blind : StatusEffect {

    float imageFade = 1.0f;
    public Image blindScreen;

    public Blind(Player _caster, Player _subject, EFFECT_TYPE _type, float _chanceToApply, float _duration) : base(_caster, _subject, _type, _chanceToApply)
    {
        blindScreen = GameObject.Find("UI Prefab").GetComponent<UIScript>().blindScreen;
        baseDuration = _duration;
        icon = Resources.Load<Sprite>("waterboltthumbnail");
    }

    public override void OnApply()
    {
        base.OnApply();

        if(subject.playerIndex == 0)
            blindScreen.enabled = true;
    }

    public override void OnTick()
    {
        base.OnTick();

        float completionPercent = elapsedTime / Duration;

        imageFade = 1 - (completionPercent * completionPercent);
        blindScreen.CrossFadeAlpha(imageFade, 0, false);
    }

    public override void OnFallOff()
    {
        base.OnFallOff();

        if (subject.playerIndex == 0)
            blindScreen.enabled = false;
    }
}
