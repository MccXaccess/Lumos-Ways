public abstract class BaseCharacterStateAbstract
{
    public abstract void EnterState(CharacterStateManager character);
    public abstract void FixedUpdateState(CharacterStateManager character);
    public abstract void UpdateState(CharacterStateManager character);
    public abstract void ExitState(CharacterStateManager character);
}