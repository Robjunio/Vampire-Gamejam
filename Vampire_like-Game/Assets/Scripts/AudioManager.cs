using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum Sound
    {
        PlayerHitEffect,
        EnemyHitEffect,
        DyingEnemy,
        Bats,
        HeroDying,
        Walk,
        Potion,
        Shield
    }

    [System.Serializable]
    public class SoundRef
    {
        public Sound name;
        public AudioClip clip;
    }

    [SerializeField] private SoundRef[] sounds;
    [SerializeField] private ObjectPool soundPool;
    public static AudioManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void PlaySound(Sound sound, Vector3 pos)
    {
        GameObject soundObj = soundPool.GetFreeObject();
        soundObj.SetActive(true);
        soundObj.GetComponent<Playable>().SetObject(soundPool, GetClip(sound));
        AudioSource audioSource = soundObj.GetComponent<AudioSource>();
        soundObj.transform.localPosition = pos;
    }

    public AudioClip GetClip(Sound sound)
    {
        foreach (SoundRef audioClip in sounds)
        {
            if (audioClip.name == sound)
            {
                return audioClip.clip;
            }
        }
        Debug.Log("Sound " + sound + " wasn't found!");
        return null;
    }
}
