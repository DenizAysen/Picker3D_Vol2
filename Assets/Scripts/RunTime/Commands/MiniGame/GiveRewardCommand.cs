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
        var forcePos = new Vector3(position1.x, position1.y, position1.z + .76f);

        var colliders = Physics.OverlapSphere(forcePos, 1.4f);

        var rewardColliderList = colliders.Where(col => col.CompareTag("Reward")).ToList();
        
        List<short> values = new List<short>();
        foreach (var col in rewardColliderList)
        {
            if (col.GetComponent<RewardController>() == null) return;
            var rewardController = col.GetComponent<RewardController>();
            values.Add(rewardController.GetRewardValue());
            
        }
        short _maxValue = values.Max();
        MiniGameSignals.Instance.onGetReward?.Invoke(_maxValue);
    }
}
