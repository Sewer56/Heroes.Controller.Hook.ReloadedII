using System;
using System.Collections.Generic;
using System.Text;
using Heroes.Controller.Hook.Interfaces.Structures;
using Heroes.Controller.Hook.XInput.Configuration;
using SharpDX.XInput;

namespace Heroes.Controller.Hook.XInput
{
    public class XInput
    {
        private const int XInputControllerLimit = 4;
        private Dictionary<int, ControllerConfigTuple> _controllers = new Dictionary<int, ControllerConfigTuple>();

        public XInput(string modFolder)
        {
            var configReadWriter = new Configurator(modFolder);

            for (int controllerNo = 0; controllerNo < XInputControllerLimit; controllerNo++)
            {
                var config = configReadWriter.GetConfiguration<Config>(controllerNo);
                if (config.ControllerPort == -1)
                    config.ControllerPort = controllerNo;

                config.Save();
                _controllers[config.ControllerPort] = new ControllerConfigTuple(new SharpDX.XInput.Controller((UserIndex) controllerNo), config);
            }
        }

        /// <summary>
        /// Sends inputs to the Inter Mod Communication's <see cref="Inputs"/> structure.
        /// </summary>
        public void SendInputs(ref Inputs inputs, int port)
        {
            if (_controllers.TryGetValue(port, out var controllerTuple))
            {
                var controller = controllerTuple.Controller;
                var config     = controllerTuple.Config;

                if (!controller.IsConnected)
                    return;

                controller.GetState(out State state);
                Converter.ToHeroesController(ref state, ref inputs, config);
            }
        }
    }
}
