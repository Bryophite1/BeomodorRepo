using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronSmoker : MonoBehaviour
{
    public Rigidbody2D ironSmokerRB;
    public GameObject[] patrolNodes;
    public GameObject smokePuff;
    public GameObject smokeOrigin;
    public Animator ironSmokerAnim;
    public Vector3 scaleChange;

    public float walkSpeed;
    public float waitLength;

    public bool walking;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SmokePuff());
        StartCoroutine(WaitToWalk());
        scaleChange = transform.localScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (walking && transform.localScale.x > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolNodes[0].transform.position, walkSpeed * Time.deltaTime);
        }
        else if (walking && transform.localScale.x < 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolNodes[1].transform.position, walkSpeed * Time.deltaTime);
        }

        if (transform.position.x <= patrolNodes[0].transform.position.x && walking && transform.localScale.x > 0)
        {
            StartCoroutine(WaitToWalk());
        }
        if (transform.position.x >= patrolNodes[1].transform.position.x && walking && transform.localScale.x < 0)
        {
            StartCoroutine(WaitToWalk());
        }

        //Animations
        if (walking)
        {
            ironSmokerAnim.SetBool("Walk", true);
        }
        else
        {
            ironSmokerAnim.SetBool("Walk", false);
        }

    }
    public IEnumerator SmokePuff()
    {
        Instantiate(smokePuff, smokeOrigin.transform);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(SmokePuff());
    }
    public IEnumerator WaitToWalk()
    {
        walking = false;

        yield return new WaitForSeconds(waitLength);
        walking = true;
        if (transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-scaleChange.x, scaleChange.y, scaleChange.z);
        }
        else if (transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(scaleChange.x, scaleChange.y, scaleChange.z);
        }
    }
}
