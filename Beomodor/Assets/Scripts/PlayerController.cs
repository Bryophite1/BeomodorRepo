using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerRB;
    public SpriteRenderer playerRenderer;
    public Vector2 playerScale;
    public GameObject singRadius;
    public ParticleSystem songParticle;
    public ParticleSystem gravParticle;
    public Animator playerAnim;
    public GameObject blink;
    public GameObject eyes;
    public GameObject mouth;
    public PhysicsMaterial2D[] physics;
    public Image canvasFlat;
    public AudioSource playerAudio;
    public AudioClip[] playerSound;
    public GameObject shroomAudio;

    public float moveSpeed;
    public float jumpForce;
    public float currentVelocity;
    public float canvasFlatAlphaValue;
    public float singVolume;
    public Vector3 scaleChange;

    public bool jumping;
    public bool jumpRising;
    public bool directionFlipped;
    public bool gravityReversed;
    public bool singing;
    public bool startSinging;
    public bool landing;
    public bool blinkOverride;
    public bool gravTouch;
    public bool gravWait;
    public bool sceneChange;
    private bool startJump;

    public int gravState;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        playerRenderer = GetComponentInChildren<SpriteRenderer>();
        scaleChange = transform.localScale;
        StartCoroutine(Blinking());

        canvasFlatAlphaValue = 1;
        canvasFlat = GameObject.Find("CanvasFlat").GetComponent<Image>();
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
            transform.localScale = new Vector3(-scaleChange.x, scaleChange.y, scaleChange.z);
        }
        else if (directionFlipped && gravityReversed)
        {
            transform.localScale = new Vector3(scaleChange.x, scaleChange.y, scaleChange.z);
        }
        else if (!directionFlipped && gravityReversed)
        {
            transform.localScale = new Vector3(-scaleChange.x, scaleChange.y, scaleChange.z);
        }
        else if (!directionFlipped && !gravityReversed)
        {
            transform.localScale = new Vector3(scaleChange.x, scaleChange.y, scaleChange.z);
        }

        //ANIMATIONS
        if (Input.GetKey(KeyCode.A) && !jumping || Input.GetKey(KeyCode.D) && !jumping)
        {
            playerRB.sharedMaterial = physics[1];
            playerAnim.SetBool("walk", true);
        }
        else
        {
            playerRB.sharedMaterial = physics[0];
            playerAnim.SetBool("walk", false);
        }

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            playerRB.sharedMaterial = physics[1];
        }
        else
        {
            playerRB.sharedMaterial = physics[0];
        }

        if (!sceneChange && canvasFlat.color.a > 0)
        {
            canvasFlatAlphaValue -= 0.01f;
        }

        if (sceneChange && canvasFlatAlphaValue < 1)
        {
            canvasFlatAlphaValue += 0.05f;
        }

        if (sceneChange && canvasFlatAlphaValue >= 1)
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
        }

        canvasFlat.color = new Color(0, 0, 0, canvasFlatAlphaValue);

        //Eyes
        if (blink.active == true)
        {
            eyes.SetActive(false);
        }
        else
        {
            eyes.SetActive(true);
        }

        //Singing Volume Adjuster
        if (!playerAudio.isPlaying && singing)
        {
            playerAudio.PlayOneShot(playerSound[0]);
        }
        playerAudio.volume = singVolume;

        if (singing && singVolume < 0.5)
        {
            singVolume += 0.01f;

        }
        else if (!singing && singVolume > 0)
        {
            singVolume -= 0.1f;
        }
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !jumping && !singing)
        {
            StartCoroutine(Jump());
            //playerRB.velocity = new Vector3(playerRB.velocity.x, jumpForce * Time.deltaTime, 0);
            //playerRB.AddForce(new Vector3(-1 * moveSpeed * Time.deltaTime, 0, 0));
        }

        //GRAVSHIFT
        if (gravState == 0 && gravTouch)//Temporary
        {
            Debug.Log("Up");
            playerRB.gravityScale = -1;
            gravParticle.Play();
            transform.eulerAngles = Vector3.forward * 180;
            gravityReversed = true;
        }
        if (gravState == 1 && gravTouch) //Temporary
        {
            Debug.Log("Down");
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

        //Singing Animations
        if (singing)
        {
            blink.SetActive(true);
            mouth.SetActive(true);
        }
        else
        {
            if (!blinkOverride)
            {
                blink.SetActive(false);
            }
            mouth.SetActive(false);
        }

        //JumpLand
        if(!jumping && landing)
        {
            playerAnim.SetTrigger("land");
        }

    }
    private void OnTriggerStay2D(Collider2D other)
    {
        //if (!jumpRising && other.CompareTag("Ground"))
        {
            //jumping = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!startJump && other.CompareTag("Ground"))
        {
            StopCoroutine(Jump());
            jumpRising = false;
            startJump = false;
            StartCoroutine(Land());
        }
        if (other.CompareTag("Mushrooms"))
        {
            if (!gravTouch && !gravWait)
            {
                StartCoroutine(GravShift());
            }
        }
        if (other.CompareTag("LevelEnd"))
        {
            sceneChange = true;
        }
    }
    public IEnumerator Jump()
    {
        jumping = true;
        playerAnim.SetTrigger("jump");
        playerAnim.ResetTrigger("land");
        startJump = true;
        yield return new WaitForSeconds(0.2f);
        jumpRising = true;
        yield return new WaitForSeconds(0.1f);
        //playerRB.velocity = new Vector3(playerRB.velocity.x, jumpForce * Time.deltaTime, 0);
        yield return new WaitForSeconds(0.2f);
        jumpRising = false;
        startJump = false;
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
        jumping = false;
        landing = true;
        yield return new WaitForSeconds(0.1f);
        landing = false;
    }
    public IEnumerator Blinking()
    {
        if (!singing)
        {
            blinkOverride = true;
            blink.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            blink.SetActive(false);
            blinkOverride = false;
        }
        yield return new WaitForSeconds(Random.Range(2, 3));
        StartCoroutine(Blinking());
    }
    public IEnumerator GravShift()
    {
        Instantiate(shroomAudio, gameObject.transform);
        if (gravState == 0)
        {
            gravState = 1;
        }
        else if (gravState == 1)
        {
            gravState = 0;
        }
        gravTouch = true;
        yield return new WaitForEndOfFrame();
        gravTouch = false;
        gravWait = true;
        yield return new WaitForSeconds(0.4f);
        gravWait = false;
        
    }
}
