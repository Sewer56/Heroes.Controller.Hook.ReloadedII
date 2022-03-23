using Heroes.Controller.Hook.Interfaces;
using Heroes.Controller.Hook.Interfaces.Structures.Interfaces;
using Heroes.SDK.API;
using Heroes.SDK.Classes.NativeClasses;
using Heroes.SDK.Classes.PseudoNativeClasses;
using Heroes.SDK.Definitions.Structures.Input;
using Reloaded.Hooks.Definitions;

namespace Heroes.Controller.Hook;

public unsafe class ControllerHook : IControllerHook
{
    private IHook<InputFunctions.psPADServerPC> _psPadServerHook;
    private IHook<GamePeri.Native_MakeRepeatCount> _periMakeRepeatCountHook;
    private ReloadedController[] _controllers = new ReloadedController[4];

    /* Entry Point */
    public ControllerHook(string modDirectory, string configDirectory)
    {
        // Setup controllers.
        for (int x = 0; x < _controllers.Length; x++)
            _controllers[x] = new ReloadedController(x, modDirectory, configDirectory);

        // Hook get controls function.
        _psPadServerHook            = InputFunctions.Fun_psPADServerPC.Hook(PSPADServerImpl).Activate();
        _periMakeRepeatCountHook    = GamePeri.Fun_MakeRepeatCount.Hook(MakeRepeatCountImpl).Activate();
    }

    /// <summary>
    /// Disables the hooks.
    /// </summary>
    public void Suspend()
    {
        _psPadServerHook.Disable();
        _periMakeRepeatCountHook.Disable();
    }

    /// <summary>
    /// Re-enables the hooks.
    /// </summary>
    public void Resume()
    {
        _psPadServerHook.Enable();
        _periMakeRepeatCountHook.Enable();
    }

    /// <summary>
    /// Hook for the function which normally obtains the inputs from DirectInput.
    /// </summary>
    /// <returns>Game does not use return value.</returns>
    private int PSPADServerImpl()
    {
        _psPadServerHook.OriginalFunction();

        if (!Window.IsAnyWindowActivated()) 
            return 1;

        foreach (var controller in _controllers)
            controller.SendInputs(this);

        return 1;
    }

    /// <summary>
    /// Re-implements the original function which calculates/sets how much each button has been repeatedly pressed/tapped.
    /// This is the last function executed before the game uses our inputs to perform our actions.
    /// We add trigger pressure support here.
    /// </summary>
    private SkyPad* MakeRepeatCountImpl(SkyPad* skyPad)
    {
        int port = -1;

        // Find port using address.
        for (int x = 0; x < InputFunctions.FinalInputs.Count; x++)
        {
            if (&InputFunctions.FinalInputs.Pointer[x] != skyPad) 
                continue;

            port = x;
            break;
        }

        if (port != -1)
            _controllers[port].SetTriggers(skyPad);

        return _periMakeRepeatCountHook.OriginalFunction(skyPad);
    }

    /* IControllerHook interface. */
    public event InputEvent SetInputs;
    public event InputEvent PostProcessInputs;
    public event OnInputEvent OnInput;

    public void InvokeSetInputs(ref IInputs inputs, int port)         => SetInputs?.Invoke(ref inputs, port);
    public void InvokePostProcessInputs(ref IInputs inputs, int port) => PostProcessInputs?.Invoke(ref inputs, port);
    public void InvokeOnInput(IExtendedHeroesController inputs, int port) => OnInput?.Invoke(inputs, port);
}