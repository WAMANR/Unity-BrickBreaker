using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour
{

    public int value;
    public int life;
    public int sprite;
    public Sprite hitSprite;
    public Sprite twoHitSprite;
    public AudioSource hitSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeHit()
    {
        life--;
        sprite++;
        Debug.Log("Sprite : " + sprite);

        switch (sprite)
        {
            case 1:
                GetComponent<SpriteRenderer>().sprite = hitSprite;
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = twoHitSprite;
                break;
        }
        
    }
}
