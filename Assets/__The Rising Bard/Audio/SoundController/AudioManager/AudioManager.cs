using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;

	//public AudioMixerGroup mixerGroup;

	public Sound[] sounds;



    private void OnEnable()
    {
		ItemPickSoundHandler.PlaySoundEvent += Play;


	}
    private void OnDisable()
    {
		ItemPickSoundHandler.PlaySoundEvent -= Play;
	}
    void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
			s.source.outputAudioMixerGroup = s.mixerGroup;
			
		}
	}

	public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		 
		if (s == null)
		{
			Debug.Log("Sound: " + sound + " not found! Please check "+name+ "Object in Scene");
			return;
		}

		//s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		//s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
		//
		if (s.source.isPlaying == false)
        {
            if (sound == "walk")
            {
				s.source.time = 23.5f;
            }
            s.source.Play();

        }
        if (sound == "jump")
        {
            s.source.PlayOneShot(s.source.clip);
        }

    }




    public void Stop(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);

        if (s == null)
        {
            Debug.Log("Sound: " + sound + " not found! Please check " + name + "Object in Scene");
            return;
        }
 
            s.source.Stop();
        

    }

}