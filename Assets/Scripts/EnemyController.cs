using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : PawnController
{
    public override void OnMessageReceived(PlayerController.MessageType type)
    {
        Debug.Log("enemy");
    }
}
