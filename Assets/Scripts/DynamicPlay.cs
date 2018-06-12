using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicPlay : MonoBehaviour {

    public AudioClip auido;
    //public AudioSource AudioSource;

    private void Awake()
    {
        ButtonManager.Buttons.Add(this);
    }

    public void PlayAuido()
    {
        ButtonManager.audioSource.Stop();
        ButtonManager.audioSource.PlayOneShot(auido);
    }
}
