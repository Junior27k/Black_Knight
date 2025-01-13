using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(CompositeCollider2D collision){
        if(collision.gameObject.tag == "Player")
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            player.life++;
        }
    }
}
