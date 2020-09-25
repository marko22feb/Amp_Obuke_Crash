using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhumpaFruit : MonoBehaviour
{
    public Transform player;
    private Vector3 startPos;
    float alpha;
    public float speed = 2f;

    public void FollowPlayer()
    {
        StartCoroutine(StartFollowingPlayer());
    }

    public IEnumerator StartFollowingPlayer()
    {
        yield return new WaitForSeconds(1f);
        startPos = this.transform.position;
        StartCoroutine(KeepFollowingPlayer());
    }

    public IEnumerator KeepFollowingPlayer()
    {
        alpha += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(startPos, player.transform.position, alpha);
        yield return new WaitForEndOfFrame();
        StartCoroutine(KeepFollowingPlayer());
    }
}
