using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using tech.SubLink.Platforms;

namespace tech.SubLink.Streampad.Services;

[PublicAPI]
public sealed class StreamPadRules : IPlatformRules {
    internal Func<string, float, Task>? OnControllerValue;

    public void ReactToControllerValue(Func<string, float, Task> with) { OnControllerValue = with; }
}
