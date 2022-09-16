using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioEffect : MonoBehaviour
{
    [SerializeField] private AudioClip _effect;
    private AudioSource _source;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
        _source.clip = _effect;
    }

    public void Effect()
    {
        float randPitch = Random.Range(0.9f, 1.1f);
        _source.pitch = randPitch;
        _source.Play();
    }
}
