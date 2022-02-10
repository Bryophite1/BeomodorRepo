using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeByPlayerProximity : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer spriteRenderer;
    
    /*
    public Color colorFadeOut;
    public Color colorFadeIn;
    */
    public Color spriteColor;
    public Color nearSpriteColor;
    public Color farSpriteColor;

    public float radius;
    public float distx;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {         
        distx = Mathf.Abs(gameObject.transform.position.x - player.transform.position.x);
        
        if (distx < radius)
        {
            //spriteColor = (spriteColor + colorFadeIn);
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, nearSpriteColor, Time.deltaTime*timer);
        }
        else
        {
            //spriteColor = (spriteColor + colorFadeOut);
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, farSpriteColor, Time.deltaTime*timer);
        }

        /*
        if(distx < radius)
        {
            spriteRenderer.color = Color.Lerp(spriteColor,colorFadeIn, timer);
            spriteColor = colorFadeIn;
        }

        if (distx > radius)
        {
            spriteRenderer.color = Color.Lerp(spriteColor, colorFadeOut, timer);
            spriteColor = colorFadeOut;
        }

        if (spriteRenderer.color != colorFadeOut)
        {
            timer += Time.deltaTime;

            Debug.Log("color should change: " + spriteRenderer.color);
        }
        */
    }
}
