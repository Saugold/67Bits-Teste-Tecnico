using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    [Header("Footsteps SFX")]
    [SerializeField] private AudioSource footstepsAudioSource;
    [SerializeField] private AudioClip[] footstepsAudioClip;

    
    private void FootstepsSFX()
    {
        footstepsAudioSource.PlayOneShot(footstepsAudioClip[Random.Range(0, footstepsAudioClip.Length)]);
    }

    
}
