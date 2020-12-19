using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTileTwo : MonoBehaviour
{
    public Rigidbody2D body;
    float speed = 15.0f;
    bool moving = false;
    bool win = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(!moving && !win){
            if(Input.GetKey(KeyCode.UpArrow)){
            body.velocity = Vector2.up * 70.0f;
            moving = true;
        }
        if(Input.GetKey(KeyCode.LeftArrow)){
            body.velocity = Vector2.left * 70.0f;
            moving = true;
        }
        if(Input.GetKey(KeyCode.DownArrow)){
            body.velocity = Vector2.down * 70.0f;
            moving = true;
        }
        if(Input.GetKey(KeyCode.RightArrow)){
            body.velocity = Vector2.right * 70.0f;
            moving = true;
        }
        }
    }
        void OnCollisionEnter2D(Collision2D other){
            if(moving){
                body.velocity = Vector2.down * 0.0f;
            moving = false;
            }

            if(other.gameObject.tag == "goal2"){
                win = true;
            }
    }

} 