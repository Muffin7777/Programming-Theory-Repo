using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    // ENCAPSULATION
    public static MainManager Instance { get; private set; }

    public string Playername { get; private set; }

    public HighScore[] AllChicken { get; private set; }
    public HighScore[] AllCat { get; private set; }
    public HighScore[] AllPenguin { get; private set; }

    public HighScore[] AllBest { get; private set; }

    public AnimalType ChoosenAnimal{ get; private set; }


    public enum AnimalType
    {
        CHICKEN, CAT, PENGUIN
    }

    public class HighScore
    {
        public float PassedTime { get; private set; }
        public string nameOfPlayer { get; private set; }
        public AnimalType typeOfAnimal { get; private set; }

       public HighScore(float time, string playerName, AnimalType animal)
        {
            PassedTime = time;
            nameOfPlayer = playerName;
            typeOfAnimal = animal;
        }
    }


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        AllBest = new HighScore[3];
        Playername = "None";
        ChoosenAnimal = AnimalType.CHICKEN;
        LoadHighScores();
        DontDestroyOnLoad(gameObject);
    }



    [System.Serializable]
    public class SaveHighScore
    {
        public float scoredTime;
        public string playerName;
        public string typeOfAnimal;

    }


    [System.Serializable]
    class SaveData
    {
        public List<SaveHighScore> chickens;
        public List<SaveHighScore> cats;
        public List<SaveHighScore> penguins;

    }




    public void SaveHighScores()
    {
        SaveData data = new SaveData();

        data.chickens = makeSaveHighList(AllChicken);
        data.cats = makeSaveHighList(AllCat); 
        data.penguins = makeSaveHighList(AllPenguin); 

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public List<SaveHighScore> makeSaveHighList(HighScore[] scores)
    {
       List<SaveHighScore> scoreRay = new(3);

        for (int i = 0; i < 3; i++)
        {

            HighScore high = scores[i];
            SaveHighScore sav = new();
            sav.scoredTime = high.PassedTime;
            sav.playerName = high.nameOfPlayer;
            sav.typeOfAnimal = getAnimalString(high.typeOfAnimal);
            scoreRay.Insert(i, sav);

        }

        return scoreRay;
    }

    public void LoadHighScores()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);
            AllChicken = parseHighScore(data.chickens);
            AllCat = parseHighScore(data.cats);
            AllPenguin = parseHighScore(data.penguins);

        }
        if(!File.Exists(path) || AllChicken == null)
        {
            AllChicken = new HighScore[3] { EmptyHighScore(AnimalType.CHICKEN), EmptyHighScore(AnimalType.CHICKEN), EmptyHighScore(AnimalType.CHICKEN) };
            AllCat = new HighScore[3] { EmptyHighScore(AnimalType.CAT), EmptyHighScore(AnimalType.CAT), EmptyHighScore(AnimalType.CAT) };
            AllPenguin = new HighScore[3] { EmptyHighScore(AnimalType.PENGUIN), EmptyHighScore(AnimalType.PENGUIN), EmptyHighScore(AnimalType.PENGUIN) };
         }
        CalculateAllBest();
    }

    public HighScore[] parseHighScore(List<SaveHighScore> scores)
    {

        HighScore[] scoreRay = new HighScore[3];
        for (int i = 0; i < 3; i++)
        {
            SaveHighScore sav = scores[i];
            HighScore high = new(sav.scoredTime, sav.playerName, parseAnimalType(sav.typeOfAnimal));
            scoreRay[i] = high;
        }

        return scoreRay;
    }

    /*Get Type Of Animal*/
    public AnimalType parseAnimalType(string animalType)
    {
        if ("Chicken".Equals(animalType)) {
            return AnimalType.CHICKEN;
        }
        if ("Penguin".Equals(animalType))
        {
            return AnimalType.PENGUIN;
        }
        return AnimalType.CAT;
    }
    /*Get string of type*/
    public string getAnimalString(AnimalType animalType)
    {
        if (AnimalType.CHICKEN.Equals(animalType))
        {
            return "Chicken";
        }
        if (AnimalType.PENGUIN.Equals(animalType))
        {
            return "Penguin";
        }
        return "Cat";
    }

    private HighScore EmptyHighScore(AnimalType type)
    {
        return new HighScore(86400, "none", type);
    }

    public void ChooseAnimal(AnimalType animal)
    {
        ChoosenAnimal = animal;
    }


    private void CalculateAllBest()
    {
        int catPlace = 0;
        int pengPlace = 0;
        int chickPlace = 0;
        HighScore first = BestOfThree(AllCat[catPlace], AllPenguin[pengPlace], AllChicken[chickPlace]);
        if (AnimalType.CAT.Equals(first.typeOfAnimal))
        {
            catPlace++;
        }
        else if (AnimalType.PENGUIN.Equals(first.typeOfAnimal))
        {
            pengPlace++;
        }
        else if (AnimalType.CHICKEN.Equals(first.typeOfAnimal))
        {
            chickPlace++;
        }
        HighScore second = BestOfThree(AllCat[catPlace], AllPenguin[pengPlace], AllChicken[chickPlace]);
        if (AnimalType.CAT.Equals(second.typeOfAnimal))
        {
            catPlace++;
        }
        else if (AnimalType.PENGUIN.Equals(second.typeOfAnimal))
        {
            pengPlace++;
        }
        else if (AnimalType.CHICKEN.Equals(second.typeOfAnimal))
        {
            chickPlace++;
        }
        HighScore third = BestOfThree(AllCat[catPlace], AllPenguin[pengPlace], AllChicken[chickPlace]);

        AllBest[0] = first;
        AllBest[1] = second;
        AllBest[2] = third;

    }


    public bool CheckForNewHighScore(float timeScored)
    {
        int pos = -1;

        if (AnimalType.CAT.Equals(ChoosenAnimal))
        {
            pos = GetPosition(AllCat, timeScored);
        }
        else if (AnimalType.PENGUIN.Equals(ChoosenAnimal))
        {
            pos = GetPosition(AllPenguin, timeScored);
        }
        else if (AnimalType.CHICKEN.Equals(ChoosenAnimal))
        {
            pos = GetPosition(AllChicken, timeScored);
        }

        if (pos == -1)
        {
            return false;
        }

        if (AnimalType.CAT.Equals(ChoosenAnimal))
        {
            if (pos < 2)
            {
                AllCat[2] = AllCat[1];
            }
            if (pos < 1)
            {
                AllCat[1] = AllCat[0];
            }
            AllCat[pos] = new HighScore(timeScored, Playername, ChoosenAnimal);
        }
        else if (AnimalType.PENGUIN.Equals(ChoosenAnimal))
        {

            if (pos < 2)
            {
                AllPenguin[2] = AllPenguin[1];
            }
            if (pos < 1)
            {
                AllPenguin[1] = AllPenguin[0];
            }
            AllPenguin[pos] = new HighScore(timeScored, Playername, ChoosenAnimal);
        }
        else if (AnimalType.CHICKEN.Equals(ChoosenAnimal))
        {
            if (pos < 2)
            {
                AllChicken[2] = AllChicken[1];
            }
            if (pos < 1)
            {
                AllChicken[1] = AllChicken[0];
            }
            AllChicken[pos] = new HighScore(timeScored, Playername, ChoosenAnimal);
        }

        CalculateAllBest();
        SaveHighScores();
        return true;
    }


    private int GetPosition(HighScore[] highScores, float timeNow)
    {
        if (timeNow < highScores[0].PassedTime)
        {
           return 0;
        }else if (timeNow < highScores[1].PassedTime)
        {
           return 1;
        } else if (timeNow < highScores[2].PassedTime)
        {
            return 2;
        }

        return -1;
    }

    private HighScore BestOfThree(HighScore one, HighScore two, HighScore three)
    {
        HighScore best = one;
        if (one.PassedTime > two.PassedTime)
        {
            best = two;
        }
        if (two.PassedTime > three.PassedTime)
        {
            best = three;
        }
        return best;
    }

    public void setPlayerName(string value)
    {
        Playername = value;
    }

}
