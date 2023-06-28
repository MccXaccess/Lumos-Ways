using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using EasyTransition;

public class GameManager : MonoBehaviour
{
    // i think we can later make advanced respawn system like last time you died is gonna be shown...?
    [Tooltip("place for respawn on death")] public Transform safeSpot;
    [Tooltip("place for start point")] public Transform startPos;

    [Tooltip("put levels in order")] public List<GameObject> stages = new List<GameObject>();

    [Tooltip("soundtracks")] public List<AudioSource> HMsoundtracks = new List<AudioSource>();

    private GameObject currentLevel;

    private bool isAlive;
    private bool isRespawning;

    public GameObject player;
    
    public PlayerState currentState;
    public static GameManager Instance;

    public GameObject isDeadUI;
    public ParticleSystem deathParticles;

    private StickToSurface stickToSurface;   

    public TextMeshProUGUI textMeshProCurrentStage;
    
    public DemoLoadScene demoLoadScene;

    public AudioSource whenDies;

    public bool IsAlive { get => isAlive; }
    public enum PlayerState
    {
        DEAD,
        ALIVE,
        RESPAWN,
    }

    private void Awake()
    {
        Instance = this;
        // if (Instance == null)
        // {
        //     Instance = this;
        //     DontDestroyOnLoad(this);
        // }
        // else if (Instance != this)
        // {
        //     Destroy(gameObject);
        // }
    }
    
    private void Start()
    {
        isAlive = true;
        currentState = PlayerState.ALIVE;
        stickToSurface = player.GetComponent<StickToSurface>();
        currentLevel = stages?[0];
        InitiateStages();
        player.transform.position = startPos.position;
    }

    private IEnumerator AlphaAnimationCoroutine()
    {
        // Increase alpha from 0 to 1
        float duration = 1f;
        float elapsedTime = 0f;
        Color startColor = textMeshProCurrentStage.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            Color newColor = Color.Lerp(startColor, endColor, t);
            textMeshProCurrentStage.color = newColor;
            yield return null;
        }

        // Wait for 0.5 seconds
        yield return new WaitForSeconds(0.5f);

        // Decrease alpha from 1 to 0
        duration = 1f;
        elapsedTime = 0f;
        startColor = textMeshProCurrentStage.color;
        endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            Color newColor = Color.Lerp(startColor, endColor, t);
            textMeshProCurrentStage.color = newColor;
            yield return null;
        }
    }

    public void Dead()
    {
        player.transform.parent.SetParent(null);
        stickToSurface.isSqueezed = false;
        currentState = PlayerState.DEAD;
        isAlive = false;
        whenDies.Play();
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
        HMsoundtracks[0].gameObject.SetActive(true);

        textMeshProCurrentStage.text = currentLevel.name;
        StartCoroutine(AlphaAnimationCoroutine());
    }

    private void NextStage(int index)
    {
        currentLevel = stages[index + 1];
        SetActive(stages, false);
        currentLevel.SetActive(true);
        stickToSurface.initWin = false;

        HMsoundtracks[index + 1].gameObject.SetActive(true);

        textMeshProCurrentStage.text = currentLevel.name;
        StartCoroutine(AlphaAnimationCoroutine());
        demoLoadScene.StartTransition();
        
    }

    IEnumerator IENextStage()
    {
         
        yield return new  WaitForSeconds(0.25F);
    }

    private void InitWin()
    {
        
        int currentIndex = stages.IndexOf(currentLevel);
        HMsoundtracks[currentIndex].gameObject.SetActive(false);

        if (currentIndex >= stages.Count - 1)
        {
            currentLevel = stages[0];
            InitiateStages();
            return;
        }
        NextStage(currentIndex);

        // STOP TIME AND START TRANSITION

        // RESUME TIME AND STOP TRANSITION

        // show ui 
        // transition
        // after pressed button boolean == false;

        // in case if i want to make dynamic teleport to levels...
        //transform.position = nextLvlPosition.position;
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
        if (stickToSurface.isSqueezed && currentState == PlayerState.ALIVE)
        {
            Dead();  
        }

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