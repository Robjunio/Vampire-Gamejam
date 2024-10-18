using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playable : MonoBehaviour
{
    private ObjectPool ObjectPool;
    private AudioSource audioSource;

    private void Awake()
    {
        TryGetComponent(out audioSource);
    }

    public void SetObject(ObjectPool pool, AudioClip clip)
    {
        ObjectPool = pool;

        audioSource.clip = clip;
        audioSource.volume = 0.1f;
        audioSource.maxDistance = 100f;
        audioSource.spatialBlend = 1f;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        audioSource.dopplerLevel = 0f;
        audioSource.Play();

        Invoke("Disable", audioSource.clip.length + 1f);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        if(ObjectPool != null)
        {
            ObjectPool.ReturnObject(gameObject);
        }

        ObjectPool = null;
    }
}
