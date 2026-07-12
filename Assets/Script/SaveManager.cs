using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }
    [SerializeField] private UnitDatabase unitDatabase;
    [SerializeField] private string saveFileName = "player_save.json";
    [SerializeField] private PlayerSaveData currentSaveData;

    public PlayerSaveData CurrentSaveData => currentSaveData;

    private string SavePath => Path.Combine(Application.persistentDataPath, saveFileName);

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        unitDatabase.Initialize();

        LoadGame();
    }

    public void CreateNewSave()
    {
        currentSaveData = new PlayerSaveData();
        currentSaveData.InitializeRuntimeData();
        currentSaveData.SyncUnitProgress(unitDatabase);
        SaveGame();
    }

    public void SaveGame()
    {
        if (currentSaveData == null)
        {
            Debug.LogError("저장할 플레이어 데이터가 없습니다.");
            return;
        }

        try
        {
            string json = JsonUtility.ToJson(currentSaveData, true);
            File.WriteAllText(SavePath, json);

            Debug.Log($"게임 저장 완료: {SavePath}");
        }
        catch (Exception exception)
        {
            Debug.LogError($"게임 저장 실패: {exception.Message}");
        }
    }

    public void LoadGame()
    {
        if (!File.Exists(SavePath))
        {
            CreateNewSave();
            return;
        }

        try
        {
            string json = File.ReadAllText(SavePath);

            currentSaveData = JsonUtility.FromJson<PlayerSaveData>(json);

            if (currentSaveData == null)
            {
                Debug.LogError("저장 데이터를 읽지 못했습니다.");
                CreateNewSave();
                return;
            }

            currentSaveData.InitializeRuntimeData();
            currentSaveData.SyncUnitProgress(unitDatabase);

            Debug.Log($"게임 불러오기 완료: {SavePath}");
        }
        catch (Exception exception)
        {
            Debug.LogError($"게임 불러오기 실패: {exception.Message}");

            BackupBrokenSave();
            CreateNewSave();
        }
    }

    public void DeleteSave()
    {
        if (File.Exists(SavePath))
        {
            File.Delete(SavePath);
        }

        CreateNewSave();
    }

    private void BackupBrokenSave()
    {
        if (!File.Exists(SavePath))
        {
            return;
        }

        string backupFileName =
            $"broken_save_{DateTime.Now:yyyyMMdd_HHmmss}.json";

        string backupPath =
            Path.Combine(Application.persistentDataPath, backupFileName);

        File.Copy(SavePath, backupPath, true);
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveGame();
        }
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
