// GENERATED AUTOMATICALLY FROM 'Assets/Input/PlayerInputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputs"",
    ""maps"": [
        {
            ""name"": ""Controls"",
            ""id"": ""36739c6b-4067-46c7-8694-cf06cb0016b7"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""27ccc30b-1d19-4ff9-901f-d960ec1275fc"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Thrust"",
                    ""type"": ""Value"",
                    ""id"": ""0130b7f6-e777-44c8-b0f5-122189b2a58e"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Respawn"",
                    ""type"": ""Button"",
                    ""id"": ""1e7e3b15-ca2f-465b-9482-92582552c339"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Brake"",
                    ""type"": ""Value"",
                    ""id"": ""253a98d9-c397-42a8-a7ed-7f6db791d36f"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""cd195701-7ebc-4c83-8506-6085c30027b2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Drift"",
                    ""type"": ""Value"",
                    ""id"": ""dc3be3fd-20e8-4801-a300-b1cef8d5546f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d8d02983-5298-460f-864d-236afb2e19bd"",
                    ""path"": ""<DualShockGamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PS4"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0265d24b-a9ea-4b0c-b552-b6d4458c0a91"",
                    ""path"": ""<DualShockGamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PS4"",
                    ""action"": ""Thrust"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a40f0f05-a228-4df3-9901-c8a6b6094f9a"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PS4"",
                    ""action"": ""Thrust"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""db3f1b53-c5ab-4736-b7e4-4115471cbe94"",
                    ""path"": ""<DualShockGamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PS4"",
                    ""action"": ""Respawn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6c752df6-2ff1-4f75-a0bf-d63095b6281d"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PS4"",
                    ""action"": ""Respawn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fb3c244f-8389-4bc2-92f7-821b89de5e7c"",
                    ""path"": ""<DualShockGamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PS4"",
                    ""action"": ""Brake"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9f3e2a03-2dcd-4af5-a8e3-c57c2874220c"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PS4"",
                    ""action"": ""Brake"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f28e530d-aa74-4f80-87aa-59b06814f869"",
                    ""path"": ""<DualShockGamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PS4"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bf8b2f9f-784b-4b88-b1dd-50f8906881ad"",
                    ""path"": ""<DualShockGamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PS4"",
                    ""action"": ""Drift"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""PS4"",
            ""bindingGroup"": ""PS4"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<DualShockGamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Controls
        m_Controls = asset.FindActionMap("Controls", throwIfNotFound: true);
        m_Controls_Movement = m_Controls.FindAction("Movement", throwIfNotFound: true);
        m_Controls_Thrust = m_Controls.FindAction("Thrust", throwIfNotFound: true);
        m_Controls_Respawn = m_Controls.FindAction("Respawn", throwIfNotFound: true);
        m_Controls_Brake = m_Controls.FindAction("Brake", throwIfNotFound: true);
        m_Controls_Fire = m_Controls.FindAction("Fire", throwIfNotFound: true);
        m_Controls_Drift = m_Controls.FindAction("Drift", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Controls
    private readonly InputActionMap m_Controls;
    private IControlsActions m_ControlsActionsCallbackInterface;
    private readonly InputAction m_Controls_Movement;
    private readonly InputAction m_Controls_Thrust;
    private readonly InputAction m_Controls_Respawn;
    private readonly InputAction m_Controls_Brake;
    private readonly InputAction m_Controls_Fire;
    private readonly InputAction m_Controls_Drift;
    public struct ControlsActions
    {
        private @PlayerControls m_Wrapper;
        public ControlsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Controls_Movement;
        public InputAction @Thrust => m_Wrapper.m_Controls_Thrust;
        public InputAction @Respawn => m_Wrapper.m_Controls_Respawn;
        public InputAction @Brake => m_Wrapper.m_Controls_Brake;
        public InputAction @Fire => m_Wrapper.m_Controls_Fire;
        public InputAction @Drift => m_Wrapper.m_Controls_Drift;
        public InputActionMap Get() { return m_Wrapper.m_Controls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ControlsActions set) { return set.Get(); }
        public void SetCallbacks(IControlsActions instance)
        {
            if (m_Wrapper.m_ControlsActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnMovement;
                @Thrust.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnThrust;
                @Thrust.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnThrust;
                @Thrust.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnThrust;
                @Respawn.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnRespawn;
                @Respawn.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnRespawn;
                @Respawn.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnRespawn;
                @Brake.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnBrake;
                @Brake.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnBrake;
                @Brake.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnBrake;
                @Fire.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnFire;
                @Drift.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnDrift;
                @Drift.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnDrift;
                @Drift.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnDrift;
            }
            m_Wrapper.m_ControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Thrust.started += instance.OnThrust;
                @Thrust.performed += instance.OnThrust;
                @Thrust.canceled += instance.OnThrust;
                @Respawn.started += instance.OnRespawn;
                @Respawn.performed += instance.OnRespawn;
                @Respawn.canceled += instance.OnRespawn;
                @Brake.started += instance.OnBrake;
                @Brake.performed += instance.OnBrake;
                @Brake.canceled += instance.OnBrake;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @Drift.started += instance.OnDrift;
                @Drift.performed += instance.OnDrift;
                @Drift.canceled += instance.OnDrift;
            }
        }
    }
    public ControlsActions @Controls => new ControlsActions(this);
    private int m_PS4SchemeIndex = -1;
    public InputControlScheme PS4Scheme
    {
        get
        {
            if (m_PS4SchemeIndex == -1) m_PS4SchemeIndex = asset.FindControlSchemeIndex("PS4");
            return asset.controlSchemes[m_PS4SchemeIndex];
        }
    }
    public interface IControlsActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnThrust(InputAction.CallbackContext context);
        void OnRespawn(InputAction.CallbackContext context);
        void OnBrake(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnDrift(InputAction.CallbackContext context);
    }
}
