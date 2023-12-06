using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpParticle : MonoBehaviour
{
    public AudioClip levelUpAudio;
    public ParticleSystem particle;
    public static LevelUpParticle Instance;

    private AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = levelUpAudio;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelUp()
    {
        if (particle != null)
        {
            particle.Play();
            if (audioSource != null)
            {
                Debug.Log("AudioPlay");
                audioSource.Play();
            }
            
        }
    }
}
