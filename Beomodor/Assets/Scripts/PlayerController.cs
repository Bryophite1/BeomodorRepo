using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerRB;
    public SpriteRenderer playerRenderer;
    public Vector2 playerScale;
    public GameObject singRadius;
    public ParticleSystem songParticle;
    public ParticleSystem gravParticle;
    public Animator playerAnim;

    public float moveSpeed;
    public float jumpForce;
    public float currentVelocity;

    public bool jumping;
    public bool jumpRising;
    public bool directionFlipped;
    public bool gravityReversed;
    public bool singing;
    public bool startSinging;
    public bool landing;

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

        //ANIMATIONS
        if (Input.GetKey(KeyCode.A) && !jumping || Input.GetKey(KeyCode.D) && !jumping)
        {
            playerAnim.SetBool("walk", true);
        }
        else
        {
            playerAnim.SetBool("walk", false);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !jumping && !singing)
        {
            StartCoroutine(Jump());
            //playerRB.velocity = new Vector3(playerRB.velocity.x, jumpForce * Time.deltaTime, 0);
            //playerRB.AddForce(new Vector3(-1 * moveSpeed * Time.deltaTime, 0, 0));
        }

        //GRAVSHIFT
        if (Input.GetKeyDown(KeyCode.E))//Temporary
        {
            playerRB.gravityScale = -1;
            gravParticle.Play();
            transform.eulerAngles = Vector3.forward * 180;
            gravityReversed = true;
        }
        if (Input.GetKeyDown(KeyCode.Q)) //Temporary
        {
            playerRB.gravityScale = 1;
            gravParticle.Play();
            transform.eulerAngles = Vector3.forward * 0;
            transform.localScale = playerScale;
            gravityReversed = false;
        }

        //SINGING
        if (Input.GetKey(KeyCode.V) && !jumping)
        {
            singing = true;
        }
        else
        {
            singing = false;
        }

        if (singing)
        {
            singRadius.SetActive(true);

            if (!startSinging && Input.GetKeyDown(KeyCode.V) || !startSinging && landing)
            {
                StartCoroutine(SingBegin());
            }
        }
        else
        {
            singRadius.SetActive(false);
        }

    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!jumpRising && other.CompareTag("Ground"))
        {
            jumping = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!jumpRising && other.CompareTag("Ground"))
        {
            StartCoroutine(Land());
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
    public IEnumerator SingBegin()
    {
        startSinging = true;
        yield return new WaitForSeconds(0.1f);
        startSinging = false;
        StartCoroutine(SingParticles());
        StopCoroutine(SingBegin());
    }
    public IEnumerator SingParticles()
    {
        yield return new WaitForSeconds(0.1f);
        songParticle.Emit(1);
        if (singing)
        {
            StartCoroutine(SingParticles());
        }
    }
    public IEnumerator Land()
    {
        landing = true;
        yield return new WaitForSeconds(0.1f);
        landing = false;
    }
}
