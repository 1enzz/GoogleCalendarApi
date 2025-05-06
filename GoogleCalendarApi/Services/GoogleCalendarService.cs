
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
        private readonly string[] Scopes = { CalendarService.Scope.Calendar };
        private const string AppName = "GoogleCalendar API";
        private readonly string CredentialPath = "Credentials/Credentials.json";

        public CalendarService GetCalendarService()
        {
            UserCredential credential;

            using (var stream = new FileStream(CredentialPath, FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;

                return new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = AppName,
                });
            }
        }

        public string CriarEvento(InformacoesProcesso jsonEventInfo)
        {
            DateTime dataFinalConvertida, dataPublicacaoConvertida;

            InformacoesProcesso info = new InformacoesProcesso();

            var service = GetCalendarService();
            
            Event newEvent = new Event()
            {
                Summary = jsonEventInfo.Processo,
                Location = jsonEventInfo.LocalProcesso,
                Description = "Agendamento processo que se iniciou em: " + jsonEventInfo.DataPublicacao,
                Start = new EventDateTime()
                {
                    DateTime = jsonEventInfo.DataFinal,
                    TimeZone = "America/Sao_Paulo"
                },
                End = new EventDateTime()
                {
                    DateTime = jsonEventInfo.DataFinal,
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