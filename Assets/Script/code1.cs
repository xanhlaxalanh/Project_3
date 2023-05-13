using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class code1 : MonoBehaviour
{
    public GameObject prefab; 
    public float eating_time;

    public float price;

    public int quality = 0;

    public float yummy;
    
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
