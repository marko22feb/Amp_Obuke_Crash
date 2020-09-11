using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject WhumpaPanel;
    private Animation anim;
    private Animator animC;

    public void Awake()
    {
        anim = GetComponent<Animation>();
        animC = GetComponent<Animator>();
    }

    public void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            anim.Play("Tornado");
        }
    }

    public void EnableController()
    {
        animC.enabled = true;
    }


    public void DisableController()
    {
        animC.enabled = false;
    }
}
