using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HighScoreUIManager : MonoBehaviour
{
    public TMP_Text firstScore;
    public TMP_Text secondScore;
    public TMP_Text thirdScore;

    // Start is called before the first frame update
    void Start()
    {
        LoadAllBestHighScore();
    }

    public void DropDownChanged(TMP_Dropdown drop)
    {
       
        if (0 == drop.value)
        {
            LoadAllBestHighScore();
        }
        else if (1 == drop.value)
        {
            LoadAllCatHighScore();
        }
        else if (2 == drop.value)
        {
            LoadAllPenguinHighScore();
        }
        else if (3 == drop.value)
        {
            LoadAllChickenHighScore();
        }
    }

    // ABSTRACTION
    public void LoadAllBestHighScore()
    {

        SetLine(firstScore, MainManager.Instance.AllBest[0]);

        SetLine(secondScore, MainManager.Instance.AllBest[1]);

        SetLine(thirdScore, MainManager.Instance.AllBest[2]);
    }

    // ABSTRACTION
    public void LoadAllCatHighScore()
    {
        SetLine(firstScore, MainManager.Instance.AllCat[0]);

        SetLine(secondScore, MainManager.Instance.AllCat[1]);

        SetLine(thirdScore, MainManager.Instance.AllCat[2]);
    }

    // ABSTRACTION
    public void LoadAllPenguinHighScore()
    {
        SetLine(firstScore, MainManager.Instance.AllPenguin[0]);

        SetLine(secondScore, MainManager.Instance.AllPenguin[1]);

        SetLine(thirdScore, MainManager.Instance.AllPenguin[2]);
    }

    // ABSTRACTION
    public void LoadAllChickenHighScore()
    {
        SetLine(firstScore, MainManager.Instance.AllChicken[0]);

        SetLine(secondScore, MainManager.Instance.AllChicken[1]);

        SetLine(thirdScore, MainManager.Instance.AllChicken[2]);
    }


    private void SetLine(TMP_Text line, MainManager.HighScore highScore)
    {
        string spaces = "                 ";
        int addSpaces = 10 - highScore.nameOfPlayer.Length;
        if (addSpaces < 1)
        {
            addSpaces = 0;
        }
        if (MainManager.AnimalType.CAT.Equals(highScore.typeOfAnimal))
        {
            addSpaces += 6;
        }
        for (int i = 0; i < addSpaces; i++)
        {
            spaces += " ";
        }

        float minutes = Mathf.Floor(highScore.PassedTime / 60);
        float seconds = Mathf.Floor(highScore.PassedTime % 60);
        line.SetText($"{highScore.nameOfPlayer}/{highScore.typeOfAnimal}{spaces}{minutes}:{seconds}");
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(1);
    }

}
