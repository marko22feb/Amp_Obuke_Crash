using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShowHidePanel : MonoBehaviour
{
    Animation anim;
    List<AnimationState> states;

    public void Awake()
    {
        anim = GetComponent<Animation>();
        states = new List<AnimationState>(anim.Cast<AnimationState>());
        Hide();
    }

    public void Hide()
    {
        anim.Play(states[0].name);
    }

    public void Show()
    {
        anim.Play(states[1].name);
    }

    public IEnumerator PlayAnimAfterDelay (bool show, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (show) Show(); else Hide();
    }
}
