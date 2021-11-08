using ThFnsc.NFe.Core.Entities.Shared;

namespace ThFnsc.NFe.Core.Entities;

public class NFNotifier : BaseSoftDeleteEntity
{
    public string Title { get; private set; }

    public string NotifierType { get; private set; }

    public string JsonData { get; private set; }

    public TimeSpan Delay { get; private set; }

    public ICollection<ScheduledGeneration> ScheduledGenerations { get; private set; }

    public NFNotifier(string title, string notifierType, string jsonData, TimeSpan delay)
    {
        Update(title, notifierType, jsonData, delay);
    }

    public void Update(string title, string notifierType, string jsonData, TimeSpan delay)
    {
        if (delay < TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(nameof(delay), "Delay cannot be negative");
        Title = title ?? throw new ArgumentNullException(nameof(title));
        NotifierType = notifierType ?? throw new ArgumentNullException(nameof(notifierType));
        JsonData = jsonData ?? throw new ArgumentNullException(nameof(jsonData));
        Delay = delay;
    }

    protected NFNotifier() { }
}
