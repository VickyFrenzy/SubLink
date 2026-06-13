using System.Threading.Tasks;
using tech.SubLink.StreamElements.SEClient;

namespace tech.SubLink.StreamElements.Services;

internal sealed partial class StreamElementsService {
    private void WireCallbacks() {
        _streamElements.TipEvent += OnTipEvent;
    }

    private void OnTipEvent(object? sender, TipEventArgs e) {
        Task.Run(async () => {
            if (_rules is StreamElementsRules { OnTipEvent: { } callback })
                await callback(e);
        });
    }
}
