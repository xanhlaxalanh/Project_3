using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int t = (int)GameObject.Find("Handler").GetComponent<LevelHandler>().time;
        GameObject.Find("Tip").transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = GameObject.FindGameObjectWithTag("player").GetComponent<Player_move>().tip.ToString() + "$";
        GameObject.Find("time").GetComponent<TextMeshProUGUI>().text = "DAY " + LevelHandler.level.ToString();
        GameObject.Find("time").GetComponent<TextMeshProUGUI>().text += " - " + (t/60).ToString()+":"+(t%60).ToString();
        GameObject.Find("rating").transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = ((int)(LevelHandler.avg_rate)).ToString();
    }
}
