using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSight : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(SightCheck());
    }

    public IEnumerator SightCheck()
    {
        while (true)
        {
            if (SenseManager.manager.CanSee(gameObject))
            {
                Debug.Log("I see");
            }
            else
            {
                Debug.Log("I not see");
            }

            yield return new WaitForSeconds(7);
        }
    }
}
