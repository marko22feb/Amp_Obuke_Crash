using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject WhumpaPanel;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameController.control.WhumpaFruitCount++;
            WhumpaPanel.GetComponent<ShowHidePanel>().Show();
            StartCoroutine(WhumpaPanel.GetComponent<ShowHidePanel>().PlayAnimAfterDelay(false, 3f));
            Destroy(gameObject);
        }
    }
}
