using UnityEngine;

public class XPPool : ObjectPool
{
    private void OnEnable()
    {
        XP.Collected += ReturnObject;
    }

    private void OnDisable()
    {
        XP.Collected -= ReturnObject;
    }
}
