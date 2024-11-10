using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private Collider playerCol;
    private Animator animator;
    private void Awake()
    {
        playerCol = GetComponent<Collider>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            if (other.gameObject.GetComponent<NPCMove>() != null)
            {
                if (other.gameObject.GetComponent<NPCMove>().isSleep == true)
                {
                    Debug.Log("Esta dormindo");
                    //StackManager.instance.AddToStack(other.transform);
                }
                else
                {
                    Punch();
                }
            }
        }
    }
    private void Punch()
    {
        Debug.Log("Socou");
        animator.SetTrigger("DoPunch");

    }
}

