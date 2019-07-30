namespace Heroes.Controller.Hook.Heroes
{
    /// <summary>
    /// Returns individual Heroes controllers for player(s) 1/2/3.
    /// </summary>
    public static unsafe class HeroesControllerFactory
    {
        /// <summary>
        /// Contains the address of the first controller inputs.
        /// </summary>
        public static HeroesController* PlayerOnePtr = (HeroesController*)0x00A2F948;

        /// <summary>
        /// Returns a pointer to an individual Sonic Heroes controller structure.
        /// </summary>
        public static HeroesController* GetController(int controllerPort)
        {
            return PlayerOnePtr + controllerPort;
        }
    }
}
