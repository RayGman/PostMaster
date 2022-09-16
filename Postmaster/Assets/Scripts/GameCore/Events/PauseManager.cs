using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour, IPauseHandler
{
    public static PauseManager pauseManager { get; private set; }
    public bool IsPaused { get; private set; }

    private readonly List<IPauseHandler> _handlers = new List<IPauseHandler>();

    private void Awake()
    {
        pauseManager = this;
    }

    public void Register(IPauseHandler handler)
    {
        _handlers.Add(handler);
    }

    public void UnRegister(IPauseHandler handler)
    {
        _handlers.Remove(handler);
    }

    public void SetPause(bool isPaused)
    {
        IsPaused = isPaused;

        foreach (var handler in _handlers)
        {
            handler.SetPause(IsPaused);
        }
    }
}
