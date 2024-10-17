using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Animator Animator;
    private Rigidbody2D rb;
    public float moveSpeed = 2.0f;
    private float moveHorizontal, moveVertical;
    private Vector2 movement;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        movement = new Vector2 (moveHorizontal, moveVertical);

        if(movement.magnitude > 0) {
            Animator.SetFloat("X", moveHorizontal);
            Animator.SetFloat("Y", moveVertical);
            UIManager.Instance.ActivateText(false);
        }
        else
        {
            UIManager.Instance.ActivateText(true);
        }
        
        Animator.SetFloat("Magnitude", movement.magnitude);
    }

    private void FixedUpdate()
    {
        rb.velocity = movement.normalized * moveSpeed * Time.fixedDeltaTime;
    }
}
