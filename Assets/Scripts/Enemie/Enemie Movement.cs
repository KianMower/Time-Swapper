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
            bulletFirePoint.transform.localScale = new Vector3(1f, 1, 1);

            Debug.DrawRay(bulletFirePoint.transform.position, bulletFirePoint.transform.TransformDirection(Vector2.left) * 5f, Color.red);

            RaycastHit2D hit = Physics2D.Raycast(bulletFirePoint.transform.position, bulletFirePoint.transform.TransformDirection(Vector2.left), 5f);

            if (hit)
            {
                Debug.Log(hit.collider.name);
                if (canFire && hit.collider.name == "Player")
                {
                    speed = 0;
                    Debug.Log("HIT Left");
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

        //Moving to the Right
        if (!toNextPoint)
        {
            transform.position = Vector3.MoveTowards(transform.position, target2.position, moving);
            transform.localScale = new Vector3(-1f, 1, 1f);
            bulletFirePoint.transform.localScale = new Vector3(-1f, 1, 1);

            Debug.DrawRay(bulletFirePoint.transform.position, bulletFirePoint.transform.TransformDirection(Vector2.left) * 5f, Color.red);

            RaycastHit2D hit = Physics2D.Raycast(bulletFirePoint.transform.position, bulletFirePoint.transform.TransformDirection(Vector2.left), 5f);

            if (hit)
            {
                Debug.Log(hit.collider.name);

                if (canFire && hit.collider.name == "Player")
                {
                    speed = 0;
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
