using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieMovement : MonoBehaviour
{
    float speed = 5f;

    bool toPointOne = true;
    bool toPointTwo = false;

    Transform target1;
    Transform target2;

    public GameObject moveTo;
    public GameObject moveTo2;


    private void Awake()
    {
        target1 = moveTo.transform;
        target2 = moveTo2.transform;
    }

    private void Update()
    {
        var moving = speed * Time.deltaTime;

        if (toPointOne == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, target1.position, moving);
            transform.localScale = new Vector3(1f, 1f, 1f);

            if (Vector3.Distance(transform.position, target1.position) < 0.001f)
            {
                toPointTwo = true;
                toPointOne = false;
            }
        }

        if (toPointTwo == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, target2.position, moving);
            transform.localScale = new Vector3(-1f, 1, 1f);

            if (Vector3.Distance(transform.position, target2.position) < 0.001f)
            {
                toPointOne = true;
                toPointTwo = false;
            }

        }

    }
}
