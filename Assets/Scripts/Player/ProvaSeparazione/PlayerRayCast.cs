using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Null : MonoBehaviour
{
    Camera _mainCamera;
    void Start()
    {
        _mainCamera = Camera.main;
    }
    private RaycastHit[] RaycastNonAlloc(int maxCollisions = 10)
    {
        RaycastHit[] hits = new RaycastHit[maxCollisions];
        Physics.RaycastNonAlloc(_mainCamera.transform.position, _mainCamera.transform.forward, hits, 1000, LayerMask.GetMask("Default"));

        foreach (var hit in hits)
        {
            // if(hit.collider) Debug.Log("NON ALLOC: " + hit.collider.gameObject.name);
        }

        return hits;
    }
    private RaycastHit[] RaycastAlloc()
    {
        RaycastHit[] hits = Physics.RaycastAll(_mainCamera.transform.position, _mainCamera.transform.forward, 1000, LayerMask.GetMask("Default"));

        // foreach(var hit in hits)
        // {
        //     if(hit.collider)
        //     {
        //         if(hit.collider.gameObject.TryGetComponent(out IDamageable damageable))
        //         {
        //             damageable.TakeDamage(1);
        //         }

        //         Debug.Log("ALLOC: " + hit.collider.gameObject.name);
        //     }
        // }
        return hits;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 1000);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Vector3.down * 1f);
    }
}
