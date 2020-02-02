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

    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    void Start()
    {
        if(player == null)
        {
            player = this;
        } else
        {
            if(player != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        // Player Movement
        playerBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * movingSpeed;
        playerAnimator.SetFloat("moveX", playerBody.velocity.x);
        playerAnimator.SetFloat("moveY", playerBody.velocity.y);

        // Player Movement Animations
        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            playerAnimator.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
            playerAnimator.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
        }

        // Player stays within boundaries
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x),
                                         Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y),
                                         transform.position.z);
    }

    public void SetBounds(Vector3 leftLimit, Vector3 rightLimit)
    {
        bottomLeftLimit = leftLimit + new Vector3(.5F, .5F, 0F);
        topRightLimit = rightLimit + new Vector3(-.5F, -.5F, 0F);
    }
}
