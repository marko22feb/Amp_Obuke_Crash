using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritterController : PawnController
{
    public override void OnMessageReceived(PlayerController.MessageType type)
    {
        Debug.Log("critter");
    }
}
