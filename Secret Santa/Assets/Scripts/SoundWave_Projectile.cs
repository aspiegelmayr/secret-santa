using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWave_Projectile : MonoBehaviour
{
    [SerializeField] float speed = 4f;
    [SerializeField] float pushForce = 20f;
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
            CancelInvoke("Deactivate");
            gameObject.SetActive(false);
            other.GetComponent<PlayerMovement>().externalForce += transform.forward * pushForce;
        }
            
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
