using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Heroes.Controller.Hook.Heroes;
using Heroes.Controller.Hook.Interfaces;
using Heroes.Controller.Hook.Interfaces.Structures;
using Reloaded.Hooks;
using Reloaded.Hooks.X86;
using static Reloaded.Hooks.X86.FunctionAttribute;

namespace Heroes.Controller.Hook
{
    public unsafe class ControllerHook : IControllerHook
    {
        /* Addresses of the SkyPad structures in game memory mapped to controller ports. */
        private static Dictionary<int, int> _skypadAddresses = new Dictionary<int, int>()
        {
            { 0x00A23A68, 0 },
            { 0x00A23AB4, 1 },
            { 0x00A23B00, 2 },
            { 0x00A23B4C, 3 }
        };

        private IHook<psPADServerPC> _psPadServerHook;
        private IHook<sGamePeri__MakeRepeatCount> _periMakeRepeatCountHook;
        private ReloadedController[] _controllers = new ReloadedController[4];
        
        /* Entry Point */
        public ControllerHook()
        {
            // Setup controllers.
            for (int x = 0; x < _controllers.Length; x++)
                _controllers[x] = new ReloadedController(x);

            // Hook get controls function.
            _psPadServerHook            = new Hook<psPADServerPC>(PSPADServerImpl, 0x444F30).Activate();
            _periMakeRepeatCountHook    = new Hook<sGamePeri__MakeRepeatCount>(MakeRepeatCountImpl, 0x00434FF0, 20).Activate();
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
            if (Utility.IsWindowActivated())
            {
                for (int x = 0; x < _controllers.Length; x++)
                    _controllers[x].SendInputs(this);
            }

            return 1;
        }

        /// <summary>
        /// Re-implements the original function which calculates/sets how much each button has been repeatedly pressed/tapped.
        /// This is the last function executed before the game uses our inputs to perform our actions.
        /// We add trigger pressure support here.
        /// </summary>
        private SkyPad* MakeRepeatCountImpl(SkyPad* skyPad)
        {
            if (_skypadAddresses.TryGetValue((int)skyPad, out int port))
                _controllers[port].SetTriggers(skyPad);

            return _periMakeRepeatCountHook.OriginalFunction(skyPad);
        }

        /* IControllerHook interface. */
        public event InputEvent SetInputs;
        public event InputEvent PostProcessInputs;
        public event OnInputEvent OnInput;

        public void InvokeSetInputs(ref Inputs inputs, int port)         => SetInputs?.Invoke(ref inputs, port);
        public void InvokePostProcessInputs(ref Inputs inputs, int port) => PostProcessInputs?.Invoke(ref inputs, port);
        public void InvokeOnInput(Inputs inputs, int port)               => OnInput?.Invoke(inputs, port);

        /* Hook function definitions. */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [Function(Reloaded.Hooks.X86.CallingConventions.Cdecl)]
        public delegate int psPADServerPC(); // 00444F30

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [Function(new[] { Register.eax, }, Register.eax, StackCleanup.Caller)]
        public delegate SkyPad* sGamePeri__MakeRepeatCount(SkyPad* skyPad); // 00434FF0
    }
}
