using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBees : MonoBehaviour
{
    public bool destroyBees;
    public Animator outputAnimation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bees"))
        {
            outputAnimation.SetBool("On", true);
            if (destroyBees)
            {
                Destroy(other.gameObject, 0.2f);
            }
        }
    }
}
