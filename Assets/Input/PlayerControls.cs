//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Input/PlayerControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Controller"",
            ""id"": ""79e898bd-bd6f-4514-af73-495a1fef30f3"",
            ""actions"": [
                {
                    ""name"": ""SpecialAttack"",
                    ""type"": ""Button"",
                    ""id"": ""6729a8a6-6f48-4121-a9ac-47e6c5617fc2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""GaugeRefill"",
                    ""type"": ""Button"",
                    ""id"": ""30bbcc9b-7a65-48d8-afec-128408e32dc8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Guard"",
                    ""type"": ""Button"",
                    ""id"": ""610930cd-e208-4d0c-af58-9a80a77ee3e7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""StandardAttack"",
                    ""type"": ""Button"",
                    ""id"": ""9ec31b04-aa3a-4582-9e46-c15ac957019e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PreviousSelection"",
                    ""type"": ""Button"",
                    ""id"": ""712cc7b4-c965-4e57-bfc2-d932aa13bca3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""NextSelection"",
                    ""type"": ""Button"",
                    ""id"": ""4c6e6b7c-91f0-48fe-892f-3fd568995aaf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""712e7496-64ff-4a00-b3df-bd569667be1e"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpecialAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""21b9383e-9741-4506-abad-871edec483f6"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GaugeRefill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9f4939e5-d57d-4fec-841b-773ab09eda54"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Guard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""69a0e786-7ec5-4ae3-b650-1fa6a78bbc1c"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""StandardAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""184afd1a-35b5-4b21-a8e5-a6cbfdc7fffa"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PreviousSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a9280bc1-1315-487b-bf19-cf3da1b2062e"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NextSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Controller
        m_Controller = asset.FindActionMap("Controller", throwIfNotFound: true);
        m_Controller_SpecialAttack = m_Controller.FindAction("SpecialAttack", throwIfNotFound: true);
        m_Controller_GaugeRefill = m_Controller.FindAction("GaugeRefill", throwIfNotFound: true);
        m_Controller_Guard = m_Controller.FindAction("Guard", throwIfNotFound: true);
        m_Controller_StandardAttack = m_Controller.FindAction("StandardAttack", throwIfNotFound: true);
        m_Controller_PreviousSelection = m_Controller.FindAction("PreviousSelection", throwIfNotFound: true);
        m_Controller_NextSelection = m_Controller.FindAction("NextSelection", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Controller
    private readonly InputActionMap m_Controller;
    private IControllerActions m_ControllerActionsCallbackInterface;
    private readonly InputAction m_Controller_SpecialAttack;
    private readonly InputAction m_Controller_GaugeRefill;
    private readonly InputAction m_Controller_Guard;
    private readonly InputAction m_Controller_StandardAttack;
    private readonly InputAction m_Controller_PreviousSelection;
    private readonly InputAction m_Controller_NextSelection;
    public struct ControllerActions
    {
        private @PlayerControls m_Wrapper;
        public ControllerActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @SpecialAttack => m_Wrapper.m_Controller_SpecialAttack;
        public InputAction @GaugeRefill => m_Wrapper.m_Controller_GaugeRefill;
        public InputAction @Guard => m_Wrapper.m_Controller_Guard;
        public InputAction @StandardAttack => m_Wrapper.m_Controller_StandardAttack;
        public InputAction @PreviousSelection => m_Wrapper.m_Controller_PreviousSelection;
        public InputAction @NextSelection => m_Wrapper.m_Controller_NextSelection;
        public InputActionMap Get() { return m_Wrapper.m_Controller; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ControllerActions set) { return set.Get(); }
        public void SetCallbacks(IControllerActions instance)
        {
            if (m_Wrapper.m_ControllerActionsCallbackInterface != null)
            {
                @SpecialAttack.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnSpecialAttack;
                @SpecialAttack.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnSpecialAttack;
                @SpecialAttack.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnSpecialAttack;
                @GaugeRefill.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnGaugeRefill;
                @GaugeRefill.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnGaugeRefill;
                @GaugeRefill.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnGaugeRefill;
                @Guard.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnGuard;
                @Guard.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnGuard;
                @Guard.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnGuard;
                @StandardAttack.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnStandardAttack;
                @StandardAttack.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnStandardAttack;
                @StandardAttack.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnStandardAttack;
                @PreviousSelection.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnPreviousSelection;
                @PreviousSelection.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnPreviousSelection;
                @PreviousSelection.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnPreviousSelection;
                @NextSelection.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnNextSelection;
                @NextSelection.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnNextSelection;
                @NextSelection.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnNextSelection;
            }
            m_Wrapper.m_ControllerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SpecialAttack.started += instance.OnSpecialAttack;
                @SpecialAttack.performed += instance.OnSpecialAttack;
                @SpecialAttack.canceled += instance.OnSpecialAttack;
                @GaugeRefill.started += instance.OnGaugeRefill;
                @GaugeRefill.performed += instance.OnGaugeRefill;
                @GaugeRefill.canceled += instance.OnGaugeRefill;
                @Guard.started += instance.OnGuard;
                @Guard.performed += instance.OnGuard;
                @Guard.canceled += instance.OnGuard;
                @StandardAttack.started += instance.OnStandardAttack;
                @StandardAttack.performed += instance.OnStandardAttack;
                @StandardAttack.canceled += instance.OnStandardAttack;
                @PreviousSelection.started += instance.OnPreviousSelection;
                @PreviousSelection.performed += instance.OnPreviousSelection;
                @PreviousSelection.canceled += instance.OnPreviousSelection;
                @NextSelection.started += instance.OnNextSelection;
                @NextSelection.performed += instance.OnNextSelection;
                @NextSelection.canceled += instance.OnNextSelection;
            }
        }
    }
    public ControllerActions @Controller => new ControllerActions(this);
    public interface IControllerActions
    {
        void OnSpecialAttack(InputAction.CallbackContext context);
        void OnGaugeRefill(InputAction.CallbackContext context);
        void OnGuard(InputAction.CallbackContext context);
        void OnStandardAttack(InputAction.CallbackContext context);
        void OnPreviousSelection(InputAction.CallbackContext context);
        void OnNextSelection(InputAction.CallbackContext context);
    }
}
