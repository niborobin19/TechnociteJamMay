using System.Security.Claims;
using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Public Members

    public static SoundManager Instance;

    #endregion


    #region Serialized Members
    [SerializeField]
    private AudioSource _monoAudioSource;

    [SerializeField]
    private AudioSource _bipAudioSource;

    [SerializeField]
    private AudioSource[] _spatializedAudioSources;

    [SerializeField]
    private AudioSource _ambientAudioSource;

    #endregion



    #region Public Methods
    public void PlayAudioClip(AudioClip clip, float volume)
    {
        _monoAudioSource.PlayOneShot(clip, volume);
    }

    public void PlayAudioClipSpatialized(AudioClip clip, float volume, int source)
    {
        _spatializedAudioSources[source].PlayOneShot(clip, volume);
    }

    public void PlayBip(AudioClip clip, float volume)
    {
        //_bipAudioSource.pitch = 1f;
        _bipAudioSource.PlayOneShot(clip, volume);
    }

    public void StopBip()
    {
        _bipAudioSource.Stop();
    }

    #endregion



    #region Unity API
    private void Awake() 
    {
        InitializeInstance();
    }

    #endregion


    #region Utils
    private void InitializeInstance()
    {
        Instance = this;
    }


    #endregion


    #region Private And Protected

    #endregion
}