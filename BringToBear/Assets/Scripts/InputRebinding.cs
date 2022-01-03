using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputRebinding : MonoBehaviour
{
    //references
    [SerializeField] private InputActionReference shootAction = null;
    [SerializeField] private InputActionReference dashAction = null;
    [SerializeField] private InputActionReference dropAction = null;
    [SerializeField] private InputActionReference shieldAction = null;
    [SerializeField] private InputActionReference thrustAction = null;
    [SerializeField] private InputActionReference discardAction = null;

    //shoot
    [SerializeField] private TMP_Text bindingUI = null;
    [SerializeField] private GameObject startRebind = null;
    [SerializeField] private GameObject waitingForInput;
    //dash
    [SerializeField] private TMP_Text bindingUI2 = null;
    [SerializeField] private GameObject startRebind2 = null;
    [SerializeField] private GameObject waitingForInput2;
    //shield
    [SerializeField] private TMP_Text bindingUIText3 = null;
    [SerializeField] private GameObject startRebind3 = null;
    [SerializeField] private GameObject waitingForInput3;
    //drop
    [SerializeField] private TMP_Text bindingUIText4 = null;
    [SerializeField] private GameObject startRebind4 = null;
    [SerializeField] private GameObject waitingForInput4;
    //thrust
    [SerializeField] private TMP_Text bindingUIText5 = null;
    [SerializeField] private GameObject startRebind5 = null;
    [SerializeField] private GameObject waitingForInput5;
    //discard
    [SerializeField] private TMP_Text bindingUIText6 = null;
    [SerializeField] private GameObject startRebind6 = null;
    [SerializeField] private GameObject waitingForInput6;

    [SerializeField] private PlayerController playerController = null;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    private const string RebindsKey = "rebinds";

    private void Start()
    {
        Debug.Log("start");
        string rebinds = PlayerPrefs.GetString(RebindsKey, string.Empty);
        if (string.IsNullOrEmpty(rebinds)) { return; }
        playerController.PlayerInput.actions.LoadBindingOverridesFromJson(rebinds);

        bindingUI.text = InputControlPath.ToHumanReadableString(shootAction.action.bindings[0].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        bindingUI2.text = InputControlPath.ToHumanReadableString(dashAction.action.bindings[0].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        bindingUIText3.text = InputControlPath.ToHumanReadableString(shieldAction.action.bindings[0].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        bindingUIText4.text = InputControlPath.ToHumanReadableString(dropAction.action.bindings[0].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
    }

    public void StartRebindingShoot()
    {
        startRebind.SetActive(false);
        waitingForInput.SetActive(true);

        rebindingOperation = shootAction.action.PerformInteractiveRebinding().OnMatchWaitForAnother(0.1f).OnComplete(operation => RebindCompleteShoot()).Start();
    }

    private void RebindCompleteShoot()
    {
        int bindinngIndex = shootAction.action.GetBindingIndexForControl(shootAction.action.controls[0]);

        bindingUI.text = InputControlPath.ToHumanReadableString(shootAction.action.bindings[0].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        //save
        string rebinds = playerController.PlayerInput.actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString(RebindsKey, rebinds);

        rebindingOperation.Dispose();
        startRebind.SetActive(true);
        waitingForInput.SetActive(false);

        
    }

    public void StartRebindingDash()
    {
        startRebind2.SetActive(false);
        waitingForInput2.SetActive(true);

        rebindingOperation = dashAction.action.PerformInteractiveRebinding().OnMatchWaitForAnother(0.1f).OnComplete(operation => RebindCompleteDash()).Start();
    }

    private void RebindCompleteDash()
    {
        int bindinngIndex = dashAction.action.GetBindingIndexForControl(dashAction.action.controls[0]);

        bindingUI2.text = InputControlPath.ToHumanReadableString(dashAction.action.bindings[0].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        //save
        string rebinds = playerController.PlayerInput.actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString(RebindsKey, rebinds);

        rebindingOperation.Dispose();
        startRebind2.SetActive(true);
        waitingForInput2.SetActive(false);
    }

    public void StartRebindingShield()
    {
        startRebind3.SetActive(false);
        waitingForInput3.SetActive(true);

        rebindingOperation = shieldAction.action.PerformInteractiveRebinding().OnMatchWaitForAnother(0.1f).OnComplete(operation => RebindCompleteShield()).Start();
    }

    private void RebindCompleteShield()
    {
        int bindinngIndex = shieldAction.action.GetBindingIndexForControl(shieldAction.action.controls[0]);

        bindingUIText3.text = InputControlPath.ToHumanReadableString(shieldAction.action.bindings[0].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        //save
        string rebinds = playerController.PlayerInput.actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString(RebindsKey, rebinds);

        rebindingOperation.Dispose();
        startRebind3.SetActive(true);
        waitingForInput3.SetActive(false);
    }

    public void StartRebindingDrop()
    {
        startRebind4.SetActive(false);
        waitingForInput4.SetActive(true);

        rebindingOperation = dropAction.action.PerformInteractiveRebinding().OnMatchWaitForAnother(0.1f).OnComplete(operation => RebindCompleteDrop()).Start();
    }

    private void RebindCompleteDrop()
    {
        int bindinngIndex = dropAction.action.GetBindingIndexForControl(dropAction.action.controls[0]);

        bindingUIText4.text = InputControlPath.ToHumanReadableString(dropAction.action.bindings[0].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        //save
        string rebinds = playerController.PlayerInput.actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString(RebindsKey, rebinds);

        rebindingOperation.Dispose();
        startRebind4.SetActive(true);
        waitingForInput4.SetActive(false);
    }
    public void StartRebindingThrust()
    {
        startRebind5.SetActive(false);
        waitingForInput5.SetActive(true);

        rebindingOperation = thrustAction.action.PerformInteractiveRebinding().OnMatchWaitForAnother(0.1f).OnComplete(operation => RebindCompleteThrust()).Start();
    }

    private void RebindCompleteThrust()
    {
        int bindinngIndex = thrustAction.action.GetBindingIndexForControl(thrustAction.action.controls[0]);

        bindingUIText5.text = InputControlPath.ToHumanReadableString(thrustAction.action.bindings[0].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        //save
        string rebinds = playerController.PlayerInput.actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString(RebindsKey, rebinds);

        rebindingOperation.Dispose();
        startRebind5.SetActive(true);
        waitingForInput5.SetActive(false);
    }

    public void StartRebindingDiscard()
    {
        startRebind6.SetActive(false);
        waitingForInput6.SetActive(true);

        rebindingOperation = discardAction.action.PerformInteractiveRebinding().OnMatchWaitForAnother(0.1f).OnComplete(operation => RebindCompleteDiscard()).Start();
    }

    private void RebindCompleteDiscard()
    {
        int bindinngIndex = discardAction.action.GetBindingIndexForControl(discardAction.action.controls[0]);

        bindingUIText6.text = InputControlPath.ToHumanReadableString(discardAction.action.bindings[0].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        //save
        string rebinds = playerController.PlayerInput.actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString(RebindsKey, rebinds);

        rebindingOperation.Dispose();
        startRebind6.SetActive(true);
        waitingForInput6.SetActive(false);
    }
}
