namespace CharacterAssembly.Movement.Movers
{
    public class RunMover : DefaultMover
    {
        public RunMover(MovementController movementController, MovementSettings settings) : base(movementController, settings)
        {
        }

        public override void Enter()
        {
            AnimatorController.Run();
        }
    }
}