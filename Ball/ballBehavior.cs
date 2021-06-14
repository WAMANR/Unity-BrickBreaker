using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballBehavior : MonoBehaviour
{

    public int speed;
    public bool inPlay;
    public Transform paddle;
    public Transform explosion;
    public Transform extraLife;
    public Transform canonMode;
    public Rigidbody2D rb;
    public GameManager gm;
    public BrickScript bs;
    public AudioSource hitSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hitSound = GetComponent<AudioSource>();
        

    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gameOver)
        {
            return;
        }
        if (!inPlay)
        {
            transform.position = new Vector2(paddle.position.x, -3.97f);
        }
        if (Input.GetKeyDown("space") && !inPlay)
        {
            inPlay = true;
            rb.AddForce(Vector2.up * speed);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bottom"))
        {
            Debug.Log("Ball out");
            rb.velocity = Vector2.zero;
            inPlay = false;
            gm.UpdateLives(-1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.transform.CompareTag("Brick"))
        {
            BrickScript brickScript = collision.gameObject.GetComponent<BrickScript>();

            if (brickScript.life > 1)
            {

                brickScript.takeHit();
            }

            else
            {

                hitSound.Play();
                int chanceExtraLife = Random.Range(1, 101);
                int chanceCanonMode = Random.Range(1, 101);
                if (chanceExtraLife < 50)
                {
                    extraLife = Instantiate(extraLife, collision.transform.position, Quaternion.identity);
                }

                else if (chanceCanonMode < 50)
                {
                    canonMode = Instantiate(canonMode, collision.transform.position, Quaternion.identity);
                }
                Transform newExplosion = Instantiate(explosion, collision.transform.position, collision.transform.rotation);
                Destroy(newExplosion.gameObject, 2.0f);
                Destroy(collision.gameObject);
                gm.UpdateScoreGain(brickScript.value);
                gm.UpdateBricksLeft();
            }
        }
        
    }







}
