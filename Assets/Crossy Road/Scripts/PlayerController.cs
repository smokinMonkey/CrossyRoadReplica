using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // variables 
    public float moveDistance = 1;
    public float moveTime = 0.4f;
    public float colliderDistCheck = 1;
    public bool isIdle = true;
    public bool isDead = false;
    public bool isMoving = false;
    public bool isJumping = false;
    public bool jumpStart = false;
    public ParticleSystem particle = null;
    public GameObject chick = null;
    public new Renderer renderer = null;
    private bool isVisible = false;


    // Start is called before the first frame update
    void Start()
    {
        renderer = chick.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO Manager -> check if can play

        if(isDead)
        {
            return;
        } else
        {
            CanIdle();
            CanMove();
            IsVisible();
        }
        
    }

    // checks if the player can be idle
    void CanIdle()
    {
        if(isIdle)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) ||
                Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) ||
                Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) ||
                Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                CheckIfCanMove();
            }
        }
    }

    // checks if the player can move
    void CheckIfCanMove()
    {
        // raycast - find if there is any collider box in front of player
        RaycastHit hit;
        Physics.Raycast(this.transform.position, -chick.transform.up, out hit, colliderDistCheck);
        Debug.DrawRay(this.transform.position, -chick.transform.up * colliderDistCheck, Color.red, 2);
        if(hit.collider == null)
        {
            SetMove();
        } else
        {
            if(hit.collider.tag == "collider")
            {
                Debug.Log("Hit something with collider tag.");
            } else
            {
                SetMove();
            }
        }
    }

    // checks if the player can move
    void CanMove()
    {
        if (isMoving)
        {
            // for future implement switch case will be more optimized 
            // moving up with up arrow and w
            if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
            {
                Moving(new Vector3(transform.position.x, transform.position.y, transform.position.z + moveDistance));
            }
            // moving down with down arrow and s
            else if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
            {
                Moving(new Vector3(transform.position.x, transform.position.y, transform.position.z - moveDistance));
            }
            // moving right with right arrow and d
            else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
            {
                Moving(new Vector3(transform.position.x + moveDistance, transform.position.y, transform.position.z));
            }
            // moving left with left arrow and a
            else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
            {
                Moving(new Vector3(transform.position.x - moveDistance, transform.position.y, transform.position.z));
            }
        }
    }

    // method to move the player
    void Moving(Vector3 pos)
    {
        isIdle = false;
        isMoving = false;
        isJumping = true;
        jumpStart = false;

        LeanTween.move(this.gameObject, pos, moveTime).setOnComplete(MoveComplete);
    }

    // method to set the moves for player
    void SetMove()
    {
        Debug.Log("Hit nothing, keep moving!");

        isIdle = false;
        isMoving = true;
        jumpStart = true;
    }

    // method to complete move for player
    void MoveComplete()
    {
        isJumping = false;
        isIdle = true;
    }

    // set the player moving forward state
    void SetMoveForwardState()
    {

    }

    // set if the player is visible
    void IsVisible()
    {
        if(renderer.isVisible)
        {
            isVisible = true;
        }

        if(!renderer.isVisible && isVisible)
        {
            Debug.Log("Player off screen, Apple GotHit()");
            GotHit();
        }
    }

    // checks if the player got hit
    public void GotHit()
    {
        isDead = true;
        // animate dead chicken
        ParticleSystem.EmissionModule em = particle.emission;
        em.enabled = true;

        // TODO: manager -> GameOver()
    }

}