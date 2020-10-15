using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyelidsScript : MonoBehaviour
{
    private float alpha = 0.75f;
    private SkinnedMeshRenderer rend;

    private void Start()
    {
        rend = GetComponent<SkinnedMeshRenderer>();
        StartCoroutine(StartAnim());
    }

    public IEnumerator StartAnim()
    {
        StartCoroutine(EyesClosed());
        yield return new WaitForSeconds(15f);
        StartCoroutine(StartAnim());
    }

    public IEnumerator EyesClosed()
    {
        yield return new WaitForEndOfFrame();
        alpha -= Time.deltaTime * 3f;
        rend.material.SetFloat("Vector1_A765FC8C", alpha);
        if (alpha > 0.0) StartCoroutine(EyesClosed()); else { alpha = 0; StartCoroutine(EyesOpened()); }
    }

    public IEnumerator EyesOpened()
    {
        yield return new WaitForEndOfFrame();
        alpha += Time.deltaTime * 3f;
        rend.material.SetFloat("Vector1_A765FC8C", alpha);
        if (alpha < 0.75) StartCoroutine(EyesOpened()); else { alpha = 0.75f; }
    }
}
