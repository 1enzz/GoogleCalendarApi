using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Tasks.v1;
using GoogleCalendarApi.Interfaces;
using Google.Apis.Util.Store;
using GoogleCalendarApi.Models;
using Google.Apis.Tasks.v1.Data;
using System.Globalization;

namespace GoogleCalendarApi.Services
{
    public class GoogleTasksServices : IGoogleTaskService
    {
        private readonly string[] Scopes = {"https://www.googleapis.com/auth/tasks"};
        private const string AppName = "GoogleCalendar API";
        private readonly string CredentialPath = "Credentials/Credentials.json";

        public TasksService GetTasksService(){

            UserCredential credential;

            using (var stream = new FileStream(CredentialPath, FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new NullDataStore()).Result;

                return new TasksService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = AppName,
                });
            }
        }
        public string CriarTask(InformacoesProcessoTask jsonTaskInfo)
        {
            var service = GetTasksService();
            var taskList = service.Tasklists.List().Execute().Items?.FirstOrDefault();

            var newTask =  new Google.Apis.Tasks.v1.Data.Task
            {
                Title = jsonTaskInfo.Titulo,
                Position = jsonTaskInfo.LocalProcesso,
                Notes = jsonTaskInfo.Descricao,
                Due = DateTime.ParseExact(jsonTaskInfo.DataPrazo, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd'T'00:00:00.000'Z'")
            };

            var request = service.Tasks.Insert(newTask, taskList.Id);
            var createdTask = request.Execute();

            return $"Tarefa criada com ID: {createdTask.Id}";
        }
    }
}
