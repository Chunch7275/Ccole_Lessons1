using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearAgent : MonoBehaviour
{

    private Bot bot;

    private bool hiveDropped = false;

    private Vector3 HivePosition;
    // Start is called before the first frame update
    void Start()
    {
        bot = GetComponent<Bot>();

        NavPlayerMovement.DroppedHive += OnHiveDrop;

    }

    // Update is called once per frame
    void Update()
    {
        bool canSeeTarget = bot.CanSeeTarget();

        if (!canSeeTarget)
        {
            bot.Wander();
        } else if (!hiveDropped) 
        {
            bot.Pursue();
        } else if (hiveDropped) 
        {
            bot.Seek(HivePosition);
        }
    }
    void OnHiveDrop(Vector3 HivePosition)
    {
    hiveDropped = true;
    }
}
