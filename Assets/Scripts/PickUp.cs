using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject WhumpaPanel = other.GetComponent<PlayerController>().WhumpaPanel;
            GameController.control.AddWhumpaFruids(1);
            WhumpaPanel.GetComponent<ShowHidePanel>().Show();
            StartCoroutine(WhumpaPanel.GetComponent<ShowHidePanel>().PlayAnimAfterDelay(false, 3f));
            Destroy(gameObject);
        }
    }
}
