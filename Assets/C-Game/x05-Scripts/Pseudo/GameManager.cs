using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // i think we can later make advanced respawn system like last time you died is gonna be shown...?
    [Tooltip("place for respawn on death")] public Transform safeSpot;

    [Tooltip("put levels in order")] public List<GameObject> stages = new List<GameObject>();

    private GameObject currentLevel;

    private bool isAlive;
    private bool isRespawning;

    public GameObject player;
    
    public PlayerState currentState;
    public static GameManager Instance;

    public GameObject isDeadUI;
    public ParticleSystem deathParticles;

    private StickToSurface stickToSurface;   

    public bool IsAlive { get => isAlive; }
    public enum PlayerState
    {
        DEAD,
        ALIVE,
        RESPAWN,
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        isAlive = true;
        currentState = PlayerState.ALIVE;
        stickToSurface = player.GetComponent<StickToSurface>();
        currentLevel = stages?[0];
        InitiateStages();
    }

    public void Dead()
    {
        currentState = PlayerState.DEAD;
        isAlive = false;
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

    private void InitiateStages()
    {
        SetActive(stages, false);
        currentLevel.SetActive(true);
        stickToSurface.initWin = false;
    }

    private void NextStage(int index)
    {
        currentLevel = stages[index + 1];
        SetActive(stages, false);
        currentLevel.SetActive(true);
        stickToSurface.initWin = false;
    }

    IEnumerator IENextStage()
    {
         
        yield return new  WaitForSeconds(0.25F);
    }

    private void InitWin()
    {
        int currentIndex = stages.IndexOf(currentLevel);

        if (currentIndex >= stages.Count - 1)
        {
            currentLevel = stages[0];
            InitiateStages();
            return;
        }
        NextStage(currentIndex);
        return;
    
        // show ui 
        // change level?
        // after pressed button boolean == false;
    }

    private void SetActive(List <GameObject> objects, bool state = false)
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(state);
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R) && currentState == PlayerState.DEAD)
        {
            currentState = PlayerState.RESPAWN;
        }

        if (stickToSurface.initWin)
        {
            InitWin();
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