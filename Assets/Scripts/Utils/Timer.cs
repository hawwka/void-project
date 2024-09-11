public class Timer
{
    public bool IsRunning { get; private set; }

    float duration;
    float timeLeft;


    public Timer(float duration)
    {
        this.duration = duration;
    }

    public void Start()
    {
        IsRunning = true;

        timeLeft = duration;
    }

    public void Tick(float deltaTime)
    {
        if (!IsRunning) return;

        timeLeft -= deltaTime;

        if (timeLeft <= 0)
            IsRunning = false;
    }
}