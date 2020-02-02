using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController player;

    [Header("Player")]
    public Rigidbody2D playerBody;
    public Animator playerAnimator;
    public float movingSpeed;

    [Header("Area")]
    public string areaTransitionName;

    void Start()
    {
        if(player == null)
        {
            player = this;
        } else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        playerBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * movingSpeed;
        playerAnimator.SetFloat("moveX", playerBody.velocity.x);
        playerAnimator.SetFloat("moveY", playerBody.velocity.y);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            playerAnimator.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
            playerAnimator.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
        }
    }
}
