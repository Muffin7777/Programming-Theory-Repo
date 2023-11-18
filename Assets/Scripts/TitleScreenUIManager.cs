using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TitleScreenUIManager : MonoBehaviour
{
    public TMP_InputField NameInputField;
    public GameObject NameTooLongText;
    public GameObject NoNameEnteredText;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            goToMenu();
        }
    }


    public void goToMenu()
    {
        NameTooLongText.SetActive(false);
        NoNameEnteredText.SetActive(false);
        string nameInput = NameInputField.text;
        if (nameInput.Length < 1)
        {
            NoNameEnteredText.SetActive(true);
            return;
        }
        if (nameInput.Length > 10)
        {
            NameTooLongText.SetActive(true);
            return;
        }
        MainManager.Instance.setPlayerName(nameInput);
        SceneManager.LoadScene(1);
    
    }
}
