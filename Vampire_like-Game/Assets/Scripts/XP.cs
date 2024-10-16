using DG.Tweening;
using System;
using UnityEngine;

public class XP : MonoBehaviour
{
    private bool collected;
    public delegate void Collect(GameObject obj);
    public static event Collect Collected;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collected) return;

        collected = true;

        transform.DOMove(collision.gameObject.transform.position, 0.5f).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            // Collect XP
            gameObject.SetActive(false);
        });
    }

    private void OnDisable()
    {
        collected = false;
        Collected?.Invoke(gameObject);
    }

}
