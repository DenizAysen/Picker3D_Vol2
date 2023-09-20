using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ForceBallsToPoolCommand
{
    private PlayerManager _manager;
    private PlayerForceData _forceData;
    public ForceBallsToPoolCommand(PlayerManager manager, PlayerForceData forceData)
    {
        _manager = manager;
        _forceData = forceData;
    }
    internal void Execute()
    {
        
        var transform1 = _manager.transform;
        var position1 = transform1.position;
        var forcePos = new Vector3(position1.x, position1.y, position1.z + 1.25f);

        var colliders = Physics.OverlapSphere(forcePos, 1.75f);

        var collectableColliderList = colliders.Where(col => col.CompareTag("Collectable")).ToList();

        foreach (var col in collectableColliderList)
        {
            if (col.GetComponent<Rigidbody>() == null) continue;
            var rb = col.GetComponent<Rigidbody>();
            rb.AddForce(new Vector3(0, _forceData.ForceParameters.y, _forceData.ForceParameters.z), ForceMode.Impulse);
        }

        collectableColliderList.Clear();
    }
}
