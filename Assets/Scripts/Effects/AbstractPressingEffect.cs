public abstract class AbstractPressingEffect : AbstractCreatedObject
{
    public virtual void SetStartPositionAndStartRun()
    {
        transform.localPosition = Parent.position;
        PlayAnimation();
    }

    protected abstract void PlayAnimation();
}