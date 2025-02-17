using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rbplayer;

    private Vector3 direction = Vector3.zero;
    [SerializeField]
    private float ForceMultiplier = 10.0f;
    [SerializeField]
    private ForceMode forcemode;

    public GameObject spawnPoint;

    private Dictionary<Item.VegetableType, int> inventory = new Dictionary<Item.VegetableType, int>();
    

    // Start is called before the first frame update
    void Start()
    {
        rbplayer = GetComponent<Rigidbody>();
        
        foreach (Item.VegetableType type in System.Enum.GetValues(typeof(Item.VegetableType)))
        {
            inventory.Add(type, 0);
        }
    }

    void Update()
    {
        float HorizontalVelocity = Input.GetAxis("Horizontal");
        float VerticalVelocity = Input.GetAxis("Vertical");
        
        direction = new Vector3(HorizontalVelocity, 0, VerticalVelocity);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rbplayer.AddForce(direction * ForceMultiplier, forcemode);

        if (transform.position.z > 38)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 38);
        }
        else if (transform.position.z < -38)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -38);
        }
    }

    private void Respawn()
    {
        rbplayer.MovePosition(spawnPoint.transform.position);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Vegetable"))
        {
            Item item = collider.GetComponent<Item>();
            AddItemToInventory(item);
            PrintInventory();
        }
    }

    private void AddItemToInventory(Item item)
    {
        inventory[item.veggieType]++;
    }

    private void PrintInventory()
    {
        string output = "";

        foreach (KeyValuePair<Item.VegetableType, int> pair in inventory)
        {
            output += string.Format("{0}: {1}; ", pair.Key, pair.Value);
        }

        Debug.Log(output);
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Hazard"))
        {
            Respawn();
        }
    }
}
