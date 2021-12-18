using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour{
    public static bool playerInAttack;
    public static bool hittable;

    private void Awake(){
        playerInAttack = false;
        hittable = true;
    }

    private void OnTriggerEnter2D(Collider2D collider){
       if (collider.gameObject.tag == "Player"){
           playerInAttack = true;
           
       }
    }

    private void OnTriggerExit2D(Collider2D collider){
       if (collider.gameObject.tag == "Player"){
           playerInAttack = false;
           hittable = true;
       }
    }


  
}
