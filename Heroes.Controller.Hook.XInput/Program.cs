using System;
using System.Diagnostics;
using Heroes.Controller.Hook.Interfaces;
using Heroes.Controller.Hook.Interfaces.Structures;
using Heroes.Controller.Hook.Interfaces.Structures.Interfaces;
using Reloaded.Mod.Interfaces;
using Reloaded.Mod.Interfaces.Internal;

namespace Heroes.Controller.Hook.XInput
{
    public class Program : IMod
    {
        public const string MyModId = "sonicheroes.controller.hook.xinput";
        private const string HookModId = "sonicheroes.controller.hook";
        
        public static IModLoader ModLoader;
        public static ILogger Logger;

        private XInput _xInput;
        private WeakReference<IControllerHook> _controllerHook;
        
        public void Start(IModLoaderV1 loader)
        {
            #if DEBUG
            Debugger.Launch();
            #endif
            ModLoader = (IModLoader)loader;
            Logger    = (ILogger) ModLoader.GetLogger();

            /* Your mod code starts here. */
            string modDirectory = ModLoader.GetDirectoryForModId(MyModId);

            _xInput = new XInput(modDirectory);
            ModLoader.ModLoaded += ModLoaded;
            SetupController();
        }

        private void ModLoaded(IModV1 modInstance, IModConfigV1 modConfig)
        {
            if (modConfig.ModId == HookModId)
                SetupController();
        }

        private void SetupController()
        {
            _controllerHook = ModLoader.GetController<IControllerHook>();
            if (_controllerHook.TryGetTarget(out var target))
                target.SetInputs += OnSetInputs;
        }

        private void TearDownController()
        {
            if (_controllerHook.TryGetTarget(out var target))
                target.SetInputs -= OnSetInputs;
        }

        private void OnSetInputs(ref IInputs inputs, int port) => _xInput.SendInputs(ref inputs, port);

        /* Mod loader actions. */
        public void Suspend()   => TearDownController();
        public void Resume()    => SetupController();
        public void Unload()    => TearDownController();

        public bool CanUnload()  => true;
        public bool CanSuspend() => true;
        public Action Disposing { get; }
    }
}
