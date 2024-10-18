using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private LayerMask m_LayerMask;
    private Mesh mesh;
    private Vector3 origin;
    private float startingAngle;
    [SerializeField] private float fov;

    [SerializeField] private float viewDistance;

    [SerializeField] private Transform aim;

    private bool alreadyPlayed;
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        origin = Vector3.zero;
    }
    private void Update()
    {
        if(Time.timeScale < 1) return;

        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (alreadyPlayed)
            {
                return;
            }
            else
            {

                alreadyPlayed = true;
                audioSource.Play();
            }
        }
        else
        {
            alreadyPlayed = false;
            audioSource.Stop();
        }


        Vector3 origin = Vector3.zero;

        int rayCount = 15;
        float angle = startingAngle;
        float angleIncrease = fov / rayCount;



        aim.rotation = Quaternion.Euler(0, 0, angle - fov/2);


        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for(int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, m_LayerMask);
            if(raycastHit2D.collider == null)
            {
                // No Hit
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                vertex = raycastHit2D.point;
            }

            vertices[vertexIndex] = vertex;

            if(i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            angle -= angleIncrease;
            vertexIndex++;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }


    public void SetAimDirection(Vector3 aimDirection)
    {
        var angle = GetAngleFromVectorFloat(aimDirection);
        startingAngle = angle + fov / 2f;
    }

    private float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    } 

    private Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
}
