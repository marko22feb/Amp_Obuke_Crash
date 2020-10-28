using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainToolTip : MonoBehaviour
{
    public NavigationItem selectednavItem;

    public void FirstClick()
    {
        selectednavItem.GetComponent<Image>().color = Color.red;
    }

    public void SecondClick()
    {
        selectednavItem.GetComponent<Image>().color = Color.green;
    }

    public void ThirdClick()
    {
        selectednavItem.GetComponent<Image>().color = Color.black;
    }
}
