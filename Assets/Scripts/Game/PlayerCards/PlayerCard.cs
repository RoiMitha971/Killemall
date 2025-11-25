using System.Threading.Tasks;
public abstract class PlayerCard<T> : Card<T>
{
    public bool CanBeUsed
    {
        get
        {
            return _canBeUsed;
        }
        protected set
        {
            _canBeUsed = value;
        }
    }

    private bool _canBeUsed;

    public abstract Task Use();

}
