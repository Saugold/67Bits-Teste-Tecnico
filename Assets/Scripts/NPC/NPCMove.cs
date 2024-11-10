using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMove : MonoBehaviour
{
    [Header("Patrol Settings")]
    [SerializeField] private float patrolRadius = 10f;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Vector3 targetPosition;

    [Header("States")]
    public bool isWaiting = false;
    public bool isPathing = false;
    public bool isSleep = false;
    public bool isCarrying = false;
    public bool canTake = false;



    private Rigidbody rb;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform colPos;

    [Header("Take Damage SFX")]
    [SerializeField] private AudioSource damageAudioSource;
    [SerializeField] private AudioClip[] damageAudioClip;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        isSleep = false;
    }

    void Start()
    {
        if (isSleep == false)
        {
            ChooseRandomDestination();  
        }
    }

    void Update()
    {

        animator.SetBool("isWalking", isPathing);
        if (!isSleep && !isCarrying)
        {
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && !isWaiting && !isSleep && !isCarrying)
            {
                StartCoroutine(WaitAndChooseNewDestination());
            }

        }
        if(rb.velocity.magnitude < 0.1f)
        {
            isPathing = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Punch"))
        {
            TakeDamage();
        }
        else
        {
            Debug.Log("Nahh");
        }
    }

    public void TakeDamage()
    {
        agent.enabled = false;
        isPathing = false;
        animator.enabled = false;
        isSleep = true;
        damageAudioSource.PlayOneShot(damageAudioClip[Random.Range(0, damageAudioClip.Length)]);
        Rigidbody rb = GetComponent<Rigidbody>();
        //if (rb != null)
        //{
        //    rb.AddForce(Vector3.down * 50f);
        //}

        StartCoroutine(WaitCanTake());
    }
    private IEnumerator WaitCanTake()
    {
        yield return new WaitForSeconds(1.5f);
        rb.constraints = RigidbodyConstraints.FreezePositionY;


        canTake = true;
    }
    public void DisableAllColliders()
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider col in colliders)
        {
            col.enabled = false;
        }
    }
    public void Carry()
    {
        DisableAllColliders();
        isPathing = false;
        isSleep = false;
        isCarrying = true;
    }
    // ---------PATRULHA--------------------------------------------------------------------------   
    void ChooseRandomDestination()
    {
        if (agent.enabled)
        {
            isPathing = true;
            Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
            randomDirection += transform.position;

            NavMeshHit navHit;

            if (NavMesh.SamplePosition(randomDirection, out navHit, patrolRadius, NavMesh.AllAreas))
            {
                targetPosition = navHit.position;
                agent.SetDestination(targetPosition);
            }
        }
    }

    IEnumerator WaitAndChooseNewDestination()
    {
        isPathing = false;
        isWaiting = true;
        float randomWaitTime = Random.Range(3f, 6f);
        yield return new WaitForSeconds(randomWaitTime);
        ChooseRandomDestination();
        isWaiting = false;
    }
}
