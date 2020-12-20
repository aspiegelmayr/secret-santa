using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stereo : MonoBehaviour
{
    [SerializeField] float shootInterval = 1f;
    [SerializeField] Transform target;
    [SerializeField] GameObject projectile;
    

    private List<GameObject> pooledProjectiles;
    private int maxProjectiles = 5;
    [SerializeField] AudioSource soundsource;
    // Start is called before the first frame update
    void Start()
    {
        Deactivate();
        Invoke("Activate",5f);
        pooledProjectiles = new List<GameObject>();
        for(int i=0; i<maxProjectiles; i++){
            GameObject obj = (GameObject)Instantiate(projectile);
            obj.SetActive(false);
            obj.transform.parent = gameObject.transform;
            pooledProjectiles.Add(obj);
        }
    }

    public void Activate(){
        InvokeRepeating("LaunchProjectile", 0f, shootInterval);
        soundsource.mute = false;
    }
    public void Deactivate(){
        CancelInvoke("LaunchProjectile");
        soundsource.mute = true;
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

    void OnTriggerEnter(Collider other){
        if(other.tag == "sword"){
            Deactivate();
        }
    }
}
