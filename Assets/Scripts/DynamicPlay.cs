using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicPlay : MonoBehaviour {

    public AudioClip auido;
    //public AudioSource AudioSource;

    public void PlayAuido()
    {
        ButtonManager.auidoSource.Stop();
        ButtonManager.auidoSource.PlayOneShot(auido);
    }
}
