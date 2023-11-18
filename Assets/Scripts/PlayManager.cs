using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayManager : MonoBehaviour
{

    private float timePassed;
    public bool gameRunning = false;
    public TMP_Text timerField;
    public TMP_Text nameField;
    public TMP_Text scoreTimeField;
    public GameObject controlTextField;
    public GameObject finishedGameText;
    public GameObject didHighScoreText;
    private Vector3 spawnPosition = new Vector3(0, 0.09F, 0F);
    private Animal playerAnimal;
    public GameObject catPrefab;
    public GameObject penguinPrefab;
    public GameObject chickenPrefab;
    public GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        timePassed = 0;
        gameRunning = false;
        nameField.text = MainManager.Instance.Playername;
        spawnChoosenAnimal();

    }

    private void spawnChoosenAnimal()
    {
        GameObject animalPrefab = catPrefab;
        if (MainManager.AnimalType.PENGUIN.Equals(MainManager.Instance.ChoosenAnimal))
        {
            animalPrefab = penguinPrefab;
        }
        else if(MainManager.AnimalType.CHICKEN.Equals(MainManager.Instance.ChoosenAnimal))
        {
            animalPrefab = chickenPrefab;
        }

        GameObject animObj = Instantiate(animalPrefab, spawnPosition, new Quaternion());

        playerAnimal = animObj.GetComponent<Animal>();

        mainCamera.GetComponent<FollowPlayer>().player = animObj;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAnimal.finishGame)
        {
            playerAnimal.finishGame = false;
            GameFinished();

        }

        if (!gameRunning && Input.GetKeyDown(KeyCode.S))
        {
            StartGame();
        }

        if (!gameRunning && Input.GetKeyDown(KeyCode.M)) {
            SceneManager.LoadScene(1);
        }


        TimerUpdate(timerField);

    }


    // ABSTRACTION
    private void StartGame()
    {

        controlTextField.SetActive(false);
        finishedGameText.SetActive(false);
        didHighScoreText.SetActive(false);
        timePassed = 0;
        gameRunning = true;
        playerAnimal.gameObject.transform.position = spawnPosition;
        playerAnimal.gameRunning = true;
    }


    // ABSTRACTION
    private void TimerUpdate(TMP_Text ttField)
    {
        if (gameRunning) {
            timePassed += Time.deltaTime;
        }
        float minutes = Mathf.Floor(timePassed / 60);
        float seconds = Mathf.Floor(timePassed % 60);
        if (seconds < 10)
        {
            ttField.text = $" {minutes}:0{seconds}";
        }
        else
        {

            ttField.text = $" {minutes}:{seconds}";
        }
    }

    private void GameFinished()
    {
        gameRunning = false;
        finishedGameText.SetActive(true);
        playerAnimal.gameRunning = false;
        TimerUpdate(scoreTimeField);
        bool newHigh = MainManager.Instance.CheckForNewHighScore(timePassed);
        if (newHigh)
        {
            didHighScoreText.SetActive(true);
        }
    }
}
