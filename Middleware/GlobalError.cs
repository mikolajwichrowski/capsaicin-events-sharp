#pragma warning disable CS8618

using System.Text.Json;

namespace capsaicin_events_sharp;

public class GlobalError {
    public int StatusCode { get; set; }

    public string Message { get; set; }

    public override string ToString() {
        return JsonSerializer.Serialize(this);
    }
}