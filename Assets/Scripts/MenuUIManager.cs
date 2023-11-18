using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIManager : MonoBehaviour
{
    public RawImage catImg;
    public RawImage penguinImg;
    public RawImage chickenImg;

    private static readonly Color GreenColor = new(0.3f, 0.8f, 0.35f, 1f);


    // Start is called before the first frame update
    void Start()
    {
        SelectCat();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void SelectCat()
    {
        UnSelectAllAnimals();
        MainManager.Instance.ChooseAnimal(MainManager.AnimalType.CAT);
        catImg.color = GreenColor;
    }


    public void SelectPenguin()
    {
        UnSelectAllAnimals();
        MainManager.Instance.ChooseAnimal(MainManager.AnimalType.PENGUIN);
        penguinImg.color = GreenColor;
    }


    public void SelectChicken()
    {
        UnSelectAllAnimals();
        MainManager.Instance.ChooseAnimal(MainManager.AnimalType.CHICKEN);
        chickenImg.color = GreenColor;
    }

    private void UnSelectAllAnimals()
    {
        catImg.color = Color.white;
        penguinImg.color = Color.white;
        chickenImg.color = Color.white;
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadHighScore()
    {
        SceneManager.LoadScene(2);
    }

    public void Exit()
    {
        #if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
        #else
                        Application.Quit(); // original code to quit Unity player
        #endif
    }

}
