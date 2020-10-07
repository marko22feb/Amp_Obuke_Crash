using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScriptShaderGraph : MonoBehaviour
{
    MeshRenderer rend;
    float alpha = 1;

    public void Awake()
    {
        rend = GetComponent<MeshRenderer>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if( other.tag == "Player")
        {
            StartCoroutine(decreaseAlpha());
        }
    }

    IEnumerator decreaseAlpha()
    {
        yield return new WaitForEndOfFrame();
        alpha -= Time.deltaTime * 0.3f;
        rend.material.SetFloat("Vector1_E1AB361A", alpha);
        if (alpha > 0) StartCoroutine(decreaseAlpha()); else Destroy(gameObject);
    }
}
