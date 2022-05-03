using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(string name)
    {
        
        AudioClip audioClip = Resources.Load<AudioClip>($@"Sounds/{name}");
        if (audioClip == null || source == null) return;
        source.Stop();
        source.clip = audioClip;
        source.Play();
    }
}
