using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int WhumpaFruitCount = 0;
    public int extraLives = 0;
    public Text WhumpaCountText;
    public Text ExtraLivesText;
    public static GameController control;

    public void Awake()
    {
        DontDestroyOnLoad(this);

        if (control == null)
        {
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }

        WhumpaCountText = GameObject.Find("Text_WhumpaFruitCount").GetComponent<Text>();
        if (WhumpaFruitCount < 10)
            WhumpaCountText.text = "0" + WhumpaFruitCount;
        else WhumpaCountText.text = "" + WhumpaFruitCount;

        ExtraLivesText = GameObject.Find("Text_ExtraLivesCount").GetComponent<Text>();
        if (extraLives < 10)
            ExtraLivesText.text = "0" + extraLives;
        else ExtraLivesText.text = "" + extraLives;
    }

    public void AddWhumpaFruids(int amount)
    {
        WhumpaFruitCount += amount;
        if (WhumpaFruitCount > 99)
        {
            WhumpaFruitCount = 0; AddExtraLives(1);
        }
        if (WhumpaFruitCount < 10)
        WhumpaCountText.text = "0" + WhumpaFruitCount;
        else WhumpaCountText.text = "" + WhumpaFruitCount;
    }

    public void AddExtraLives(int amount)
    {
        extraLives += amount;

        if (extraLives < 10)
            ExtraLivesText.text = "0" + extraLives;
        else ExtraLivesText.text = "" + extraLives;

        ExtraLivesText.transform.parent.GetComponent<ShowHidePanel>().Show();
        StartCoroutine(ExtraLivesText.transform.parent.GetComponent<ShowHidePanel>().PlayAnimAfterDelay(false, 3f));
    }
}
