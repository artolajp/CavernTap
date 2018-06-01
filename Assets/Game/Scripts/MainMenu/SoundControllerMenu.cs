using UnityEngine;
using System.Collections;

public class SoundControllerMenu : MonoBehaviour
{

    private static SoundControllerMenu instance;

    [SerializeField]
    private AudioClip click=null,buy = null, cancel = null;

    private AudioSource[] audioSources;
    void Start()
    {
        instance = FindObjectOfType<SoundControllerMenu>();
        audioSources = GetComponents<AudioSource>();
    }

    public void PlayClick()
    {

        instance.audioSources[0].PlayOneShot(click);

    }
    public void PlayBuy()
    {
        instance.audioSources[0].PlayOneShot(buy);

    }
    public void PlayCancel()
    {
        instance.audioSources[0].PlayOneShot(cancel);

    }



}
