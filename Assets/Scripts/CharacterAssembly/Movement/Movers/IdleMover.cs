namespace CharacterAssembly.Movement.Movers
{
    public class IdleMover : DefaultMover
    {
        public IdleMover(MovementController movementController, MovementSettings settings) : base(movementController, settings)
        {
        }

        public override void Enter()
        {
            AnimatorController.Idle();
        }
    }
}