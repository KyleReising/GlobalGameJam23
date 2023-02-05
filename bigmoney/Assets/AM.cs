using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AM : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource me;
    public List<AudioClip> clips = new List<AudioClip>();
    void Start()
    {
        me = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playClip(int i)
    {
        me.PlayOneShot(clips[i]);
    }
}
