using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWalls : MonoBehaviour
{
    SpriteRenderer groudSprite;
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            // groudSprite.color = 70;
            this.gameObject.SetActive(false);
        }
    }
}
