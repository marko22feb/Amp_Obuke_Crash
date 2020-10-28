using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTip : MonoBehaviour
{
    public GameObject toolTip;
    public void OnClicked()
    {
        toolTip.SetActive(true);
        GameController.control.GetComponent<NavigationManager>().NewSelection(toolTip.GetComponent<NavigationLayoutObject>());
        toolTip.GetComponent<MainToolTip>().selectednavItem = this.gameObject.GetComponent<NavigationItem>();
    }
}
