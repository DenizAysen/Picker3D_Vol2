using UnityEngine;

public class OnLevelLoaderCommand
{
    private Transform _levelHolder;
    internal OnLevelLoaderCommand(Transform levelHolder)
    {
        _levelHolder = levelHolder;
    }
    internal void Execute(byte levelIndex)
    {
        //2 level arasi bosluk 174.77
        if(_levelHolder.childCount == 0)
        {
            Object.Instantiate(Resources.Load<GameObject>($"Prefabs/LevelPrefabs/level {levelIndex}"), _levelHolder);
        }
            
        else
            Object.Instantiate(Resources.Load<GameObject>($"Prefabs/LevelPrefabs/PrefabVariants/level {levelIndex}"), 
                _levelHolder.GetChild(0).position + Vector3.forward * 174.77f,Quaternion.identity,_levelHolder);
    }
}
