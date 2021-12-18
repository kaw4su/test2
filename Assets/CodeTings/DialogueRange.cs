using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueRange : MonoBehaviour {
    //DialogueManager dm = GameObject.AddComponent<DialogueManager>();

    public static bool talkRange;

    private void Awake(){
        talkRange = false;
    }

    private void OnTriggerEnter2D(Collider2D collider){
       if (collider.gameObject.tag == "Player"){
           talkRange = true;
       }
   }

   private void OnTriggerExit2D(Collider2D collider){
       if (collider.gameObject.tag == "Player"){
           talkRange = false;
           //dm.ExitDialogueMode();
       }
   }
}
