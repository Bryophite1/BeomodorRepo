using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSetter : MonoBehaviour
{
    public Animator treeAnimator;
    public int currentInteger;
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
        if (other.CompareTag("Player"))
        {
            currentInteger = treeAnimator.GetInteger("WalkwayNumber");
            currentInteger += 1;
            treeAnimator.SetInteger("WalkwayNumber",currentInteger);
            Destroy(this.gameObject);
        }
    }
}
