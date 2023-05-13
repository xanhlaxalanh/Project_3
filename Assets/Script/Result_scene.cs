using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class Result_scene : MonoBehaviour
{
    bool levelup;
    GameObject a;
    GameObject b;
    GameObject c;
    GameObject d;
    GameObject e;
    IEnumerator showing(){
        a.SetActive(false);
        b.SetActive(false);
        c.SetActive(false);
        d.SetActive(false);
        e.SetActive(false);
        yield return new WaitForSeconds(1);
        a.SetActive(true);
        
        yield return new WaitForSeconds(1);
        b.SetActive(true);
        yield return new WaitForSeconds(1);
        c.SetActive(true);
        yield return new WaitForSeconds(1);
        if (!levelup){
            d.SetActive(true);
        }
        e.SetActive(true);
        
    }
    // Start is called before the first frame update
    void Start()
    {
        a = GameObject.Find("guest");
        b = GameObject.Find("Tip");
        c = GameObject.Find("rating");
        d = GameObject.Find("fired");
        e = GameObject.Find("proceed");
        GameObject.Find("guest").transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Number of Guest: " + LevelHandler.guest_num.ToString();
        GameObject.Find("Tip").transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Tip: " + LevelHandler.money.ToString();
        GameObject.Find("rating").transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Rating: "+ ((int)LevelHandler.avg_rate).ToString();
        levelup = (30<=LevelHandler.avg_rate);
        StartCoroutine(showing());
    }
    public void process(){
        if (!levelup)
            SceneManager.LoadScene("Assets/Scenes/M.unity",LoadSceneMode.Single);
        else{
            LevelHandler.level++;
            SceneManager.LoadScene("Assets/Scenes/SampleScene.unity",LoadSceneMode.Single);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
