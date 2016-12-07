using UnityEngine;
using System.Collections;

public class NP_Boarder : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag != "Player")
        {
            return;
        }

        anim.SetTrigger("Show");
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag != "Player")
        {
            return;
        }

        anim.SetTrigger("Hide");
    }
}