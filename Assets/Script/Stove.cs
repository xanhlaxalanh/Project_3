using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum StoveStatus{IDLE, WORKING, DONE};
public class Stove : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject food;
    public float remainingTime;

    public GameObject bubble;
    private GameObject bub;

    public StoveStatus stostatus = StoveStatus.IDLE;
    public GameObject menu;
    void Start()
    {
        
    }
    public void open_menu(){
        GameObject men = Instantiate(menu,Vector3.zero,Quaternion.identity);
        
        men.transform.SetParent(GameObject.Find("Canvas").transform);
        men.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        men.GetComponent<Menu>().stove = gameObject;
        
    }
    public void cook(GameObject food){
        remainingTime = 5.0f;
        this.food = food;
        stostatus = StoveStatus.WORKING;
    }
    public GameObject get_food(){
        if (stostatus == StoveStatus.DONE){
            GameObject temp = this.food.GetComponent<code1>().prefab;
            this.food = null;
            Destroy(bub);
            stostatus = StoveStatus.IDLE;
            return Instantiate(temp,transform.position,Quaternion.identity);
        }
        return null;
    }
    // Update is called once per frame
    void Update()
    {
        if (stostatus==StoveStatus.WORKING){
            remainingTime-=Time.deltaTime;
            if (remainingTime<=0){
                stostatus = StoveStatus.DONE;
                bub = Instantiate(bubble,transform.position+new Vector3(1.0f,1.0f,0.0f),Quaternion.identity);
                bub.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = this.food.GetComponent<SpriteRenderer>().sprite;
            }
        }
        if (stostatus==StoveStatus.WORKING){
            GetComponent<Animator>().SetBool("isworking",true);
        }
        else{
            GetComponent<Animator>().SetBool("isworking",false);
        }
    }
}
