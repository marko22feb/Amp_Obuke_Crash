using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public enum PickUpType { WhumpaFruit, Gem};
    public PickUpType Type;

    public List<AudioClip> WhumpaPickUpSounds;
    public AudioClip GemPickUpSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject WhumpaPanel = other.GetComponent<PlayerController>().WhumpaPanel;
            GameController.control.AddWhumpaFruids(1);
            WhumpaPanel.GetComponent<ShowHidePanel>().Show();
            StartCoroutine(WhumpaPanel.GetComponent<ShowHidePanel>().PlayAnimAfterDelay(false, 3f));
            AudioClip soundToPlay = null;
            switch (Type)
            {
                case PickUpType.WhumpaFruit:
                    soundToPlay = WhumpaPickUpSounds[Random.Range(0, WhumpaPickUpSounds.Count)];
                    break;
                case PickUpType.Gem:
                    soundToPlay = GemPickUpSound;
                    break;
                default:
                    break;
            }
            AudioSource.PlayClipAtPoint(soundToPlay, transform.position);
            Destroy(gameObject);
        }
    }
}
