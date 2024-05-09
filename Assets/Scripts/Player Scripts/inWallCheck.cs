using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inWallCheck : MonoBehaviour
{
    [SerializeField] LayerMask ground;
    private Vector3 edgeDirection;
    //Distance to teleport the player if we are stuck
    public float teleportDistance = 1f; 

    private void Awake()
    {
        
    }

    private void Update()
    {
       //Debug.Log(isStuck());
       if(isStuck())
       {
            Debug.Log("is stuck");
            //transform.position = resetPos();
            resetPos();
       }

    }   

    public bool isStuck()
    {
        //Does a raycast and if it collides hits object on ground layer, we are stuck so it returns true!
        RaycastHit2D check = Physics2D.Raycast(transform.position, Vector2.zero, Mathf.Infinity, ground);
        return check.collider != null;
    }

    private void resetPos()
    {
        int angleStep = 10;
        for (int angle = 0; angle < 360; angle += angleStep)
        {
            Vector2 raycastDirection = Quaternion.Euler(0, 0, angle) * transform.right;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, raycastDirection, 10f, ~ground);
            Debug.Log("Angle: " + angle + ", Distance: " + hit.distance + ", Collider Name: " + (hit.collider != null ? hit.collider.gameObject.name : "None"));
        }
    }

}