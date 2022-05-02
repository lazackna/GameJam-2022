using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private static AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void Play(string name)
    {
        
        AudioClip audioClip = Resources.Load<AudioClip>($@"Sounds/{name}");
        if (audioClip == null) return;
        source.Stop();
        source.clip = audioClip;
        source.Play();
    }
}
