using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeeperAttack : MonoBehaviour
{

    //PlayerController player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().life--;
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            player = collider.GetComponent<PlayerController>();
        }
    }

    public void damage(){
        player.life--;
    }*/

}
