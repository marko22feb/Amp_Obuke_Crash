using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnController : MonoBehaviour
{
    public virtual void OnMessageReceived(PlayerController.MessageType type)
    {
    }

    public virtual void OnMessageSend(PlayerController.MessageType type, float radius, Vector3 location)
    {
       Collider[] overlappedObjects = Physics.OverlapSphere(location, radius);
        for (int i = 0; i < overlappedObjects.Length; i++)
        {
            PawnController temp = overlappedObjects[i].GetComponent<PawnController>();
            if(temp != null)
            {
                temp.OnMessageReceived(type);
            }
        }
    }
}
