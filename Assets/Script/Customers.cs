using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
public enum Status{IDLE, ENTERING, ORDERING,WAITING, EATING, CHECKOUT, EXITING};
public class Customers : MonoBehaviour
{
    
    public float waitTimeCounter;
    public float maxWaitTime;
    [SerializeField] public GameObject food_ordered;
    [SerializeField] public GameObject bubble;
    public float mood = 100.0f;
    [SerializeField] public Sprite[] emotes;
    public Status personal_status;

    public Transform exit;
    public float wallet;

    public GameObject player;
    public int priority;
    
    public int dish;
    private NavMeshAgent agent;
    private List<GameObject> possible_dish;
    public GameObject slot;
    public GameObject restaurant;
    public GameObject spawner;
    private float total_price = 0.0f;
    public GameObject bubble_text;
    public Sprite coin;
    public GameObject currentSlot;
    // Start is called before the first frame update
    public Transform target; // assign the target in the Inspector
    public float speed = 1.0f; // adjust the speed as needed
    public GameObject finding_slot(){
            GameObject[] myObject = GameObject.FindGameObjectsWithTag("slot");
            foreach (GameObject obj in myObject){
                if (obj.GetComponent<slot>().guest_sit(gameObject)){
                    currentSlot = obj;
                    return obj;
                }
            }
        return null;
    }
    private void mood_display(){
        GameObject emo = gameObject.transform.GetChild(0).gameObject;
        if (emo != null){
            SpriteRenderer img = emo.GetComponent<SpriteRenderer>();
            if (mood < 1)
                img.sprite = emotes[10];
            else if (mood>=1 && mood <10)
                img.sprite = emotes[9];
            else if (mood>=10 && mood <20)
                img.sprite = emotes[8];
            else if (mood>=20 && mood <30)
                img.sprite = emotes[7];
            else if (mood>=30 && mood <40)
                img.sprite = emotes[6];
            else if (mood>=40 && mood <50)
                img.sprite = emotes[5];
            else if (mood>=50 && mood <60)
                img.sprite = emotes[4];
            else if (mood>= 60 && mood <70)
                img.sprite = emotes[3];
            else if (mood>=70 && mood <80)
                img.sprite = emotes[2];
            else if (mood>=80 && mood <90)
                img.sprite = emotes[1];
            else if (mood>=90 && mood <=100)
                img.sprite = emotes[0];
        }

    }
    public void customer_initialize(){
        int level = LevelHandler.level;
        maxWaitTime = Random.Range(0.0f,15.0f + 100.0f - (level-1)*8.0f);
        wallet = Random.Range(15.0f,100.0f);
        dish = (int)Random.Range(1.0f,3.1f);
    }
    void Start()
    {
        possible_dish = new List<GameObject>();
        personal_status = Status.IDLE;
        customer_initialize();
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        GameObject myObject = finding_slot();
        //Debug.Log(myObject);
        if (myObject != null)
        {
            //Debug.Log("Found the game object: " + myObject.name);
            target = myObject.GetComponent<slot>().chair_pos(gameObject);
            
            personal_status = Status.ENTERING;
            StartCoroutine(entering());
            
        }
        else{
            Destroy(gameObject);
        }
    }
    IEnumerator entering()
    {
        //yield return new WaitUntil(() => (agent.remainingDistance==0));
        agent.SetDestination(target.position);
        yield return new WaitForSeconds(Time.deltaTime);
        yield return new WaitUntil(() => (agent.remainingDistance<=0));
        // Code to execute after the condition is true
        StartCoroutine(ordering());

    }

    IEnumerator exiting(){
        Debug.Log("exti");
        currentSlot.GetComponent<slot>().guest_leave(gameObject);
        GameObject.Find("Handler").GetComponent<LevelHandler>().gain_rating(mood);
        Destroy(bubble_text);
        agent.SetDestination(spawner.transform.position);
        personal_status = Status.EXITING;
        yield return new WaitForSeconds(Time.deltaTime);
        yield return new WaitUntil(() => (agent.remainingDistance<=0));
        // Code to execute after the condition is true
        Destroy(gameObject);
    }

    public void give_order(){
        mood-=(waitTimeCounter/maxWaitTime)*20;
        possible_dish.Clear();
        StopCoroutine(ordering());
        List<GameObject> menu =GameObject.Find("Menu").GetComponent<Menu>().food;
        foreach (GameObject i in menu){
            if (i.GetComponent<code1>().price<=wallet)
                possible_dish.Add(i);
        }
        if ((dish == 0)||(possible_dish.Count<1))
            checkout();
        food_ordered = possible_dish[(int)Random.Range(0.0f,possible_dish.Count)];
        if (bubble_text!=null){
            Destroy(bubble_text);
        }
        bubble_text = Instantiate(bubble,transform.position + new Vector3(0.5f,0.5f,0.0f),Quaternion.identity);
        bubble_text.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().sprite = food_ordered.GetComponent<SpriteRenderer>().sprite;
        personal_status = Status.WAITING;
        StartCoroutine(waiting());    
    }
    IEnumerator ordering()
    {
        Debug.Log("time to order");
        bubble_text = Instantiate(bubble,transform.position + new Vector3(0.5f,0.5f,0.0f),Quaternion.identity);
        waitTimeCounter = maxWaitTime;
        personal_status = Status.ORDERING;
        yield return new WaitForSeconds(Time.deltaTime);
        yield return new WaitUntil(() => (personal_status==Status.ORDERING&&waitTimeCounter<=0));
        if (personal_status==Status.ORDERING){
            if (waitTimeCounter<=0)
                mood = 0;
                StartCoroutine(exiting());
        }
    }
    void move(){
        if (personal_status != Status.ENTERING)
            return;
        // calculate the horizontal and vertical distances to the target
        float dx = target.position.x - transform.position.x;
        float dy = target.position.y - transform.position.y;

        // move horizontally if not aligned with the target
        if (Mathf.Abs(dx) > 0.1f)
        {
            transform.position += new Vector3(Mathf.Sign(dx) * speed * Time.deltaTime, 0, 0);
        }
        // move vertically if aligned with the target
        else if (Mathf.Abs(dy) > 0.1f)
        {
            transform.position += new Vector3(0, Mathf.Sign(dy) * speed * Time.deltaTime, 0);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        //finding_slot();
        //move();
        waitTimeCounter-=Time.deltaTime;

        if (bubble_text){
            bubble_text.transform.position = transform.position + new Vector3(0.5f,0.5f,0.0f);
        }
        //Debug.Log(mood);
        if (agent.remainingDistance<=0){
            gameObject.GetComponent<Animator>().SetBool("isIdle",true);
            gameObject.GetComponent<Animator>().SetFloat("VelY",-1.0f);
        }
        else{
            gameObject.GetComponent<Animator>().SetBool("isIdle",false);
            gameObject.GetComponent<Animator>().SetFloat("VelX",agent.velocity.x);
            gameObject.GetComponent<Animator>().SetFloat("VelY",agent.velocity.y);

        }
        mood_display();
    }
    private void OnMouseDown() {
        //GameObject.FindGameObjectWithTag("Player").GetComponent<Player_move>().moving(gameObject);
    }
    IEnumerator eating(){
        dish -=1;
        personal_status = Status.EATING;
        yield return new WaitForSeconds(Time.deltaTime);
        yield return new WaitForSeconds(5.0f);
        /*
        possible_dish.Clear();
        //StopAllCoroutines();
        List<GameObject> menu =GameObject.Find("Menu").GetComponent<Menu>().food;
        foreach (GameObject i in menu){
            if (i.GetComponent<code1>().price<=wallet)
                possible_dish.Add(i);
        }
        if ((dish == 0)||(possible_dish.Count<1))
            wait_for_checkout();
        else
            ordering();
        */
        wait_for_checkout();
    }
    IEnumerator waiting()
    {
        //Debug.Log("time to order");
        waitTimeCounter = maxWaitTime;
        personal_status = Status.WAITING;
        yield return new WaitForSeconds(Time.deltaTime);
        yield return new WaitUntil(() => (waitTimeCounter<=0 && personal_status==Status.WAITING));
        if (personal_status==Status.WAITING){
            if (waitTimeCounter<=0)
                mood = 0;
                StartCoroutine(exiting());
        }
    }
    public void eat_food(GameObject food){
        StopAllCoroutines();
        mood-=((maxWaitTime - waitTimeCounter)/maxWaitTime)*20;
        Debug.Log(food.name.Replace("(Clone)","") + " D-" + food_ordered.name);
        wallet -= food_ordered.GetComponent<code1>().price;
        Destroy(bubble_text);
        if (food.name.Replace("(Clone)","") == "D-" + food_ordered.name){
            mood += food_ordered.GetComponent<code1>().yummy;
        }
        else{
            mood -= 30.0f;
            if (mood<=0){
                StartCoroutine(exiting());
            }
        }
        Destroy(food);
        StartCoroutine(eating());
    }
    public void wait_for_checkout(){
        personal_status = Status.CHECKOUT;
        Debug.Log("checkout");
        bubble_text = Instantiate(bubble,transform.position + new Vector3(0.5f,0.5f,0.0f),Quaternion.identity);
        bubble_text.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().sprite = coin;
    }
    public void checkout(){
        player = GameObject.FindGameObjectWithTag("player");
        player.GetComponent<Player_move>().tip += 0.001f * Random.Range(0.0f,mood) * wallet;
        Destroy(bubble_text);
        StartCoroutine(exiting());
    }
    private void OnDestroy() {
        Destroy(bubble_text);
    }
}
