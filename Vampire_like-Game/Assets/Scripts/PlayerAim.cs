

using DG.Tweening;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] private FieldOfView FOV;
    void Update()
    {
        var dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        FOV.SetAimDirection(dir);

        FOV.SetOrigin(transform.position);
    }
}

