using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class State 
{
    public StateMachine StateMachine { get; set; }
    public virtual void OnExit() { }
    public virtual void OnEnter() { }

    public void OnStartGame() { } 
    public void OnCardDropped() { }
    public void OnCardHovered() { }

    public virtual void OnSuspend() { }
    public virtual void OnResume() { }
}