using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokePuff : MonoBehaviour
{
    public SpriteRenderer puffSR;
    public float riseSpeed;
    public float driftSpeed;
    public float alphaValue;

    public bool blocked;

    // Start is called before the first frame update
    void Start()
    {
        driftSpeed = Random.Range(-1f, 1f);
        gameObject.transform.parent = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!blocked)
        {
            transform.Translate(new Vector2(0, riseSpeed * Time.deltaTime));
        }
        else
        {
            alphaValue -= 0.01f;
            transform.Translate(new Vector2(driftSpeed * Time.deltaTime, 0));
        }

        alphaValue -= 0.005f;

        puffSR.color = new Color(1, 1, 1, alphaValue);

        if(alphaValue < 0.01)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            blocked = true;
        }
    }
}
