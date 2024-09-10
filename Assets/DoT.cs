using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DdoT : MonoBehaviour
{
    public static DdoT Instans;
    private void Awake()
    {
        var obj=FindAnyObjectByType<DdoT>();
        if (obj != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instans=this;
        }
    }
    public void PlayAudio(AudioClip audio)
    {
        GetComponent<AudioSource>().PlayOneShot(audio);
    }
}
