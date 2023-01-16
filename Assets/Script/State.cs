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