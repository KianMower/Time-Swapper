using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inWallCheck : MonoBehaviour
{
    Rigidbody2D rb;
    bool stuck = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isStuck())
        {
            transform.position = resetPos();
        }
    }

    private bool isStuck()
    {
        RaycastHit2D stuckCheck = Physics2D.Raycast(transform.position, Vector2.zero);
        //We are colliding with something
        if(stuckCheck.collider != null)
        {
            //Check if ray cast hits something that isn't the player
            if (stuckCheck.collider.tag != "Player") ;
            {
                //Debug.Log("in wall");
                stuck = true;
                return true;
            }
        }
        stuck = false;
        return false;
    }

    private Vector2 resetPos()
    {
        //Check around the player for a new pos to move
        for(int i = 0; i < 360; i+= 10)
        {
            Vector2 directionToMove = Quaternion.Euler(0, 0, i) * (Vector2.right * 5);
            RaycastHit2D checkPos = Physics2D.Raycast(transform.position, directionToMove);
            Vector3 direction = new Vector3(directionToMove.x, directionToMove.y, 0);
            if(checkPos.collider == null)
            {
                return transform.position + direction;
            }

        }

        return transform.position;
    }


}
