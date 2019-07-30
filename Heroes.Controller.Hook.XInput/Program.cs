using System;
using System.Diagnostics;
using Heroes.Controller.Hook.Interfaces;
using Heroes.Controller.Hook.Interfaces.Structures;
using Heroes.Controller.Hook.XInput.Help;
using Reloaded.Mod.Interfaces;
using Reloaded.Mod.Interfaces.Internal;

namespace Heroes.Controller.Hook.XInput
{
    public class Program : IMod
    {
        private const string HookModId = "sonicheroes.controller.hook";
        private const string MyModId = "sonicheroes.controller.hook.xinput";

        private XInput _xInput;
        private IModLoader _modLoader;
        private WeakReference<IControllerHook> _controllerHook;
        
        public void Start(IModLoaderV1 loader)
        {
            #if DEBUG
            Debugger.Launch();
            #endif
            _modLoader = (IModLoader)loader;

            /* Your mod code starts here. */
            string modDirectory = _modLoader.GetDirectoryForModId(MyModId);
            Buttons.SaveIfNotExist(modDirectory);

            _xInput = new XInput(modDirectory);
            _modLoader.ModLoaded += ModLoaded;
            SetupController();
        }

        private void ModLoaded(IModV1 modInstance, IModConfigV1 modConfig)
        {
            if (modConfig.ModId == HookModId)
                SetupController();
        }

        private void SetupController()
        {
            _controllerHook = _modLoader.GetController<IControllerHook>();
            if (_controllerHook.TryGetTarget(out var target))
                target.SetInputs += OnSetInputs;
        }

        private void TearDownController()
        {
            if (_controllerHook.TryGetTarget(out var target))
                target.SetInputs -= OnSetInputs;
        }

        private void OnSetInputs(ref Inputs inputs, int port) => _xInput.SendInputs(ref inputs, port);

        /* Mod loader actions. */
        public void Suspend()   => TearDownController();
        public void Resume()    => SetupController();
        public void Unload()    => TearDownController();

        public bool CanUnload()  => false;
        public bool CanSuspend() => false;
        public Action Disposing { get; }
    }
}
