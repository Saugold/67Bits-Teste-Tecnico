using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private float xMove;
    private float zMove;
    private Vector3 firstNPCPos;
    private Vector3 currentNPCPos;
    private float value;
    [SerializeField] private int NpcCapacity;
    [SerializeField] private Transform maxPoint;
    [SerializeField] private MeshRenderer meshPlayer;
    [SerializeField] private float npcValue;
    [SerializeField] public float stackPrice;
    [SerializeField] private float speed;
  
    public List<GameObject> npcList = new List<GameObject>();
    private int npcListIndexCounter = 0;
    public static StackManager Instance { get; private set; }
    public delegate void UpdateStackCapacity(int capacity);
    public event UpdateStackCapacity UpdatedStackCapacity;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        xMove = Input.GetAxis("Horizontal");
        zMove = Input.GetAxis("Vertical");
        Vector3 forwardMove = Vector3.forward * zMove * speed * Time.deltaTime;
        Vector3 horizontalMove = Vector3.right * xMove * speed * Time.deltaTime;
        rb.MovePosition(transform.position + forwardMove + horizontalMove);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<NPCMove>() != null && other.gameObject.GetComponent<NPCMove>().canTake == true)
        {
            if (npcList.Count < NpcCapacity)
            {
                npcList.Add(other.gameObject);
                other.GetComponent<NPCMove>().Carry();

                if (npcList.Count == 1)
                {

                    firstNPCPos = maxPoint.position;
                    currentNPCPos = new Vector3(other.transform.position.x, firstNPCPos.y, other.transform.position.z);
                    other.gameObject.transform.position = currentNPCPos;


                    other.gameObject.GetComponent<NPCStack>().UpdateNpcPosition(transform, true);


                    currentNPCPos = new Vector3(other.transform.position.x, other.transform.position.y + 1.0f, other.transform.position.z);
                }
                else
                {

                    GameObject lastNPC = npcList[npcList.Count - 2];
                    Vector3 lastNPCPosition = lastNPC.transform.position;


                    currentNPCPos = new Vector3(lastNPCPosition.x, lastNPCPosition.y + 1.0f, lastNPCPosition.z);
                    other.gameObject.transform.position = currentNPCPos;


                    other.gameObject.GetComponent<NPCStack>().UpdateNpcPosition(lastNPC.transform, true);
                }


                npcListIndexCounter++;
            }
            else
            {
                Debug.LogWarning("Capacidade alcançada");
            }
        }
    }

    public float SellNpcs()
    {
        value = 0;
        foreach (var npc in npcList)
        {
            value += npcValue;
            Destroy(npc.transform.parent.gameObject);
        }
        npcList.Clear();
        return value;
    }

    public void AddNpcCapacity(int capacity)
    {
        NpcCapacity += capacity;
        if(UpdatedStackCapacity != null)
        {
            UpdatedStackCapacity(NpcCapacity);
        }
    }
}
