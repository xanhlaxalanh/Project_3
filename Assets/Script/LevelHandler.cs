using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public static int level = 1;
    public float time = 800;
    public static int guest_num = 0;
    public static float avg_rate = 0;
    public GameObject cus;

    public static float money;
    IEnumerator endgame(){
        yield return new WaitUntil(() => (time>=1320));
        money += GameObject.FindGameObjectWithTag("player").GetComponent<Player_move>().tip;
        SceneManager.LoadScene("Assets/Scenes/result.unity",LoadSceneMode.Single);
    }
    void Start()
    {
        time = 420;
        StartCoroutine(endgame());
    }
    public void gain_rating(float rating){
        avg_rate = (avg_rate*guest_num + rating)/(guest_num+1);
        guest_num++;
    }
    // Update is called once per frame
    void spawning(){
        if (time<=1320 && Random.Range(0.0f,50000.0f-100.0f*level)<=10.0f){
            GameObject temp = Instantiate(cus,GameObject.Find("Spawner").transform.position,Quaternion.identity);
            temp.GetComponent<Customers>().spawner = GameObject.Find("Spawner");
        }
    }
    void Update()
    {
        time += Time.deltaTime*5;
        spawning();
        //money = GameObject.FindGameObjectWithTag("player").GetComponent<Player_move>().tip;
    }
}
