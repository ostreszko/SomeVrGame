using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public static GameMaster GM;

    public Image viewFinder;
    public PlayerController playerController;
    public GameObject playerObject;
    public int activeEnemies = 0;
    public int killedEnemies = 0;
    [System.NonSerialized]
    public float healingCrossFloating = 0.015f;
    public CanvasGroup guiCanvasGroup;
    [System.NonSerialized]
    public bool gameEnded;
    [SerializeField] private int _amount;
    [SerializeField] private List<Image> _images;
    private HeartContainer _heartContainer;
    [System.NonSerialized]
    public Score _score;

     Image viewFinderStart;
     PlayerController playerControllerStart;
     GameObject playerObjectStart;
     int activeEnemiesStart;
     int killedEnemiesStart;
     float healingCrossFloatingStart;
     CanvasGroup guiCanvasGroupStart;
     bool gameEndedStart;
     int _amountStart;
     List<Image> _imagesStart;
     HeartContainer _heartContainerStart;
     Score _scoreStart;

    private void OnEnable()
    {
        viewFinderStart = viewFinder;
        playerControllerStart = playerController;
        playerObjectStart = playerObject;
        activeEnemiesStart = activeEnemies;
        killedEnemiesStart = killedEnemies;
        healingCrossFloatingStart = healingCrossFloating;
        guiCanvasGroupStart = guiCanvasGroup;
        gameEndedStart = gameEnded;
        _amountStart = _amount;
        _imagesStart = _images;
        _heartContainerStart = _heartContainer;
        _scoreStart = _score;
    }

    void Awake()
    {
        if (GM != null)
        {
            GameObject.Destroy(GM);
        }
        else
        {
            GM = this;
        }
    }
    private void Start()
    {
        _heartContainer = new HeartContainer(_images.Select(image => new Heart(image)).ToList());
        _score = playerController.scoreObj;
        playerController.Healed += (sender, args) => _heartContainer.Replenish(args.Amount);
        playerController.Damaged += (sender, args) => _heartContainer.Deplate(args.Amount);
        playerController.Scored += (sender, args) => _score.AddScore(args.Amount);
    }

    public void ResetGameMaster() 
    {
        viewFinder = viewFinderStart;
        playerController = playerControllerStart;
        playerObject = playerObjectStart;
        activeEnemies = activeEnemiesStart;
        killedEnemies = killedEnemiesStart;
        healingCrossFloating = healingCrossFloatingStart;
        guiCanvasGroup = guiCanvasGroupStart;
        gameEnded = gameEndedStart;
        _amount = _amountStart;
        _images = _imagesStart;
        _heartContainer = _heartContainerStart;
        _score = _scoreStart;
    }
}
