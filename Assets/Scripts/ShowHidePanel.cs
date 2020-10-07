using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShowHidePanel : MonoBehaviour
{
    public enum PanelType {animatedPanels, mainPanel};
    public PanelType type;

    GameObject MainPanel;
    GameObject DeathPanel;

    Animation anim;
    List<AnimationState> states;

    public void Awake()
    {
        if (type == PanelType.animatedPanels)
        {
            anim = GetComponent<Animation>();
            states = new List<AnimationState>(anim.Cast<AnimationState>());
            Hide();
        }
        else
        {
            MainPanel = transform.GetChild(0).gameObject;
            DeathPanel = transform.GetChild(1).gameObject;
        }
    }

    public void Hide()
    {
        anim.Play(states[0].name);
    }

    public void Show()
    {
        anim.Play(states[1].name);
    }

    public void OnDeath()
    {
        MainPanel.SetActive(false);
        DeathPanel.SetActive(true);
    }

    public IEnumerator PlayAnimAfterDelay (bool show, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (show) Show(); else Hide();
    }
}
