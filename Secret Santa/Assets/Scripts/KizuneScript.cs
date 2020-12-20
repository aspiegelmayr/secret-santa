using UnityEngine;

public class KizuneScript : MonoBehaviour
{
    public void OnAnimationEnd()
    {
        gameObject.GetComponent<Animation>().Stop();
    }
}
