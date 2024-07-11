using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private bool OnFloor()
    {
        //return Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, layerGround);
        return true;
    }
}
