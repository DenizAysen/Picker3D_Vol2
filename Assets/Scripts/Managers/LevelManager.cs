using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Self Variables

    #region Seriliazed Variables
    [SerializeField] private Transform levelHolder;
    [SerializeField] private byte totalLevelCount;
    #endregion

    #region Private Variables

    private short _currentLevel;
    private LevelData _levelData;
    private OnLevelLoaderCommand _levelLoaderCommand;
    private OnLevelDestroyedCommand _levelDestroyerCommand;
    #endregion

    #endregion
    private void Awake()
    {
        _levelData = GetLevelData();
        _currentLevel = GetActiveLevel();

        Init();
    }
    private void Init()
    {
        _levelLoaderCommand = new OnLevelLoaderCommand(levelHolder);
        _levelDestroyerCommand = new OnLevelDestroyedCommand(levelHolder);
    }
    private LevelData GetLevelData()
    {
        return Resources.Load<CD_Level>("Data/CD_Level").Levels[_currentLevel];
    }

    private byte GetActiveLevel()
    {
        return (byte) _currentLevel;
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onLevelInitialize += _levelLoaderCommand.Execute;
        CoreGameSignals.Instance.onClearActiveLevel += _levelDestroyerCommand.Execute;
        CoreGameSignals.Instance.onGetLevelValue += OnGetLevelValue;
        CoreGameSignals.Instance.onNextLevel += OnNextLevel;
        CoreGameSignals.Instance.onRestartLevel += OnRestartLevel;
    }
    public byte OnGetLevelValue() 
    { 
        return (byte)_currentLevel; 
    }
    private void OnNextLevel()
    {
        _currentLevel++;
        CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
        CoreGameSignals.Instance.onReset?.Invoke();
        CoreGameSignals.Instance.onLevelInitialize?.Invoke((byte)(_currentLevel % totalLevelCount));
    }
    private void OnRestartLevel()
    {
        _currentLevel++;
        CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
        CoreGameSignals.Instance.onReset?.Invoke();
        CoreGameSignals.Instance.onLevelInitialize?.Invoke((byte)(_currentLevel % totalLevelCount));
    }
    private void UnSubscribeEvents()
    {
        CoreGameSignals.Instance.onLevelInitialize -= _levelLoaderCommand.Execute;
        CoreGameSignals.Instance.onClearActiveLevel -= _levelDestroyerCommand.Execute;
        CoreGameSignals.Instance.onGetLevelValue -= OnGetLevelValue;
        CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
        CoreGameSignals.Instance.onRestartLevel -= OnRestartLevel;
    }
    private void OnDisable()
    {
        UnSubscribeEvents();
    }
    private void Start()
    {
        CoreGameSignals.Instance.onLevelInitialize?.Invoke((byte) (_currentLevel % totalLevelCount));
    }
    
}
