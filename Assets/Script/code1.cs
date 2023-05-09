using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class code1 : MonoBehaviour
{
    public GameObject prefab; 
    
    void OnMouseDown()
    {
        StartCoroutine(CreateObjectWithDelay());
    }

    IEnumerator CreateObjectWithDelay()
    {
        yield return new WaitForSeconds(3f); 
        Instantiate(prefab, transform.position, transform.rotation); 
    }
}
