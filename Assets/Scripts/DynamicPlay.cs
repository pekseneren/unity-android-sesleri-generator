using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicPlay : MonoBehaviour {

    public AudioClip auido;
    //public AudioSource AudioSource;

    public void PlayAuido()
    {
        ButtonManagement.auidoSource.Stop();
        ButtonManagement.auidoSource.PlayOneShot(auido);
    }
}
