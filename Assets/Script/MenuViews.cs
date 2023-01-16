using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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