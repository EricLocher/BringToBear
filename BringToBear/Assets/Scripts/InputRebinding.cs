using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputRebinding : MonoBehaviour
{
    [SerializeField] private InputActionReference shootAction = null;
    [SerializeField] private PlayerController playerController = null;
    [SerializeField] private TMP_Text bindingUIText = null;
    [SerializeField] private GameObject startRebind = null;
    [SerializeField] private GameObject waitingForInput;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    public void StartRebinding()
    {
        Debug.Log("Hej hallå");
        startRebind.SetActive(false);
        waitingForInput.SetActive(true);

        //playerController.PlayerInput.SwitchCurrentActionMap("Menu");

        rebindingOperation = shootAction.action.PerformInteractiveRebinding().OnMatchWaitForAnother(0.1f).OnComplete(operation => RebindComplete()).Start();
    }

    private void RebindComplete()
    {
        Debug.Log("Hej hallå2");

        int bindinngIndex = shootAction.action.GetBindingIndexForControl(shootAction.action.controls[0]);

        bindingUIText.text = InputControlPath.ToHumanReadableString(shootAction.action.bindings[0].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);

        rebindingOperation.Dispose();
        startRebind.SetActive(true);
        waitingForInput.SetActive(false);

        //playerController.PlayerInput.SwitchCurrentActionMap("Controls");
    }
}
