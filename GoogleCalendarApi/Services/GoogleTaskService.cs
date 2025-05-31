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
        private const string AppName = "GoogleCalendarApi";
        public TasksService GetTasksService(string accessToken)
        {

            var credential = GoogleCredential.FromAccessToken(accessToken);

            return new TasksService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = AppName,
            });

        }

        public string CriarTask(EventRequestTask req)
        {
            var service = GetTasksService(req.AccessToken);
            var taskList = service.Tasklists.List().Execute().Items?.FirstOrDefault();
            var info = req.Info;

            // Fix for CS1501: Convert DataPrazo to DateTime before formatting
            DateTime dataPrazoConvertida = DateTime.Parse(info.DataPrazo, CultureInfo.InvariantCulture);
            string dataConvertida = dataPrazoConvertida.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);

            var newTask = new Google.Apis.Tasks.v1.Data.Task
            {
                Title = info.Titulo,
                Notes = info.Descricao,
                Due = dataConvertida
            };

            var request = service.Tasks.Insert(newTask, taskList.Id);
            var createdTask = request.Execute();

            return $"Tarefa criada com ID: {createdTask.Id}";
        }
    }
}
