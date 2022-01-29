using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeFollowScript : MonoBehaviour
{
    private GameObject player;

    public float beeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("SingRadius"))
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, beeSpeed * Time.deltaTime);
        }
    }
}
