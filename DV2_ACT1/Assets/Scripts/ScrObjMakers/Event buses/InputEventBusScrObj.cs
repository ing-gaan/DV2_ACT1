using UnityEngine;
using UnityEngine.InputSystem;
using System;

///<summary>Scriptable object to channel the new Unity input system events</summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Event Buses/Inputs events", fileName = "New inputEventBusScrObj")]
public class InputEventBusScrObj : ScriptableObject
{
    #region Unity actions
    public event Action<Vector2> MoveInputEvent;
    public event Action<bool> RunInputEvent;
    public event Action JumpInputEvent;
    public event Action LookInputEvent;
    public event Action<bool> AimInputEvent;
    public event Action<bool> ShootInputEvent;
    public event Action<int> ChangeWeaponInputEvent;
    public event Action ReloadWeaponInputEvent;
    public event Action<bool> InteractInputEvent;
    public event Action MoveVerticalInputEvent;
    public event Action MoveHorizontalInputEvent;
    public event Action UseHealerInputEvent;
    public event Action ChangeShieldInputEvent;
    #endregion

    #region Private attributes (Actions from Input system)
    //[SerializeField] private InputActionAsset actionAsset;
    private InputAction _move;
    private InputAction _run;
    private InputAction _jump;
    private InputAction _look;
    private InputAction _aim;
    private InputAction _shoot;
    private InputAction _changeWeapon;
    private InputAction _reloadWeapon;
    private InputAction _interact;
    private InputAction _moveVertical;
    private InputAction _moveHorizontal;
    private InputAction _useHealer;
    private InputAction _useShield;
    #endregion

    private PlayerControls _playerControls;


    private void OnEnable()
    {        
        SetInputs();
        RegisterRaiseActions();
        EnableInputs();                                       
    }

    private void OnDisable()
    {
        DisableInputs();
        UnregisterRaiseActions();               
    }

    ///<summary>Sets the input systems controls</summary>
    private void SetInputs()
    {
        _playerControls = new PlayerControls();

        //Keyboard inputs
        _move = _playerControls.GameplayCtrlMap.Move;
        _run = _playerControls.GameplayCtrlMap.Run;
        _jump = _playerControls.GameplayCtrlMap.Jump;
        _reloadWeapon = _playerControls.GameplayCtrlMap.ReloadWeapon;
        _interact = _playerControls.GameplayCtrlMap.Interact;
        _moveVertical = _playerControls.GameplayCtrlMap.MoveVertical;
        _moveHorizontal = _playerControls.GameplayCtrlMap.MoveHorizontal;
        _useHealer = _playerControls.GameplayCtrlMap.UseHealer;
        _useShield = _playerControls.GameplayCtrlMap.UseShield;

        //Mouse inputs
        _look = _playerControls.GameplayCtrlMap.Look;
        _aim = _playerControls.GameplayCtrlMap.Aim;
        _shoot = _playerControls.GameplayCtrlMap.Shoot;
        _changeWeapon = _playerControls.GameplayCtrlMap.ChangeWeapon;
    }

    ///<summary>Registers the input actions</summary>
    private void RegisterRaiseActions()
    {
        ///Raise actions - keyboard inputs
        _move.performed += RaiseMoveInputEvent;
        _move.canceled += RaiseMoveInputEvent;
        _run.performed += RaiseRunInputEvent;
        _run.canceled += RaiseRunInputEvent;
        _jump.performed += RaiseJumpInputEvent;
        _jump.canceled += RaiseJumpInputEvent;
        _interact.performed += RaiseInteractInputEvent;
        _interact.canceled += RaiseInteractInputEvent;
        _reloadWeapon.performed += RaiseReloadWeaponInputEvent;
        _moveVertical.performed += RaiseMoveVerticalInputEvent;
        _moveHorizontal.performed += RaiseMoveHorizontalInputEvent;
        _useHealer.performed += RaiseUseHealerInputEvent;
        _useShield.performed += RaiseChangeShieldInputEvent;

        ///Raise actions - Mouse inputs
        _look.performed += RaiseLookInputEvent;
        _aim.performed += RaiseAimInputEvent;
        _aim.canceled += RaiseAimInputEvent;
        _shoot.performed += RaiseShootInputEvent;
        _shoot.canceled += RaiseShootInputEvent;
        _changeWeapon.performed += RaiseChangeWeaponInputEvent;
    }

    ///<summary>enables the controls</summary>
    private void EnableInputs()
    {
        _playerControls.Enable();
    }

    ///<summary>Disables the controls</summary>
    private void DisableInputs()
    {
        _playerControls.Disable();
    }

    ///<summary>Unregisters the input actions</summary>
    private void UnregisterRaiseActions()
    {
        _move.performed -= RaiseMoveInputEvent;
        _move.canceled -= RaiseMoveInputEvent;
        _run.performed -= RaiseRunInputEvent;
        _run.canceled -= RaiseRunInputEvent;
        _jump.performed -= RaiseJumpInputEvent;
        _jump.canceled -= RaiseJumpInputEvent;
        _interact.performed -= RaiseInteractInputEvent;
        _interact.canceled += RaiseInteractInputEvent;
        _reloadWeapon.performed -= RaiseReloadWeaponInputEvent;
        _moveVertical.performed -= RaiseMoveVerticalInputEvent;
        _moveHorizontal.performed -= RaiseMoveHorizontalInputEvent;
        _useHealer.performed -= RaiseUseHealerInputEvent;
        _useShield.performed -= RaiseChangeShieldInputEvent;

        _look.performed -= RaiseLookInputEvent;
        _aim.performed -= RaiseAimInputEvent;
        _aim.canceled -= RaiseAimInputEvent;
        _shoot.performed -= RaiseShootInputEvent;
        _shoot.canceled -= RaiseShootInputEvent;
        _changeWeapon.performed -= RaiseChangeWeaponInputEvent;
    }

    ///<summary>Raise an event when the move control is manipulated</summary>
    ///<param name="context">The input context</param>
    private void RaiseMoveInputEvent(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        MoveInputEvent?.Invoke(direction);
    }

    ///<summary>Raise an event when the run control is manipulated</summary>
    ///<param name="context">The input context</param>
    public void RaiseRunInputEvent(InputAction.CallbackContext context)
    {
        bool running = context.ReadValueAsButton();
        RunInputEvent?.Invoke(running);
    }

    ///<summary>Raise an event when the jump control is manipulated</summary>
    ///<param name="context">The input context</param>
    public void RaiseJumpInputEvent(InputAction.CallbackContext context)
    {
        //bool jumping = context.ReadValueAsButton();
        JumpInputEvent?.Invoke();
    }

    ///<summary>Raise an event when the look control is manipulated</summary>
    ///<param name="context">The input context</param>
    public void RaiseLookInputEvent(InputAction.CallbackContext context)
    {
        LookInputEvent?.Invoke();
    }

    ///<summary>Raise an event when the aim control is manipulated</summary>
    ///<param name="context">The input context</param>
    public void RaiseAimInputEvent(InputAction.CallbackContext context)
    {
        bool aim = context.ReadValueAsButton();
        AimInputEvent?.Invoke(aim);
    }

    ///<summary>Raise an event when the shoot control is manipulated</summary>
    ///<param name="context">The input context</param>
    public void RaiseShootInputEvent(InputAction.CallbackContext context)
    {
        bool shoot = context.ReadValueAsButton();
        ShootInputEvent?.Invoke(shoot);
    }

    ///<summary>Raise an event when the change weapon control is manipulated</summary>
    ///<param name="context">The input context</param>
    public void RaiseChangeWeaponInputEvent(InputAction.CallbackContext context)
    { 
        int scroll = (int) context.ReadValue<float>();
        ChangeWeaponInputEvent?.Invoke(scroll);
    }

    ///<summary>Raise an event when the reload weapon control is manipulated</summary>
    ///<param name="context">The input context</param>
    public void RaiseReloadWeaponInputEvent(InputAction.CallbackContext context)
    {
        ReloadWeaponInputEvent?.Invoke();
    }

    ///<summary>Raise an event when the interact control is manipulated</summary>
    ///<param name="context">The input context</param>
    public void RaiseInteractInputEvent(InputAction.CallbackContext context)
    {        
        bool interact = context.ReadValueAsButton();

        InteractInputEvent?.Invoke(interact);
    }

    ///<summary>Raise an event when the move vertical control is manipulated</summary>
    ///<param name="context">The input context</param>
    public void RaiseMoveVerticalInputEvent(InputAction.CallbackContext context)
    {
        MoveVerticalInputEvent?.Invoke();
    }

    ///<summary>Raise an event when the move horizontal control is manipulated</summary>
    ///<param name="context">The input context</param>
    public void RaiseMoveHorizontalInputEvent(InputAction.CallbackContext context)
    {
        MoveHorizontalInputEvent?.Invoke();
    }

    ///<summary>Raise an event when the use healer control is manipulated</summary>
    ///<param name="context">The input context</param>
    public void RaiseUseHealerInputEvent(InputAction.CallbackContext context)
    {
        UseHealerInputEvent?.Invoke();
    }

    ///<summary>Raise an event when the change shield control is manipulated</summary>
    ///<param name="context">The input context</param>
    public void RaiseChangeShieldInputEvent(InputAction.CallbackContext context)
    {
        ChangeShieldInputEvent?.Invoke();
    }

}


