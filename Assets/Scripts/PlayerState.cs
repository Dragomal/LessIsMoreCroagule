using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;

public class PlayerState : MonoBehaviour
{
    public Rigidbody2D rb2D;
    public Transform player;
    public AudioSource soundSource, slurpSource;
    public AudioClip soundClip, slurpClip;
    public SpriteRenderer playerSprtieRenderer;
    public Sprite eatingSprite, idleSprite;
    public Light2D innerLight;
    private bool inAir;
    private IEnumerator gravityWallFall, gravityCeilingFall;
    public void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("FinishLine")){
            ChangeScene.Instance.GoToGame();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision){
        if(collision.contacts[0].collider.CompareTag("InvisibleWall")) return;

        gravityWallFall = GravityWallFall();
        gravityCeilingFall = GravityCeilingFall(); 
        Vector3 normal = collision.contacts[0].normal;
        soundSource.pitch = Random.Range(0.85f, 1f);

        inAir = false;
        rb2D.gravityScale = 0;
        rb2D.velocity = Vector2.zero;
        rb2D.angularVelocity = 0;
        
        if(collision.contacts[0].collider.CompareTag("Wall")){
            StartCoroutine(gravityWallFall);
        }
        else if(collision.contacts[0].collider.CompareTag("Ceiling")){
            StartCoroutine(gravityCeilingFall);
        }

        soundSource.PlayOneShot(soundClip);
        transform.DOScaleY(0.5f, 0.15f).OnComplete(()=>transform.DOScaleY(1f, 0.3f));
        player.rotation = Quaternion.LookRotation(Vector3.forward, normal);
        PlayerMovements playerMovements = player.GetComponent<PlayerMovements>();
        playerMovements.CanJump = true;
    }
    void Update(){
        if(inAir) rb2D.gravityScale = 1;
    }
    public void OnCollisionExit2D(Collision2D other){
        inAir = true;
    }
    public void Jumping(){
        StopCoroutine(gravityWallFall);
        StopCoroutine(gravityCeilingFall);
    }
    public void EatFirefly(){
        slurpSource.PlayOneShot(slurpClip);
        StartCoroutine(EatAnim());
    }
    IEnumerator GravityWallFall(){
        yield return new WaitForSeconds(1f);
        rb2D.gravityScale = 0.01f;
        yield return new WaitForSeconds(2f);
        rb2D.gravityScale = 0.1f;
        yield return new WaitForSeconds(3f);
        rb2D.gravityScale = 1;
    }
    IEnumerator GravityCeilingFall(){
        yield return new WaitForSeconds(1f);
        rb2D.gravityScale = 0.005f;
        yield return new WaitForSeconds(3f);
        rb2D.gravityScale = 1f;
    }
    IEnumerator EatAnim(){
        playerSprtieRenderer.sprite = eatingSprite;
        yield return new WaitForSeconds(0.5f);
        playerSprtieRenderer.sprite = idleSprite;
        yield return new WaitForSeconds(0.5f);
        innerLight.intensity = 1.5f;
        yield return new WaitForSeconds(0.25f);
        innerLight.intensity = 0f;
        yield return new WaitForSeconds(1f);
        innerLight.intensity = 1.5f;
        yield return new WaitForSeconds(0.25f);
        innerLight.intensity = 0f;
    }
}
