using GoogleCalendarApi.Models;

namespace GoogleCalendarApi.Interfaces
{
    public interface IGoogleCalendarService
    {
        string CriarEvento(EventRequest request);
    }
}
