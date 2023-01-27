using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    [SerializeField] Collider2D searchCol;
    [SerializeField] ContactFilter2D filter;

    void Update()
    {
        List<Collider2D> results = new List<Collider2D>();
        Physics2D.OverlapCollider(searchCol, filter, results);



        Transform target = null;
        float dist = float.MaxValue;
        for (int i = 0; i < results.Count; i++)
        {
            float calc = Vector3.Distance(transform.position, results[i].transform.position);
            if(calc < dist)
            {
                dist = calc;
                target = results[i].transform;
            }
        }
        if (target == null) return;


        var vec = target.position - transform.position;
        var deg = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, deg + 90f);
    }
}
