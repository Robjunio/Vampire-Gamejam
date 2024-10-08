

using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] private FieldOfView FOV;
    void Update()
    {
        Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        FOV.SetAimDirection(dir);
    }
}
