using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public bool IsNewGame = true;
    public int WhumpaFruitCount = 0;
    public int extraLives = 0;
    public Text WhumpaCountText;
    public Text ExtraLivesText;
    public static GameController control;
    [HideInInspector]
    public GameObject Player;
    private Canvas MainHud;

    public void Awake()
    {
        Time.timeScale = 1;

        DontDestroyOnLoad(this);

        if (control == null)
        {
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
    }

    private void Reference()
    {
        Player = GameObject.FindWithTag("Player");
        MainHud = GameObject.Find("MainHud").GetComponent<Canvas>();

        WhumpaCountText = GameObject.Find("Text_WhumpaFruitCount").GetComponent<Text>();
        if (WhumpaFruitCount < 10)
            WhumpaCountText.text = "0" + WhumpaFruitCount;
        else WhumpaCountText.text = "" + WhumpaFruitCount;

        ExtraLivesText = GameObject.Find("Text_ExtraLivesCount").GetComponent<Text>();
        if (extraLives < 10)
            ExtraLivesText.text = "0" + extraLives;
        else ExtraLivesText.text = "" + extraLives;
    }

    private void Start()
    {
        Reference();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Active");
        Reference();
        if (!IsNewGame) Load();
    }

    public void GameOver()
    {
        MainHud.GetComponent<ShowHidePanel>().OnDeath();
        Time.timeScale = 0;
    }

    public void AddWhumpaFruids(int amount)
    {
        WhumpaFruitCount += amount;
        if (WhumpaFruitCount > 99)
        {
            WhumpaFruitCount = 0; AddExtraLives(1);
        }
        if (WhumpaFruitCount < 10)
            WhumpaCountText.text = "0" + WhumpaFruitCount;
        else WhumpaCountText.text = "" + WhumpaFruitCount;
    }

    public void AddExtraLives(int amount)
    {
        extraLives += amount;

        if (extraLives < 10)
            ExtraLivesText.text = "0" + extraLives;
        else ExtraLivesText.text = "" + extraLives;

        ExtraLivesText.transform.parent.GetComponent<ShowHidePanel>().Show();
        StartCoroutine(ExtraLivesText.transform.parent.GetComponent<ShowHidePanel>().PlayAnimAfterDelay(false, 3f));

        IsNewGame = false;
        if (amount < 0) LoadLastSavedScene();
    }

    public void Save(string AutoSave)
    {
        SaveGame save = new SaveGame();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.123");
        GameObject player = GameObject.FindWithTag("Player");

        save.playerPositionX = player.transform.position.x;
        save.playerPositionY = player.transform.position.y;
        save.playerPositionZ = player.transform.position.z;

        save.playerRotationX = player.transform.rotation.x;
        save.playerRotationY = player.transform.rotation.y;
        save.playerRotationZ = player.transform.rotation.z;

        save.lastPlayedScene = SceneManager.GetActiveScene().buildIndex;
        save.lastUsedAutoSave = AutoSave;

        save.WhumpaFruitCount = WhumpaFruitCount;
        save.extraLives = extraLives;

        bf.Serialize(file, save);
        file.Close();
    }
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.123"))
        {
            SaveGame save = new SaveGame();
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.123", FileMode.Open);
            save = (SaveGame)bf.Deserialize(file);
            GameObject player = GameObject.FindWithTag("Player");

            if (SceneManager.GetActiveScene().buildIndex == save.lastPlayedScene)
            {
                player.transform.position = new Vector3(save.playerPositionX, save.playerPositionY, save.playerPositionZ);
                player.transform.rotation = new Quaternion(save.playerRotationX, save.playerRotationY, save.playerRotationZ, 0);
            }

            WhumpaFruitCount = save.WhumpaFruitCount;
            extraLives = save.extraLives;

            file.Close();
        }
    }

    public void LoadLastSavedScene()
    {
        SaveGame save = new SaveGame();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/gamesave.123", FileMode.Open);
        save = (SaveGame)bf.Deserialize(file);

        SceneManager.LoadScene(save.lastPlayedScene);

        file.Close();
    }

    public bool CanLoadGame()
    {
        return true;
    }

    public int GetLastPlayedScene()
    {
        SaveGame save = new SaveGame();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/gamesave.123", FileMode.Open);
        save = (SaveGame)bf.Deserialize(file);

        int temp = save.lastPlayedScene;

        file.Close();

        return temp;
    }

    public string GetLastAutoSaveName()
    {
        SaveGame save = new SaveGame();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/gamesave.123", FileMode.Open);
        save = (SaveGame)bf.Deserialize(file);

        string temp = save.lastUsedAutoSave;

        file.Close();

        return temp;
    }
}




[System.Serializable]
public class SaveGame
{
    public int WhumpaFruitCount;
    public int extraLives;

    public int lastPlayedScene;
    public string lastUsedAutoSave;

    public float playerPositionX;
    public float playerPositionY;
    public float playerPositionZ;

    public float playerRotationX;
    public float playerRotationY;
    public float playerRotationZ;

    public SaveGame()
    {

    }
}