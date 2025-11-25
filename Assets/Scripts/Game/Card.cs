using UnityEngine;

public abstract class Card<T> : MonoBehaviour
{
    public bool Active
    {
        get
        {
            return _active;
        }
        protected set
        {
            _active = value;
            gameObject.SetActive(value);
        }
    }

    protected T _context;
    private bool _active;
    public abstract void Activate(T card);
}
