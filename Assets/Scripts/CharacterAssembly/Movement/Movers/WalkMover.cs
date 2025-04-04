namespace CharacterAssembly.Movement.Movers
{
    public class WalkMover : DefaultMover
    {
        public WalkMover(MovementController movementController, MovementSettings settings) : base(movementController, settings)
        {
        }

        public override void Enter()
        {
            AnimatorController.Walk();
        }
    }
}