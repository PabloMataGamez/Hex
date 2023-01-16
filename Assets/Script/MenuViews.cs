using System;
using UnityEngine;

class MenuViews : MonoBehaviour
{
    public EventHandler PlayClicked;

    public void Play()
        => OnPlayClciked(EventArgs.Empty);

    protected virtual void OnPlayClciked(EventArgs eventArgs)
    {
        var handler = PlayClicked;
        handler?.Invoke(this, eventArgs);
    }
}