using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

    public AudioSource musicSource;
    public AudioClip GameState;
    public AudioClip AlertState;
    public AudioClip ResearchState;
    public AudioClip DeathState;
    public AudioClip WinState;
    public AudioClip MenuState;
    public AudioClip DumbState;
    public AudioClip EpicState;
    public AudioClip SpookyState;

    private float timeSpecialMusicStart;
    private float specialMusicDelay = 30;

    void Awake()
    {
        musicSource.clip = MenuState;
        musicSource.Play();
        musicSource.loop = true;
    }

	// Use this for initialization
	void Start () {
        timeSpecialMusicStart = Time.time;
	}

    public void launchMenuMusic()
    {
        musicSource.clip = MenuState;
        musicSource.Play();
        musicSource.loop = true;
    }

    public void launchGameLoopMusic()
    {
        musicSource.clip = GameState;
        musicSource.Play();
        musicSource.loop = true;
    }

    public void launchResearchMusic()
    {
        musicSource.clip = ResearchState;
        musicSource.Play();
        musicSource.loop = true;
    }

    public void launchAlertMusic()
    {
        musicSource.clip = AlertState;
        musicSource.Play();
        musicSource.loop = true;
    }

    public void launchDeathMusic()
    {
        musicSource.clip = DeathState;
        musicSource.Play();
        musicSource.loop = false;
    }

    public void launchWinMusic()
    {
        musicSource.clip = WinState;
        musicSource.Play();
        musicSource.loop = false;
    }

    public void launchEpicMusic()
    {
        musicSource.clip = EpicState;
        musicSource.Play();
        musicSource.loop = true;
    }

    public void launchSpookyMusic()
    {
        musicSource.clip = SpookyState;
        musicSource.Play();
        musicSource.loop = true;
    }

    public void launchDumbMusic()
    {
        musicSource.clip = DumbState;
        musicSource.Play();
        musicSource.loop = true;
    }
	
	// Update is called once per frame
	void Update () {
        
        if(Input.GetKeyDown(KeyCode.Keypad0))
        {
            launchGameLoopMusic();
        }

	    if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            launchResearchMusic();
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            launchAlertMusic();
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            launchMenuMusic();
        }

        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            launchDeathMusic();
        }

        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            launchWinMusic();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            launchDumbMusic();
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            launchEpicMusic();
        }

        if (Input.GetKeyDown(KeyCode.CapsLock))
        {
            launchSpookyMusic();   
        }
	}
}
