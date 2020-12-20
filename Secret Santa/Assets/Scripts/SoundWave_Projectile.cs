using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWave_Projectile : MonoBehaviour
{
    [SerializeField] float speed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void startTimer(){
        Debug.Log("t");
        Invoke("Deactivate",5f);
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            //Destroy(gameObject);
            CancelInvoke("Deactivate");
            gameObject.SetActive(false);
        }
            
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
