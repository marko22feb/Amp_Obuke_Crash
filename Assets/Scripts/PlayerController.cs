﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject WhumpaPanel;
    private Animation anim;
    private Animator animC;
    private AudioSource audio;
    public ParticleSystem dust;
    public List<AudioClip> CrashSpins;
    public List<AudioClip> WaterSounds;
    public List<AudioClip> GroanSounds;
    public AudioClip LandSounds;
    public PhysicMaterial Land;
    public PhysicMaterial Water;

    public void Awake()
    {
        anim = GetComponent<Animation>();
        animC = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (!anim.isPlaying)
            {
                anim.Play("Tornado");
                int a = Random.Range(0, CrashSpins.Count);
                audio.clip = CrashSpins[a];
                audio.Play(0);
            }
        }
    }

    public void PlayFootStepsSounds(float volume)
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position + (Vector3.up * 0.5f), Vector3.down, out hitInfo, 1f))
        {
            if (hitInfo.collider.material.name == "Water (Instance)")
            {
                AudioSource.PlayClipAtPoint(WaterSounds[Random.Range(0, WaterSounds.Count)], transform.position, volume);
            }
            else
            {
                AudioSource.PlayClipAtPoint(LandSounds, transform.position, volume);
            }
        }
        dust.Play();
    }

    public void PlayAnim(string animName, List<AudioClip> sounds)
    {
        anim.Play(animName);
        int a = Random.Range(0, sounds.Count);
        audio.clip = sounds[a];
        audio.Play(0);
    }

    public void GameOver()
    {
        GameController.control.GameOver();
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
