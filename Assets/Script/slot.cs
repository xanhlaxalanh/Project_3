using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slot : MonoBehaviour
{
    // Start is called before the first frame update
    public int max_seat;
    public List<GameObject> guest;
    public List<GameObject> chair;
    public int max_dish;
    public List<Transform> dish;

    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public bool guest_sit(GameObject gue){
        if (guest.Contains(gue)){
            return false;
        }
        if (guest.Count==chair.Count){
            return false;
        }
        guest.Add(gue);
        return true;
    }
    public bool guest_leave(GameObject gue){
        if (guest.Contains(gue)){
            guest.Remove(gue);
            return true;
        }
        return false;
    }
    public Transform chair_pos(GameObject gue){
        if (guest.Contains(gue)){
            return chair[guest.IndexOf(gue)].transform;
        }
        return null;
    }
    
}
