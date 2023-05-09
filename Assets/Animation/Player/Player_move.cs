using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_move : MonoBehaviour
{
    [SerializeField] private Animator anim;
    public float speed;

    private void Update()
    {
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
    }
}
