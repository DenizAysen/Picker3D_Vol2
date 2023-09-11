using UnityEngine;

public class OnLevelDestroyedCommand
{
    private Transform _levelHolder;
    internal OnLevelDestroyedCommand(Transform levelHolder)
    {
        _levelHolder = levelHolder;
    }
    internal void Execute()
    {
        if (_levelHolder.childCount <= 0) return;
        Object.Destroy(_levelHolder.GetChild(0).gameObject);
    }
}
