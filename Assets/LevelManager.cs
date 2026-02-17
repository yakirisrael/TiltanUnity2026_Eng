using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public PlayerMovement playerMovement;
    
    public static LevelManager Instance;
    
    public List<AudioClip> levelClips;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance == null)
        {
            //first instance
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        
        PersistencyManager.Instance.SavePlayerData(playerMovement);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
