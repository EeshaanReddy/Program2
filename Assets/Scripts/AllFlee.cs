using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllFlee : MonoBehaviour
{
    public static AllFlee allFlee;

    public List<GameObject> allBandits;

    private void Awake()
    {
        allFlee = this;
    }

    public void flee()
    {
        foreach (GameObject bandit in allBandits)
        {
            FSMTransitions trans = bandit.GetComponent<FSMTransitions>();
            trans.TransitionToState(trans.EnemyFlee);
        }
    }
}
