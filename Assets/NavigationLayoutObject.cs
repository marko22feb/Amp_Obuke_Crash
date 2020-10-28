using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationLayoutObject : MonoBehaviour
{
    public int ColumnNumb;
    public int RowNumb;

    private void Start()
    {
        StartCoroutine(DelayEndFrame());
    }

    IEnumerator DelayEndFrame()
    {
        yield return new WaitForEndOfFrame();
        GetColumnAndRowNumb(GetComponent<GridLayoutGroup>(), out ColumnNumb, out RowNumb);
    }

    void GetColumnAndRowNumb(GridLayoutGroup glg, out int column, out int row)
    {
        column = 0;
        row = 0;

        if (glg.transform.childCount == 0) return;

        column = 1;
        row = 1;

        RectTransform firstChildGob = glg.transform.GetChild(0).GetComponent<RectTransform>();
        Vector2 firstChildPos = firstChildGob.anchoredPosition;

        bool RowFound = false;

        for (int i = 1; i < glg.transform.childCount; i++)
        {
            RectTransform currentChildGob = glg.transform.GetChild(i).GetComponent<RectTransform>();

            Vector2 currentChildPos = currentChildGob.anchoredPosition;

            if (firstChildPos.x == currentChildPos.x)
            {
                row++;
                RowFound = true;
            }
            else
            {
               if (!RowFound)
                column++;
            }
        }

        AssignColumnAndRow(glg);
    }

    void AssignColumnAndRow(GridLayoutGroup glg)
    {
        int index = 0;
        for (int x = 0; x < RowNumb; x++)
        {
            for (int y = 0; y < ColumnNumb; y++)
            {
                if (index >= glg.transform.childCount) break;
                glg.transform.GetChild(index).GetComponent<NavigationItem>().gridGraph = new Vector2(x, y);
                index++;
            }
        }

        GetDirectionNav(glg);
    }

    void GetDirectionNav (GridLayoutGroup glg)
    {
        Transform NavGrid = glg.transform;
        foreach(Transform child in NavGrid)
        {
            NavigationItem CurrentNavItem = child.GetComponent<NavigationItem>();

            //Up
            foreach (Transform kid in NavGrid)
            {
                NavigationItem NavItemToCheck = kid.GetComponent<NavigationItem>();
                if (NavItemToCheck.gridGraph.x + 1 == CurrentNavItem.gridGraph.x && NavItemToCheck.gridGraph.y == CurrentNavItem.gridGraph.y)
                {
                    CurrentNavItem.Up = new NavigationItem.NextNav(NavigationItem.Direction.up, NavItemToCheck.gameObject);
                }
            }

            //Down
            foreach (Transform kid in NavGrid)
            {
                NavigationItem NavItemToCheck = kid.GetComponent<NavigationItem>();
                if (NavItemToCheck.gridGraph.x - 1 == CurrentNavItem.gridGraph.x && NavItemToCheck.gridGraph.y == CurrentNavItem.gridGraph.y)
                {
                    CurrentNavItem.Down = new NavigationItem.NextNav(NavigationItem.Direction.down, NavItemToCheck.gameObject);
                }
            }

            //Left
            foreach (Transform kid in NavGrid)
            {
                NavigationItem NavItemToCheck = kid.GetComponent<NavigationItem>();
                if (NavItemToCheck.gridGraph.x == CurrentNavItem.gridGraph.x && NavItemToCheck.gridGraph.y + 1 == CurrentNavItem.gridGraph.y)
                {
                    CurrentNavItem.Left = new NavigationItem.NextNav(NavigationItem.Direction.left, NavItemToCheck.gameObject);
                }
            }

            //Right
            foreach (Transform kid in NavGrid)
            {
                NavigationItem NavItemToCheck = kid.GetComponent<NavigationItem>();
                if (NavItemToCheck.gridGraph.x == CurrentNavItem.gridGraph.x && NavItemToCheck.gridGraph.y - 1 == CurrentNavItem.gridGraph.y)
                {
                    CurrentNavItem.Right = new NavigationItem.NextNav(NavigationItem.Direction.right, NavItemToCheck.gameObject);
                }
            }
        }
    }
}
