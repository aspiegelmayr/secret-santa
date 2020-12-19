using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTile : MonoBehaviour
{
    public Rigidbody2D body;
    float speed = 15.0f;
    bool moving = false;
    bool win = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(!moving && !win){
            if(Input.GetKey(KeyCode.W)){
            body.velocity = Vector2.up * 70.0f;
            moving = true;
        }
        if(Input.GetKey(KeyCode.A)){
            body.velocity = Vector2.left * 70.0f;
            moving = true;
        }
        if(Input.GetKey(KeyCode.S)){
            body.velocity = Vector2.down * 70.0f;
            moving = true;
        }
        if(Input.GetKey(KeyCode.D)){
            body.velocity = Vector2.right * 70.0f;
            moving = true;
        }
        }
    }
        void OnCollisionEnter2D(Collision2D other){
            body.velocity = Vector2.down * 0.0f;
            moving = false;
            if(other.gameObject.tag == "goal1")
            win = true;
    }

}