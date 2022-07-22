#pragma warning disable CS8618

using System.Net;

namespace capsaicin_events_sharp;

public class HttpError : HttpRequestException {
    public HttpError(string messsage, HttpStatusCode statusCode): base(messsage, null, statusCode) {}
}