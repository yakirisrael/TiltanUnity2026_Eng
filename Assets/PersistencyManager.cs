using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Windows;
using File = System.IO.File;

public class PersistencyManager : MonoBehaviour
{
    public static PersistencyManager Instance;

    private int totalSecondsPlayed = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }

      //  PlayerPrefs.DeleteKey("TotalSecondsPlayed");
        totalSecondsPlayed = PlayerPrefs.GetInt("TotalSecondsPlayed", 0);
        StartCoroutine(EachSecond());
    }
    

    IEnumerator EachSecond()
    {
        yield return new WaitForSeconds(1);
        totalSecondsPlayed++;
        PlayerPrefs.SetInt("TotalSecondsPlayed", totalSecondsPlayed);
        Debug.Log(totalSecondsPlayed);
        StartCoroutine(EachSecond());
    }

    public void SavePlayerData(PlayerMovement playerMovement)
    {
        string playerJsonData = JsonUtility.ToJson(playerMovement);
        Debug.Log(Application.persistentDataPath);
        File.WriteAllText(Application.persistentDataPath + "/playerData.json", playerJsonData);
    }
    
    public PlayerMovement LoadPlayerData()
    {
        string playerJsonData = File.ReadAllText(Application.persistentDataPath + "/playerData.json");
        return JsonUtility.FromJson<PlayerMovement>(playerJsonData);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
