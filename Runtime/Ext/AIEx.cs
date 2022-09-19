using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class AIEx
{
    public static Vector3 ExRandomNavmeshLocation(this Transform transform, float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    public static Vector3 ExSamplePositionOnNavMesh(this Transform agent, Vector3 direction)
    {
        Vector3 sourcePosition = direction;
        sourcePosition += agent.transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = agent.position + direction.normalized * 2f;
        if (NavMesh.SamplePosition(sourcePosition, out hit, 10f, NavMesh.AllAreas))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    } 
}