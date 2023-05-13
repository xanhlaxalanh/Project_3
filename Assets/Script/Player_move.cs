using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
public class Player_move : MonoBehaviour
{
    [SerializeField] private Animator anim;

    public float speed;
    

    public NavMeshAgent agent;

    public float tip;

    public GameObject food_handling;
    private void Start() {
        agent = gameObject.GetComponent<NavMeshAgent>(); 
        agent.updateRotation = false;
        agent.updateUpAxis = false;   
    }
    public void move_key(){
        anim.SetBool("isIdle",true);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localScale = new Vector3(1, 1, 1);
            anim.SetBool("isIdle", false);
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.localScale = new Vector3(-1, 1, 1);
            anim.SetBool("isIdle", false);
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(new Vector3(0, -speed* Time.deltaTime, 0));
        }

        if(Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(new Vector3(0, speed* Time.deltaTime, 0));
        }
    }
    public void moving(GameObject target=null){
        StopAllCoroutines();
        if (target){
            if (target.CompareTag("customers"))
                agent.SetDestination(target.transform.position + new Vector3(-0.5f,-0f,0.0f));
            else if (target.CompareTag("stove"))
                agent.SetDestination(target.transform.position + new Vector3(0f,-0.5f,0.0f));
            StartCoroutine(wait_moving(target));
        }
        else{
            Debug.Log("edd");
            Vector3 curpos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,Camera.main.ScreenToWorldPoint(Input.mousePosition).y,0.0f);
            agent.SetDestination(curpos);
        }
    }
    IEnumerator wait_moving(GameObject target){
        agent.isStopped = false;
        yield return new WaitForSeconds(Time.deltaTime);
        yield return new WaitUntil(() => (agent.remainingDistance<=0.75f));
        agent.isStopped = true;
        interact(target);     
    }
    public void interact(GameObject target){
        if (target.CompareTag("customers")){
            if (target.GetComponent<Customers>().personal_status == Status.ORDERING){
                target.GetComponent<Customers>().give_order();
            }
            else if (target.GetComponent<Customers>().personal_status == Status.WAITING){
                if (food_handling)
                    target.GetComponent<Customers>().eat_food(food_handling);
            }
            else if (target.GetComponent<Customers>().personal_status == Status.CHECKOUT){
                target.GetComponent<Customers>().checkout();
            }
        }
        else if (target.CompareTag("stove")){
            if (target.GetComponent<Stove>().stostatus == StoveStatus.IDLE)
                target.GetComponent<Stove>().open_menu();
            else if (target.GetComponent<Stove>().stostatus == StoveStatus.DONE){
                food_handling = target.GetComponent<Stove>().get_food();
            }
        }
        
    }
    private void Update()
    {
        move_key();
        anim.SetBool("isIdle",true);
        if (agent.velocity != Vector3.zero)
            anim.SetBool("isIdle",false);
            anim.SetFloat("VelX",agent.velocity.x);
            anim.SetFloat("VelY",agent.velocity.y);
        
        if (Input.GetMouseButtonDown(0)){
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            GameObject tar = null;
            // Check if the ray hit object B
            if (hit.collider != null && (hit.collider.gameObject.tag == "customers"||hit.collider.gameObject.tag=="stove"))
            {
                // Perform the desired action here
                tar = hit.collider.gameObject;
            }
            if (tar != null){
                moving(tar);
            }
            else
                moving();
        }
        if (food_handling!=null){
            food_handling.transform.position = transform.position+ new Vector3(0.0f,0.4f,0.0f);
            food_handling.transform.localScale = new Vector3(0.7f,0.7f,0.7f);
        }
        if (Input.GetMouseButtonDown(1))
            if (food_handling)
                Destroy(food_handling);
    }
    
}
