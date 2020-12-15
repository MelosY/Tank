using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barriar : MonoBehaviour {

    public AudioClip hitAudio;

    //播放音乐
    public void PlayAudio()
    {
        AudioSource.PlayClipAtPoint(hitAudio, transform.position);
    }
}
