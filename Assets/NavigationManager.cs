using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationManager : MonoBehaviour
{
    public NavigationLayoutObject navObj;
    public NavigationItem selectedNavItem;
    
    public void Start()
    {
        navObj = GameObject.Find("GridPanel").GetComponent<NavigationLayoutObject>();
        NewSelection(navObj);
    }

    public void NewSelection(NavigationLayoutObject nav)
    {
        navObj = nav;
        GameController.control.Player.GetComponent<PlayerController>().inputType = PlayerController.InputType.UI;
        Cursor.visible = true;
        selectedNavItem = navObj.transform.GetChild(0).GetComponent<NavigationItem>();
        selectedNavItem.Select();
    }

    public void ClearSelection()
    {
        selectedNavItem.DeSelect();
        Cursor.visible = false;
        GameController.control.Player.GetComponent<PlayerController>().inputType = PlayerController.InputType.Game;
        selectedNavItem = null;
        navObj.transform.parent.gameObject.SetActive(false);
    }

    public void Update()
    {
       if( GameController.control.Player.GetComponent<PlayerController>().inputType == PlayerController.InputType.UI)
        {
            if (Input.GetKeyDown(KeyCode.W)) {
                if (selectedNavItem.Up.ob != null)
                {
                    selectedNavItem.DeSelect();
                    selectedNavItem.Up.ob.GetComponent<NavigationItem>().Select();
                    selectedNavItem = selectedNavItem.Up.ob.GetComponent<NavigationItem>();
                }
            }
            if (Input.GetKeyDown(KeyCode.A)) {
                if (selectedNavItem.Left.ob != null)
                {
                    selectedNavItem.DeSelect();
                    selectedNavItem.Left.ob.GetComponent<NavigationItem>().Select();
                    selectedNavItem = selectedNavItem.Left.ob.GetComponent<NavigationItem>();
                }
            }
            if (Input.GetKeyDown(KeyCode.S)) {
                if (selectedNavItem.Down.ob != null)
                {
                    selectedNavItem.DeSelect();
                    selectedNavItem.Down.ob.GetComponent<NavigationItem>().Select();
                    selectedNavItem = selectedNavItem.Down.ob.GetComponent<NavigationItem>();
                }
            }
            if (Input.GetKeyDown(KeyCode.D)) {
                if (selectedNavItem.Right.ob != null)
                {
                    selectedNavItem.DeSelect();
                    selectedNavItem.Right.ob.GetComponent<NavigationItem>().Select();
                    selectedNavItem = selectedNavItem.Right.ob.GetComponent<NavigationItem>();
                }
            }

            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                selectedNavItem.Use();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ClearSelection();
            }
        }
    }
}
