using System;

public class Timer
{
    public bool IsRunning { get; private set; }
    public event Action TimerFinished;
    
    float duration;
    float timeLeft;


    public Timer(float duration)
    {
        this.duration = duration;
    }

    public void Run()
    {
        IsRunning = true;

        timeLeft = duration;
    }

    public void Tick(float deltaTime)
    {
        if (!IsRunning) return;

        timeLeft -= deltaTime;

        if (timeLeft > 0) return;
        
        IsRunning = false;
        TimerFinished?.Invoke();
    }
}