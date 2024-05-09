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
       }

    }   

    public bool isStuck() //This function works :)
    {
        //Does a raycast and if it collides hits object on ground layer, we are stuck so it returns true!
        RaycastHit2D check = Physics2D.Raycast(transform.position, Vector2.zero, Mathf.Infinity, ground);
        return check.collider != null;
    }

    private void resetPos()
    {
        
    }

}