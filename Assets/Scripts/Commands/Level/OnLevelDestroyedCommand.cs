using UnityEngine;

public class OnLevelDestroyedCommand
{
    private Transform _levelHolder;
    public OnLevelDestroyedCommand(Transform levelHolder)
    {
        _levelHolder = levelHolder;
    }
    public void Execute()
    {
        if (_levelHolder.childCount <= 0) return;
        Object.Destroy(_levelHolder.GetChild(0).gameObject);
    }
}
