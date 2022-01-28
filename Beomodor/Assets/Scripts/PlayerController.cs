using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerRB;
    public SpriteRenderer playerRenderer;
    public Vector2 playerScale;

    public float moveSpeed;
    public float jumpForce;
    public float currentVelocity;

    public bool jumping;
    public bool jumpRising;
    public bool directionFlipped;
    public bool gravityReversed;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        playerRenderer = GetComponentInChildren<SpriteRenderer>();
        playerScale = transform.localScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentVelocity = playerRB.velocity.y;
        if (Input.GetKey(KeyCode.D))
        {
            directionFlipped = false;
            playerRB.velocity = new Vector3(1 * moveSpeed * Time.deltaTime, currentVelocity, 0);
            //playerRB.AddForce(new Vector3(1 * moveSpeed * Time.deltaTime ,0,0), ForceMode2D.);
        }

        if (Input.GetKey(KeyCode.A))
        {
            directionFlipped = true;
            playerRB.velocity = new Vector3(-1 * moveSpeed * Time.deltaTime, currentVelocity, 0);
            //playerRB.AddForce(new Vector3(-1 * moveSpeed * Time.deltaTime, 0, 0));
        }

        if (jumpRising && !gravityReversed)
        {
            playerRB.velocity = new Vector3(playerRB.velocity.x, jumpForce * Time.deltaTime, 0);
        }
        else if (jumpRising && gravityReversed)
        {
            playerRB.velocity = new Vector3(playerRB.velocity.x, -1 * jumpForce * Time.deltaTime, 0);
        }

        if (directionFlipped && !gravityReversed)
        {
            playerRenderer.flipX = true;
        }
        else if (directionFlipped && gravityReversed)
        {
            playerRenderer.flipX = false;
        }
        else if (!directionFlipped && gravityReversed)
        {
            playerRenderer.flipX = true;
        }
        else if (!directionFlipped && !gravityReversed)
        {
            playerRenderer.flipX = false;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !jumping)
        {
            StartCoroutine(Jump());
            //playerRB.velocity = new Vector3(playerRB.velocity.x, jumpForce * Time.deltaTime, 0);
            //playerRB.AddForce(new Vector3(-1 * moveSpeed * Time.deltaTime, 0, 0));
        }

        //GRAVSHIFT
        if (Input.GetKeyDown(KeyCode.E))//Temporary
        {
            playerRB.gravityScale = -1;
            transform.eulerAngles = Vector3.forward * 180;
            gravityReversed = true;
        }
        if (Input.GetKeyDown(KeyCode.Q)) //Temporary
        {
            playerRB.gravityScale = 1;
            transform.eulerAngles = Vector3.forward * 0;
            transform.localScale = playerScale;
            gravityReversed = false;
        }

    }
    public IEnumerator Jump()
    {
        jumping = true;
        jumpRising = true;
        yield return new WaitForSeconds(0.1f);
        //playerRB.velocity = new Vector3(playerRB.velocity.x, jumpForce * Time.deltaTime, 0);
        yield return new WaitForSeconds(0.2f);
        jumpRising = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!jumpRising)
        {
            jumping = false;
        }
    }
}
