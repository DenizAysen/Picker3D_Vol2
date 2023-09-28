using System;
using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Self Variables

    #region Seriliazed Variables
    [SerializeField] private Transform levelHolder;
    [SerializeField] private byte totalLevelCount;
    [SerializeField] private CoreGameSignals gameSignals;
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
        if(CoreGameSignals.Instance == null)
        {
            Debug.Log("CoreGameSignals olusmadi");
            var var = Instantiate(gameSignals.gameObject, transform.parent);
            CoreGameSignals.Instance = var.GetComponent<CoreGameSignals>();
        }
           
        CoreGameSignals.Instance.onLevelInitialize += _levelLoaderCommand.Execute;
        CoreGameSignals.Instance.onClearActiveLevel += _levelDestroyerCommand.Execute;
        CoreGameSignals.Instance.onGetLevelValue += OnGetLevelValue;
        CoreGameSignals.Instance.onNextLevel += OnNextLevel;
        CoreGameSignals.Instance.onRestartLevel += OnRestartLevel;
    }
    
    public byte OnGetLevelValue() 
    { 
        return (byte)(_currentLevel % totalLevelCount); 
    }
    [NaughtyAttributes.Button]
    private void OnNextLevel()
    {
        _currentLevel++;
        
        CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
        CoreGameSignals.Instance.onReset?.Invoke();
        StartCoroutine(InitializeLevels());
    }
    private void OnRestartLevel()
    {
        CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
        CoreGameSignals.Instance.onReset?.Invoke();
        //CoreGameSignals.Instance.onLevelInitialize?.Invoke((byte)(_currentLevel % totalLevelCount));
        //CameraSignals.Instance.onSetCameraTarget?.Invoke();
        StartCoroutine(InitializeLevels());
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
        //Debug.Log((_currentLevel + 1) % totalLevelCount);
        CoreGameSignals.Instance.onLevelInitialize?.Invoke((byte)((_currentLevel+1) % totalLevelCount));
        CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Start, 1);
    }

    private IEnumerator InitializeLevels()
    {
        yield return new WaitForSeconds(.1f);
        CoreGameSignals.Instance.onLevelInitialize?.Invoke((byte)(_currentLevel % totalLevelCount));
        CoreGameSignals.Instance.onLevelInitialize?.Invoke((byte)((_currentLevel + 1) % totalLevelCount));
        CameraSignals.Instance.onSetCameraTarget?.Invoke();
    }
}
