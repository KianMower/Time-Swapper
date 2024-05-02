using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class EnemieMovement : MonoBehaviour
{
    float speed = 5f;
    float bulletSpeed = 1000f;
        
    bool toNextPoint = true;
    bool canFire = true;   

    Transform target1;
    Transform target2;

    public GameObject bullet;
    public GameObject bulletFirePoint;
    public GameObject moveTo;
    public GameObject moveTo2;
    public GameObject player;   

    private void Awake()
    {
        target1 = moveTo.transform;
        target2 = moveTo2.transform;
    }

    private void Update()
    {
        var moving = speed * Time.deltaTime;

        if (toNextPoint)
        {
            transform.position = Vector3.MoveTowards(transform.position, target1.position, moving);
            transform.localScale = new Vector3(1f, 1f, 1f);

            Debug.DrawRay(transform.position, transform.TransformDirection(-Vector2.right) * 5f, Color.red);

            RaycastHit2D hit = Physics2D.Raycast(bulletFirePoint.transform.position, bulletFirePoint.transform.TransformDirection(-Vector2.right), 5f);

            if (hit)
            {
                speed = 0;
                if (canFire && hit.collider.name == "Player")
                {
                    StartCoroutine(fire());
                }
            }
            else
            {
                speed = 5;
            }

            if (Vector3.Distance(transform.position, target1.position) < 0.001f)
            {
                toNextPoint = false;                
            }
        }

        if (!toNextPoint)
        {
            transform.position = Vector3.MoveTowards(transform.position, target2.position, moving);
            transform.localScale = new Vector3(-1f, 1, 1f);

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * 5f, Color.red);

            RaycastHit2D hit = Physics2D.Raycast(-bulletFirePoint.transform.position, -bulletFirePoint.transform.TransformDirection(Vector2.right), 5f);

            if (hit)
            {
                speed = 0;
                if (canFire && hit.collider.name == "Player")
                {
                    StartCoroutine(fire());
                }
            }
            else
            {
                speed = 5;
            }

            if (Vector3.Distance(transform.position, target2.position) < 0.001f)
            {
                toNextPoint = true;                
            }
        }
        
    }

    IEnumerator fire()
    {
        if (toNextPoint)
        {
            GameObject cloneBullet = Instantiate(bullet, bulletFirePoint.transform.position, transform.rotation);
            Rigidbody2D cloneBulletRb = cloneBullet.GetComponent<Rigidbody2D>();

            cloneBulletRb.AddForce(-bulletFirePoint.transform.right * bulletSpeed);

            canFire = false;

            yield return new WaitForSeconds(2f);

            canFire = true;
        }

        if (!toNextPoint)
        {
            GameObject cloneBullet = Instantiate(bullet, bulletFirePoint.transform.position, transform.rotation);
            Rigidbody2D cloneBulletRb = cloneBullet.GetComponent<Rigidbody2D>();

            cloneBulletRb.AddForce(bulletFirePoint.transform.right * bulletSpeed);

            canFire = false;

            yield return new WaitForSeconds(2f);

            canFire = true;
        }
    }
}
