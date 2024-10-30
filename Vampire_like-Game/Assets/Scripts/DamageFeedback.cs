using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageFeedback : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] TMP_Text value;
    ObjectPool pool;


    public void UpdateValue(string text)
    {
        value.text = text;
        this.pool = UIManager.Instance.ValueFeedback;
        animator.Play("Value");
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void OnDisable()
    {
        if(pool != null)
        {
            pool.ReturnObject(gameObject);
        }
        pool = null;
    }
}
