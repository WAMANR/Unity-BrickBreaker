using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{

    public int speed;
    public Transform explosion;
    public Transform extraLife;
    public Transform canonMode;
    public AudioSource hitSound;
    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * speed);

        if(transform.position.y >= 4.9f)
        {
            Destroy(gameObject);
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
            Destroy(gameObject);
        }
        

    }
}
