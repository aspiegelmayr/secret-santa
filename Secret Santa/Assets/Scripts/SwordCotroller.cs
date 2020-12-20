using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCotroller : MonoBehaviour
{
    private BoxCollider col;

    void Start()
    {
        col = GetComponent<BoxCollider>();
        col.enabled = false;
    }

    public void AttackStart()
    {
        col.enabled = true;
    }

    public void AttackEnd()
    {
        col.enabled = false;
    }
}
