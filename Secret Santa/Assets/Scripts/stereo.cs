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
                InvokeRepeating("LaunchProjectile", 0f, shootInterval);
            }else{
                CancelInvoke("LaunchWave");
            }
        }
    }

    private List<GameObject> pooledProjectiles;
    private int maxProjectiles = 20;
    // Start is called before the first frame update
    void Start()
    {
        shooting = true;

        pooledProjectiles = new List<GameObject>();
        for(int i=0; i<maxProjectiles; i++){
            GameObject obj = (GameObject)Instantiate(projectile);
            obj.SetActive(false);
            pooledProjectiles.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LaunchProjectile(){
        //Instantiate(projectile, transform.position+transform.forward*-1f, Quaternion.LookRotation(target.position - transform.position, Vector3.up));
        GameObject pro = getProjectile();
        if(pro != null){
            pro.transform.position = transform.position + transform.forward * -0.5f;
            pro.transform.rotation = Quaternion.LookRotation(target.transform.position - pro.transform.position, Vector3.up);
            pro.GetComponent<SoundWave_Projectile>().startTimer();
            pro.SetActive(true);
        }
    }

    GameObject getProjectile(){
        for(int i=0; i<maxProjectiles; i++){
            if(!pooledProjectiles[i].activeSelf){
                return pooledProjectiles[i];
            }
        }
        
        return null;
    }
}
