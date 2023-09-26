using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GiveRewardCommand 
{
    private PlayerManager _manager;
    public GiveRewardCommand(PlayerManager manager)
    {
        _manager = manager;
    }

    internal void Execute()
    {
        Debug.Log("Command calisiyor");
        var transform1 = _manager.transform;
        var position1 = transform1.position;
        var forcePos = new Vector3(position1.x, position1.y, position1.z + .9f);

        var colliders = Physics.OverlapBox(forcePos, Vector3.one * 2.5f);

        var rewardColliderList = colliders.Where(col => col.CompareTag("Reward")).ToList();
        
        List<short> values = new List<short>();
        foreach (var col in rewardColliderList)
        {
            if (col.GetComponent<RewardController>() == null) return;
            var rewardController = col.GetComponent<RewardController>();
            values.Add(rewardController.GetRewardValue());
            
        }
        short _maxValue = values.Max();
        Debug.Log(_maxValue + " elmas kazanildi");
    }
}
