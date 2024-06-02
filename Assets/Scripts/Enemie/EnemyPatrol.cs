using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    public GameObject PointA;
    public GameObject PointB;
    private Rigidbody2D rb;
    private Transform currentPoint;
    public float speed =2;
    public GameObject player;

    public GameObject Bullet;
    public Transform bulletPos;
    public float timer;

    public ParticleSystem laserVFX;

    public AudioSource laserFireSFX;
    public AudioSource laserChargeSFX;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = PointB.transform;
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
       
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if(distance < 10)
        {
            timer += Time.deltaTime;
            speed = 0;

            if(timer > 3)
                    {
                        timer = 0;
                        shoot();
                    }
            if (timer > 0.8)
            {
                speed = 2;
            }

        }

        

        Vector2 point = currentPoint.position - transform.position;
        if(currentPoint == PointB.transform)
        {
            //Point B needs to be on Right
            rb.velocity = new Vector2(speed, 0);
    


        }
        else
        {
            //Point A needs to be on left
            rb.velocity = new Vector2(-speed, 0);
    

        }

        if(Vector2.Distance(transform.position, currentPoint.position)< 0.6f && currentPoint == PointB.transform)
        {
            flip();
            currentPoint = PointA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.6f && currentPoint == PointA.transform)
        {
            flip();
            currentPoint = PointB.transform;
        }

        
        
    }


    private void flip()
    {
        Vector3 localscale = transform.localScale;
        localscale.x *= -1;
        transform.localScale = localscale;
  
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(PointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(PointB.transform.position, 0.5f);
    }

    void shoot()
    {
        Instantiate(Bullet, bulletPos.position, Quaternion.identity);
        laserChargeSFX.Play();
        laserFireSFX.Play();
        laserVFX.Play();
    }
}

