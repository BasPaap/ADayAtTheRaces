using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Announcer : MonoBehaviour
{
    public AudioClip atTheGatesAudioClip;
    public AudioClip gunshotAndCommentaryAudioClip;
    
    public void PlayAtTheGatesAnnouncement(float delay = 0.0f)
    {
        PlayAnnouncement(atTheGatesAudioClip, delay);
    }


    public void PlayGunshotAndCommentaryAnnouncement(float delay = 0.0f)
    {
        PlayAnnouncement(gunshotAndCommentaryAudioClip, delay);
    }

    private void PlayAnnouncement(AudioClip audioClip, float delay)
    {
        var announcerAudioSource = GetComponent<AudioSource>();
        if (announcerAudioSource != null)
        {
            announcerAudioSource.clip = audioClip;
            announcerAudioSource.PlayDelayed(delay);
        }
    }
}
