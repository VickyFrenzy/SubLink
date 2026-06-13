using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using tech.SubLink.Platforms;
using tech.SubLink.StreamElements.SEClient;

namespace tech.SubLink.StreamElements.Services;

[PublicAPI]
public sealed class StreamElementsRules : IPlatformRules {
    internal Func<TipEventArgs, Task>? OnTipEvent;

    public void ReactToTipEvent(Func<TipEventArgs, Task> with) { OnTipEvent = with; }
}
