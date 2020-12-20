using System.Collections;
using System.Runtime;
using UnityEngine;

public class MaskMonster : Singleton<MaskMonster>
{
    public GameObject howlMask;
    private int gotHit = 0;
    [SerializeField] GameObject grunt1;   
    [SerializeField] GameObject grunt2;
    [SerializeField] GameObject grunt3;

    public void TurnAround()
    {
        howlMask.GetComponent<Animation>().Play();
        grunt1.GetComponent<AudioSource>().Play();
    }

    public void SetSpeed(float x)
    {
        if (x == 0.0f)
        {
            GetComponent<Animator>().Play("Idle");
            StartCoroutine(AttackPattern());
        }

        if (x == 1.0f)
        {
            GetComponent<Animator>().Play("Walk");
        }
    }

    public IEnumerator AttackPattern()
    {
        while (true)
        {
            yield return new WaitForSeconds(3.0f);
            GetComponent<Animator>().Play("Attack");
            grunt2.GetComponent<AudioSource>().Play();
        }
    }
    void OnTriggerEnter(Collider other){
        if(other.tag == "sword"){
            gotHit++;
            grunt3.GetComponent<AudioSource>().Play();
            if (gotHit >= 3)
            {
                gameObject.SetActive(false);
                UIManager.Instance.SetWon(true);
            }
        }
    }
}
