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
        Debug.Log("pool command calisti");
        var transform1 = _manager.transform;
        var position1 = transform1.position;
        var forcePos = new Vector3(position1.x + .1f, position1.y - 1.5f, position1.z + 1.26f);

        var colliders = Physics.OverlapSphere(forcePos, 1.35f);

        var collectableColliderList = colliders.Where(col => col.CompareTag("Collectable")).ToList();
        Debug.Log("Miknatista " + collectableColliderList.Count + " kadar top var");
        foreach (var col in collectableColliderList)
        {
            if (col.GetComponent<Rigidbody>() == null) continue;
            var rb = col.GetComponent<Rigidbody>();
            rb.AddForce(new Vector3(0, _forceData.ForceParameters.y, _forceData.ForceParameters.z), ForceMode.Impulse);
        }

        collectableColliderList.Clear();
    }
}
