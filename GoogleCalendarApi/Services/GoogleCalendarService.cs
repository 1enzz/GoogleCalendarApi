
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using GoogleCalendarApi.Models;
using GoogleCalendarApi.Interfaces;


namespace GoogleCalendarApi.Services
{
    public class GoogleCalendarService : IGoogleCalendarService
    {
        private const string AppName = "GoogleCalendarApi";

        public CalendarService GetCalendarService(string accessToken)
        {
            var credential = GoogleCredential.FromAccessToken(accessToken);

            return new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = AppName,
            });
            
        }

        public string CriarEvento(EventRequest req)
        {

            var service = GetCalendarService(req.AccessToken);
            var info = req.Info;
            Event newEvent = new Event()
            {
                Summary = info.Processo,
                Location = info.LocalProcesso,
                Description = "Agendamento processo que se iniciou em: " + info.DataPublicacao,
                Start = new EventDateTime()
                {
                    DateTime = info.DataFinal,
                    TimeZone = "America/Sao_Paulo"
                },
                End = new EventDateTime()
                {
                    DateTime = info.DataFinal.AddHours(1),
                    TimeZone = "America/Sao_Paulo"
                }
            };

            string calendarId = "primary";
            EventsResource.InsertRequest request = service.Events.Insert(newEvent, calendarId);
            Event createdEvent = request.Execute();

            return $"Evento criado: {createdEvent.HtmlLink}";
        }
    }
}