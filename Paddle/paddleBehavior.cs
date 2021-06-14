using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paddleBehavior : MonoBehaviour
{
    public float speed;
    public float rightScreenEdge;
    public float leftScreenEdge;
    public GameManager gm;
    public AudioSource powerUpSound;
    public bool canonModeActive;
    float elapsedTime;
    public float fireRate;
    public Transform projectile;

    // Start is called before the first frame update
    void Start()
    {
        powerUpSound = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gameOver)
        {
            return;
        }

        float horizontal = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * horizontal * Time.deltaTime * speed);
        if(transform.position.x < leftScreenEdge)
        {
            transform.position = new Vector2(leftScreenEdge, transform.position.y);
        }
        else if(transform.position.x > rightScreenEdge)
        {
            transform.position = new Vector2(rightScreenEdge, transform.position.y);
        }

        if (canonModeActive)
        {
            FireProjectile();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ExtraLife"))
        {
            powerUpSound.Play();
            gm.UpdateLives(1);
            Destroy(other.gameObject);

        }
        else if (other.CompareTag("CanonMode"))
        {
            powerUpSound.Play();
            canonModeActive = true;
            Debug.Log("enable");
            StartCoroutine(FireRoutine());
            Destroy(other.gameObject);

        }
    }

    private IEnumerator FireRoutine()
    {
        yield return new WaitForSeconds(10);
        canonModeActive = false;
        Debug.Log("unable");
    }

    private void FireProjectile()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= fireRate)
        {
            Debug.Log("fire");
            elapsedTime = 0f;
            projectile = Instantiate(projectile, new Vector2(transform.position.x, -3f), Quaternion.identity);
        }
    }
}
