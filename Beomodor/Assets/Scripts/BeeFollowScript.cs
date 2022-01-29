using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeFollowScript : MonoBehaviour
{
    private GameObject player;
    public ParticleSystem beeParticle;

    public float beeSpeed;
    public bool sleeping; 

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (sleeping)
        {
            transform.position = transform.position + Vector3.down * beeSpeed * Time.deltaTime;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("SingRadius") && !sleeping)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, beeSpeed * Time.deltaTime);
        }
        if (other.CompareTag("Smoke"))
        {
            Debug.Log("Smoked");
            StartCoroutine(Sleep());
        }
    }
    public IEnumerator Sleep()
    {
        yield return new WaitForSeconds(0.2f);
        sleeping = true;
        beeParticle.gravityModifier = 0.5f;
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
