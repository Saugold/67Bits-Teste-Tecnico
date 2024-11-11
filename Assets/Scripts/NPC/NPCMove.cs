using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class NPCMove : MonoBehaviour
{
    [Header("Patrol Settings")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private List<Transform> patrolPoints;
    [SerializeField] private float waitTimeAtPoint = 2f;
    private int currentPatrolIndex = 0;
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
        isPathing = true;
    }

    void Start()
    {
        if (patrolPoints.Count == 0)
        {
        
            GameObject[] patrolObjects = GameObject.FindGameObjectsWithTag("PatrolPoint");
            patrolPoints = patrolObjects.Select(go => go.transform).ToList();
        }

        if (patrolPoints.Count > 0)
        {

            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
        else
        {
            Debug.LogWarning("Nenhum ponto de patrulha encontrado!");
        }
    }

    void Update()
    {

        animator.SetBool("isWalking", isPathing);
        if (!isSleep && !isCarrying)
        {
            if (!isWaiting && agent.remainingDistance <= agent.stoppingDistance)
            {
                StartCoroutine(WaitAndMoveToNextPoint());
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
    private IEnumerator WaitAndMoveToNextPoint()
    {
        isPathing = false;
        isWaiting = true;
        yield return new WaitForSeconds(waitTimeAtPoint);

       
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, patrolPoints.Count);
        } while (randomIndex == currentPatrolIndex); 

        currentPatrolIndex = randomIndex;

       
        if (NavMesh.SamplePosition(patrolPoints[currentPatrolIndex].position, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
            isPathing = true;
        }
        else
        {
            Debug.LogWarning("O ponto de patrulha não está na NavMesh!");
        }

        isWaiting = false;
    }

}
