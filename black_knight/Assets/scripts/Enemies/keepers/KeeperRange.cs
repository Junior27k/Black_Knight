using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeeperRange : MonoBehaviour
{
    
    private void OnTriggerStay2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            GetComponentInParent<Animator>().Play("Attack", -1);

        }
    }

}
