using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeSounds : MonoBehaviour
{
    public AudioSource harmonica;

    // Start is called before the first frame update
    void Start()
    {
        harmonica = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown (KeyCode.H))
        {
            harmonica.Play ();
        }
    }
}
