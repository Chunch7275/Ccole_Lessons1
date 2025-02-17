using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] lilyPads;

    void Start()
    {
        InvokeRepeating("SpawnLilyPad", 2.0f, 5.0f);
    }

    // Update is called once per frame

    void SpawnLilyPad()
    {
        foreach (GameObject lilyPad in lilyPads)
        {
            Instantiate(lilyPad);
        }
    }
    
    void Update()
    {
        
    }
}
