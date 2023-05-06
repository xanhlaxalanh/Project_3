using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class code2 : MonoBehaviour
{
    private Vector3 screenPoint; 
    private Vector3 offset; 
    private bool isDragging = false; 
    private void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        isDragging = true;
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }
}
