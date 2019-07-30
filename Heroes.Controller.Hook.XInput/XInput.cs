using System;
using System.Collections.Generic;
using System.Text;
using Heroes.Controller.Hook.Interfaces.Structures;
using Heroes.Controller.Hook.XInput.Json;
using SharpDX.XInput;

namespace Heroes.Controller.Hook.XInput
{
    public class XInput
    {
        private SharpDX.XInput.Controller[] _controllers =
        {
            new SharpDX.XInput.Controller(UserIndex.One),
            new SharpDX.XInput.Controller(UserIndex.Two),
            new SharpDX.XInput.Controller(UserIndex.Three),
            new SharpDX.XInput.Controller(UserIndex.Four)
        };

        private Config[] _configurations;
        private ConfigReadWriter _configReadWriter;

        public XInput(string modFolder)
        {
            _configReadWriter   = new ConfigReadWriter(modFolder);
            _configurations     = new Config[_controllers.Length];
            for (int x = 0; x < _configurations.Length; x++)
                _configurations[x] = _configReadWriter.FromJson(x);
        }

        /// <summary>
        /// Sends inputs to the Inter Mod Communication's <see cref="Inputs"/> structure.
        /// </summary>
        public void SendInputs(ref Inputs inputs, int port)
        {
            var controller = _controllers[port];
            if (controller.IsConnected)
            {
                controller.GetState(out State state);
                Converter.ToHeroesController(ref state, ref inputs, _configurations[port]);
            }
        }
    }
}
