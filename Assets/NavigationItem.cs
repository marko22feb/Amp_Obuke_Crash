using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NavigationItem : MonoBehaviour
{
    [HideInInspector]
    public Vector2 gridGraph;

    [System.Serializable]
    public enum Direction { left, right, up, down}
    [System.Serializable]
    public struct NextNav { public Direction direction; public GameObject ob; public NextNav(Direction dir, GameObject gob) { direction = dir; ob = gob; } }

    [HideInInspector]
    public NextNav Up = new NextNav(Direction.up, null);
    [HideInInspector]
    public NextNav Down = new NextNav(Direction.down, null);
    [HideInInspector]
    public NextNav Left = new NextNav(Direction.left, null);
    [HideInInspector]
    public NextNav Right = new NextNav(Direction.right, null);

    public UnityEvent FunctionToCall;

    public void Select()
    {
        GetComponent<Image>().color = Color.blue;
    }

    public void DeSelect()
    {
        GetComponent<Image>().color = Color.white;
    }

    public void Use()
    {
        FunctionToCall.Invoke();
    }
}
