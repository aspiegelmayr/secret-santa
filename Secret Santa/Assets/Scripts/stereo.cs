using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stereo : MonoBehaviour
{
    [SerializeField] float shootInterval = 1f;
    [SerializeField] Transform target;
    [SerializeField] GameObject projectile;
    public bool shooting{
        set{
            if(value){
                InvokeRepeating("LaunchWave", 0f, shootInterval);
            }else{
                CancelInvoke("LaunchWave");
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        shooting = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LaunchWave(){
        Instantiate(projectile, transform.position, Quaternion.LookRotation(target.position - transform.position, Vector3.up));
    }
}
