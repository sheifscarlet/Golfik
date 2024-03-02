using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    private GameObject currentTeleporter;
    
    //Cooldown 
    private float cooldownTime = 0.5f;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = cooldownTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; 
        timer = Mathf.Clamp(timer, 0f, cooldownTime); 

        if (currentTeleporter != null && timer >= cooldownTime)
        {
            transform.position = currentTeleporter.GetComponent<Teleporter>().GetDestination().position;
            timer = 0f; 
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            currentTeleporter = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            if (collision.gameObject == currentTeleporter)
            {
                currentTeleporter = null;
            }
        }
    }
}
