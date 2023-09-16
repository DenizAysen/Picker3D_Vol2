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
                 var result = other.transform.parent.GetComponentInChildren<PoolController>().TakeResults(manager.StageValue);

                 if (result)
                 {
                     CoreGameSignals.Instance.onStageAreaSuccessFull?.Invoke(manager.StageValue);
                     InputSignals.Instance.onEnableInput?.Invoke();
                 }
                 else CoreGameSignals.Instance.onLevelFailed?.Invoke();

             });
            return;
        }
        if (other.CompareTag(_finish))
        {
            CoreGameSignals.Instance.onFinishAreaEntered?.Invoke();
            InputSignals.Instance.onDisableInput?.Invoke();
            CoreGameSignals.Instance.onLevelSuccessfull?.Invoke();
            return;
        }

        //Odev olarak mini game yapilacak
    }
    public void OnReset()
    {

    }
}
