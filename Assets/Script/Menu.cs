using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public List<GameObject> food;
    public GameObject stove;
    public void cook(GameObject foods){
        stove.GetComponent<Stove>().cook(foods);
        close();
    }
    public void close(){
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
