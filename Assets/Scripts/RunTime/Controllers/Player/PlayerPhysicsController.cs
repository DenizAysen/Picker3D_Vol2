using UnityEngine;
using DG.Tweening;
public class PlayerPhysicsController : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private PlayerManager manager;
    [SerializeField] private new Collider collider;
    [SerializeField] private new Rigidbody rigidbody;

    #endregion

    #region Private Variables

    private const string _stageArea = "StageArea";
    private const string _finish = "Finish";
    private const string _miniGameArea = "MiniGameArea";
    private const string _reward = "Reward";
    #endregion

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_stageArea))
        {
            manager.ForceCommand.Execute();
            CoreGameSignals.Instance.onStageAreaEntered?.Invoke();
            InputSignals.Instance.onDisableInput?.Invoke();

            DOVirtual.DelayedCall(3, () =>
            {
                PoolController poolController = other.transform.parent.GetComponentInChildren<PoolController>();
                var result = poolController.TakeResults(manager.StageValue);

                if (result)
                {
                    CoreGameSignals.Instance.onIncreaseCollectedCount?.Invoke(poolController.GetCollectedCount());
                    CoreGameSignals.Instance.onStageAreaSuccessFull?.Invoke(manager.StageValue);
                    InputSignals.Instance.onEnableInput?.Invoke();
                }
                else
                {
                    CoreGameSignals.Instance.onLevelFailed?.Invoke();
                }
            });
            return;
        }
        if (other.CompareTag(_finish))
        {
            CoreGameSignals.Instance.onFinishAreaEntered?.Invoke();
            InputSignals.Instance.onDisableInput?.Invoke();
           // CoreGameSignals.Instance.onLevelSuccessfull?.Invoke();
            return;
        }
        if (other.CompareTag(_miniGameArea))
        {
            MiniGameSignals.Instance.onMiniGameAreaEntered?.Invoke();
            return;
        }
        if (other.CompareTag(_reward))
        {
            // burasi gecici movement controllerda tek seferde cagirilacak
            manager.GiveReward.Execute();         
            return;
        }
        //Odev olarak mini game yapilacak
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        var transform1 = manager.transform;
        var position1 = transform1.position;
        var forcePos = new Vector3(position1.x, position1.y + 1f, position1.z + 1f);

        
        Gizmos.DrawSphere(new Vector3(position1.x, position1.y, position1.z + 1.25f), 1.75f);
       // Gizmos.DrawCube(new Vector3(position1.x, position1.y, position1.z+.9f), Vector3.one*2.5f);
    }
    public void OnReset()
    {

    }
}
