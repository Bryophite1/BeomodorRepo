using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive : MonoBehaviour
{
    public GameObject beeSwarm;
    private GameObject[] beeSwarms;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SingRadius") && playerController.startSinging)
        {
            beeSwarms = GameObject.FindGameObjectsWithTag("Bees");

            for (int i=0; i < beeSwarms.Length; i++)
            {
                Destroy(beeSwarms[i]);
            }
                

            Instantiate(beeSwarm, gameObject.transform);
        }
    }
}
