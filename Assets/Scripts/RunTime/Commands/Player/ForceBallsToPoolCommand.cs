using System.Collections;
using System.Collections.Generic;
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

    }
}
