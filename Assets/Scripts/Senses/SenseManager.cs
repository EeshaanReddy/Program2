using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenseManager : MonoBehaviour
{
    public static SenseManager manager;

    [Tooltip("According to the spec this should be 30, but that feels really really small in practice. Looked it up and a regular person has about 135 deg")]
    public float coneDegrees;

    private GameObject marshall;

    public List<GameObject> bandits;

    private void Awake()
    {
        marshall = GameObject.FindWithTag("Player");
        manager = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
            MakeSound();
    }

    public void MakeSound()
    {
        foreach (GameObject bandit in bandits)
        {
            if (CanHear(bandit))
            {
                bandit.GetComponent<GuardManager>().guard();
            }
        }
    }

    public bool CanSee(GameObject viewer)
    {
        if (Vector3.Distance(marshall.transform.position, viewer.transform.position) > 30) 
            return false;
        var position = viewer.transform.position;
        Vector3 toTarget = (marshall.transform.position-position).normalized;
        float angle = Mathf.Acos((Vector3.Dot(toTarget,viewer.transform.forward.normalized)))*Mathf.Rad2Deg;
        if (angle > 180)
            angle = 360 - angle;
        if (angle < -180)
            angle = -360 - angle;
        if (angle < -(coneDegrees/2) || angle > (coneDegrees/2))
            return false;
        RaycastHit ray = new RaycastHit();
        //model origin not at eyes
        Vector3 originFugde = new Vector3(position.x, position.y + 0.25f, position.z);
        Physics.Raycast(originFugde, toTarget, out ray);
        if (ray.collider.gameObject.tag.Equals("Player")) 
            return true;
        return false;
    }

    public bool CanHear(GameObject listener)
    {
        if (Vector3.Distance(marshall.transform.position, listener.transform.position) > 10) return false;
        GameObject marshallblding = null;
        RaycastHit hit;
        Physics.Raycast(marshall.transform.position, Vector3.up, out hit);
        if(hit.collider != null)
            marshallblding = hit.collider.gameObject;
        if (marshallblding != null && Physics.Raycast(listener.transform.position, Vector3.up, out hit))
        {
            if (marshallblding.name.Equals(hit.collider.gameObject.name)) return true;
        }
        if (hit.collider == null) return true;
        if (!hit.collider.gameObject.GetComponent<Building>().open) return false;
        return true;
    }
}
