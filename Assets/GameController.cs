using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int WhumpaFruitCount = 0;
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
    }
}
