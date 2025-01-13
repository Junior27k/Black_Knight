using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeeperAttack : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            collider.GetComponent<PlayerController>().life--;
        }
    }

}
