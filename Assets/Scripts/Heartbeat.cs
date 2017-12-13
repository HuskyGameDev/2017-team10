using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heartbeat : MonoBehaviour {
    public AudioClip normalBeat;
    public AudioClip detectedBeat;
    public AudioClip calmingBeat;

    public AudioClip dying;
    public AudioClip flatLine;

    private AudioSource source;

	// Use this for initialization
	void Start () {
        source = gameObject.GetComponents<AudioSource>()[1];
        BeatNormal();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnDetect()
    {
        source.Stop();
        source.clip = detectedBeat;
        source.Play();
    }

    public void OnRelax()
    {
        source.Stop();
        source.PlayOneShot(calmingBeat);
        
        BeatNormal();
    }

    public void BeatNormal()
    {
        source.clip = normalBeat;
        source.loop = true;
        source.Play();
    }

    public void OnDeath()
    {
        source.Stop();
        source.PlayOneShot(dying);
        
        source.PlayOneShot(flatLine);
    }
}
