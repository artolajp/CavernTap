using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

    [SerializeField]
    private AudioClip tap = null, tap2 = null,damage = null, gameOver = null, gameOverRecord = null,star=null,fail=null,exitCave=null,life=null,pausa=null,power=null;
    [SerializeField]
    private AudioClip loop = null,loopMulti=null;
    private static SoundController instance;
    private AudioSource [] audioSource;
    private bool isLooping;
    private bool isMultiplicador;
    public static SoundController Instance
    {
        get
        {
            if (!instance) instance = FindObjectOfType<SoundController>();
            return instance;
        }
    }

    public bool IsLooping
    {
        get
        {
            return isLooping;
        }

        set
        {
            isLooping = value;
        }
    }

    public enum Sounds { TAP,TAP2,GAMEOVER,GAMEOVERRECORD,DAMAGE,STAR,FAIL,EXITCAVE,LIFE,PAUSA,POWER}

    public void PlaySound(Sounds s) {
        
        if (audioSource==null|| audioSource.Length==0)
        {

            audioSource = GetComponents<AudioSource>();
        }
        if (SettingsController.Music)
        {
            audioSource[0].pitch = 0.97f + (0.03f * ScoreController.Instance.Multiplicador);

            if (ScoreController.Instance.Multiplicador > 1)
            {
                if (!isMultiplicador)
                {
                    audioSource[1].clip = loopMulti;
                    audioSource[1].Play();
                    isMultiplicador = true;
                }
                if (ScoreController.Instance.Multiplicador == 3)
                {
                    audioSource[1].pitch = 1.02f;
                }
                else if (ScoreController.Instance.Multiplicador == 4)
                {
                    audioSource[1].pitch = 1.03f;
                }
            }
            else if (isMultiplicador)
            {
                audioSource[1].clip = loop;
                audioSource[1].Play();
                audioSource[1].pitch = 1.0f;
                isMultiplicador = false;

            }
        }
        if (SettingsController.Sfx)
        {
            switch (s) {
                case Sounds.TAP: audioSource[0].PlayOneShot( tap);
                    break;
                case Sounds.TAP2:
                    audioSource[0].PlayOneShot(tap2);
                    break;
                case Sounds.GAMEOVER:
                    audioSource[0].PlayOneShot(gameOver);
                    audioSource[1].Stop();
                    break;
                case Sounds.DAMAGE:
                    audioSource[0].PlayOneShot(damage);
                    break;
                case Sounds.GAMEOVERRECORD:
                    audioSource[0].PlayOneShot(gameOverRecord);
                    audioSource[1].Stop();
                    break;
                case Sounds.STAR:
                    audioSource[0].PlayOneShot(star);
                    break;
                case Sounds.FAIL:
                    audioSource[0].PlayOneShot(fail);
                    break;
                case Sounds.EXITCAVE:
                    if (ScoreController.Instance.Multiplicador == 1)
                        audioSource[0].PlayOneShot(exitCave);
                    break;
                case Sounds.LIFE:
                    audioSource[0].PlayOneShot(life);
                    break;
                case Sounds.PAUSA:
                    audioSource[0].PlayOneShot(pausa);
                    break;
                case Sounds.POWER:
                    audioSource[0].PlayOneShot(power);
                    break;
            }
        }
    }

    void Start() {
        PlayMusic();
    }

    public void PlayMusic() {
        if (audioSource == null || audioSource.Length == 0)
        {

            audioSource = GetComponents<AudioSource>();
        }
        if (SettingsController.Music)
        {
            
            audioSource[1].clip = ScoreController.Instance.Multiplicador>1? loopMulti:loop;
            audioSource[1].Play();
            audioSource[1].loop = true;
        }
        else {
            audioSource[1].Stop();
        }
    }

}
