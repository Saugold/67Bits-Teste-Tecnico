using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStack : MonoBehaviour
{
    [SerializeField] private float followSpeed;

    public void UpdateNpcPosition(Transform followedNpc, bool isFollowStart)
    {
        StartCoroutine(StartFollowingLastNpcPosition(followedNpc, isFollowStart));
    }

    IEnumerator StartFollowingLastNpcPosition(Transform followedNpc, bool isFollowStart)
    {

        while (isFollowStart)
        {
            yield return new WaitForEndOfFrame();
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, followedNpc.position.x, followSpeed * Time.deltaTime),
                transform.position.y,
                Mathf.Lerp(transform.position.z, followedNpc.position.z, followSpeed * Time.deltaTime));
        }
    }
}
