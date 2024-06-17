using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerMovements : MonoBehaviour
{
    public bool CanJump{
        get{return canJump;}
        set{canJump = value;}
    }
    bool canJump;
    public bool PrepareJump{
        get{return prepareJump;}
        set{prepareJump = value;}
    }
    bool prepareJump;

    public Rigidbody2D rb2D;
    public GameObject jumpDirection;
    public Transform player;
    public Transform pointDirection;
    public AudioSource croaSource, soundSource;
    public AudioClip croaClip, soundClip;
    public float puissanceSaut = 2;
    public float tickSpeed = 1;
    public float angleJump;
    public PlayerState playerState;
    private float timeCroa = 3f;
    private int rotateArrow = 1;
    private float _animationFactor = 0;

    void Start(){
        DOTween.Init();
    }
    public void PrepareJumping(InputAction.CallbackContext callback){
        if(!CanJump) return;
        if(callback.started){
            PrepareJump = true;
            _animationFactor = 0;
            rotateArrow = 1;
            jumpDirection.SetActive(true);
        }
        else if(callback.canceled && PrepareJump){
            PrepareJump = false;
            CanJump = false;
            jumpDirection.SetActive(false);
            soundSource.pitch = Random.Range(1f, 1.10f);
            soundSource.PlayOneShot(soundClip);
            playerState.Jumping();
            // rb2D.gravityScale = 1;

            transform.DOScaleY(0.5f, 0.1f).OnComplete(()=>transform.DOScaleY(1f, 0.1f));
            Vector3 direction = pointDirection.position - player.position;
            rb2D.AddForce(direction * puissanceSaut, ForceMode2D.Impulse);
            rb2D.AddTorque(_animationFactor * 50);
        }
    }
    void FixedUpdate(){
        timeCroa -= Time.fixedDeltaTime;
        if(timeCroa <= 0){
            croaSource.pitch = Random.Range(0.90f, 1.05f);
            croaSource.PlayOneShot(croaClip);
            timeCroa = Random.Range(6f, 9f);
        }

        if(PrepareJump && CanJump){
            float playerAngle = player.rotation.eulerAngles.z;
            _animationFactor += Time.deltaTime * tickSpeed * rotateArrow;

            if(_animationFactor > 1 || _animationFactor < -1){
                _animationFactor = Mathf.Clamp(_animationFactor, -1, 1);
                rotateArrow *= -1;
            }
            
            jumpDirection.transform.rotation = Quaternion.Euler(0,0, playerAngle + _animationFactor * angleJump);

        }
    }
}
