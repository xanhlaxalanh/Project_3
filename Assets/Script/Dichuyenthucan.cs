using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dichuyenthucan : MonoBehaviour
{
    public LayerMask layerMask; 
    private GameObject targetObject; 
    private Transform followTransform; 
    private Vector3 offset; 

    private void Start()
    {
        targetObject = gameObject; 
        followTransform = targetObject.transform;

        offset = Vector3.zero;
    }

    private void LateUpdate()
    {
        Collider2D hit = Physics2D.OverlapBox(followTransform.position, followTransform.localScale, 0, layerMask);

        if (hit != null)
        {
            offset = hit.transform.position - followTransform.position;
            followTransform.position += offset;
        }
        else
        {
            offset = Vector3.zero;
        }
    }
}
