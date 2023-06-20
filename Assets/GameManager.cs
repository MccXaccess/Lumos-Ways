using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // i think we can later make advanced respawn system like last time you died is gonna be shown...?
    [Tooltip("place for respawn on death")] public Transform safeSpot;

    private bool isAlive;
    private bool isRespawning;

    public GameObject player;
    
    public PlayerState currentState;
    public static GameManager Instance;

    public GameObject isDeadUI;
    public ParticleSystem deathParticles;

    

    public bool IsAlive { get => isAlive; }
    public enum PlayerState
    {
        DEAD,
        ALIVE,
        RESPAWN,
    }
    
    private void Start()
    {
        Instance = this;
        isAlive = true;
        currentState = PlayerState.ALIVE;
    }

    public void Dead()
    {
        currentState = PlayerState.DEAD;
        Debug.Log(currentState);
        isAlive = false;
        Debug.Log("you're dead");
        Hide(player);
        Show(isDeadUI);

        if (deathParticles != null)
        {
            deathParticles.gameObject.transform.position = player.transform.position;
            deathParticles.Play();
        }
        else
        {
            Debug.LogError("particles system is null/");
        }
        // maybe even particles would be great/
        // maybe even camera slowly goes to somwhere else or what?
        // here we can show cooldown of UI elements and stuffs like that?
        // we can create scripts for handling the global volume when dead too here.

    }

    public void Respawn()
    {
        currentState = PlayerState.RESPAWN;
        // wait and stuff like that...
        currentState = PlayerState.ALIVE;
        isAlive = true;
        Show(player);
        Hide(isDeadUI);
        
        player.transform.position = safeSpot.position;
    }

    private void Hide(GameObject value)
    {
        value.SetActive(false);
    }

    private void Show(GameObject value)
    {
        value.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            currentState = PlayerState.RESPAWN;
        }

        switch (currentState)
        {
            case PlayerState.ALIVE:
                return;

            case PlayerState.RESPAWN:
                Respawn();
                break;

            // case PlayerState.DEAD:
            //     Dead();
            //     break;
        }
    }
}