using System;
using System.Runtime.CompilerServices;
using Heroes.Controller.Hook.Heroes;
using Heroes.Controller.Hook.Interfaces;
using Heroes.Controller.Hook.Interfaces.Enums;
using Heroes.Controller.Hook.Interfaces.Internal;
using Heroes.Controller.Hook.Interfaces.Structures;

namespace Heroes.Controller.Hook
{
    /// <summary>
    /// <see cref="ReloadedController"/> is an abstraction that writes Reloaded controls to Heroes' controller input struct.
    /// </summary>
    public unsafe class ReloadedController
    {
        // Sonic Heroes and XInput Controller
        private HeroesController* _heroesControllerPtr;
        private Inputs _lastFrameInputs;
        private int _port;
        private Config _config;

        /*
            ------------
            Constructors
            ------------
        */

        /// <summary>
        /// Creates a new <see cref="ReloadedController"/> that writes controls to Heroes' controller input struct.
        /// </summary>
        /// <param name="port">The controller port. Range 0-3.</param>
        /// <param name="modDirectory"></param>
        public ReloadedController(int port, string modDirectory)
        {
            _port = port;
            _heroesControllerPtr = HeroesControllerFactory.GetController(_port);
            _config = Config.FromPath(modDirectory, port);
        }

        /*
            ---------
            Functions
            ---------
        */

        /// <summary>
        /// Sends the input into the game by writing them into memory.
        /// </summary>
        public void SendInputs(ControllerHook hook)
        {
            var inputs = _config.UseOriginalInputs
                ? Inputs.FromHeroesController(*_heroesControllerPtr)
                : new Inputs();

            // Get inputs from subscribers.
            hook.InvokeSetInputs(ref inputs, _port);
            hook.InvokePostProcessInputs(ref inputs, _port);

            // Convert to game structure and write to memory.
            HeroesController.FromInputs(ref inputs, out var newInputs);
            newInputs.Finalize(_lastFrameInputs.ButtonFlags);
            *_heroesControllerPtr = newInputs;
            _lastFrameInputs = inputs;

            // Broadcast final inputs to subscribers.
            IHeroesController newInputsInterface = newInputs;
            hook.InvokeOnInput(new ExtendedHeroesController(ref newInputsInterface, inputs.LeftTriggerPressure, inputs.RightTriggerPressure), _port);
        }

        /// <summary>
        /// Writes the trigger pressures into game memory with data from the current frame.
        /// </summary>
        /// <remarks>
        ///     In a single game loop, the game will call the hook will execute <see cref="SendInputs"/>.
        ///     On return, the game will convert the internal structure into another structure <see cref="SkyPad"/>
        ///     supporting trigger pressure (while also doing some input post processing). We are modifying this structure
        ///     right before the game uses it to perform actions.
        /// </remarks>
        public void SetTriggers(SkyPad* skypad)
        {
            if ((_lastFrameInputs.ButtonFlags & ButtonFlags.CameraL) == 0)
                (*skypad).TriggerPressureL = _lastFrameInputs.LeftTriggerPressure;

            if ((_lastFrameInputs.ButtonFlags & ButtonFlags.CameraR) == 0)
                (*skypad).TriggerPressureR = _lastFrameInputs.RightTriggerPressure;
        }
    }
}