using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class Music : MonoBehaviour, IPauseHandler
{
    private GameData _gameData;

    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private List<AudioClip> _musics;
    private AudioSource _source;
    private float _volume;
    private bool _isPaused;

    private void Start()
    {
        _gameData = GameData.gameData;
        _volume = _gameData.Volume / 100;
        float volume = Mathf.Log10(_volume) * 20;
        if (volume < -80)
        {
            volume = -80f;
        }
        if (volume > 20)
        {
            volume = 20f;
        }
        _mixer.SetFloat("MasterVolume", volume);
        //Debug.Log(Mathf.Log10(_volume) * 20);

        _source = GetComponent<AudioSource>();

        if (_musics.Count > 0)
        {
            int randomMusic = Random.Range(0, _musics.Count);

            if (_musics[randomMusic] != null)
            {
                _source.clip = _musics[randomMusic];
                _source.Play();
                _source.loop = true;
            }
        }

        _isPaused = PauseManager.pauseManager.IsPaused;
        PauseManager.pauseManager.Register(this);
        SetPause(_isPaused);
    }

    public void SetPause(bool isPaused)
    {
        _isPaused = isPaused;

        if (_isPaused == true)
        {
            _source.Pause();
        }
        else
        {
            _source.UnPause();
        }
    }
}
