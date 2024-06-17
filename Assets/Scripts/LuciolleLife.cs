using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuciolleLife : MonoBehaviour
{
    public float range = 1;
    public float posTime = 1;
    float xPos;
    float yPos;
    Transform player;
    void Awake(){
        xPos = transform.position.x;
        yPos = transform.position.y;
    }
    void Update(){
        transform.localPosition = new Vector3(xPos, yPos + range * Mathf.Sin(Time.time * Mathf.PI), 0);
    }

    
    public void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            UiManager playerUi = other.GetComponent<UiManager>();
            playerUi.EatFirefly();
            PlayerState playerState = other.GetComponent<PlayerState>();
            playerState.EatFirefly();
            Destroy(this.gameObject);
        }
    }
}
