using GoogleCalendarApi.Models;

namespace GoogleCalendarApi.Interfaces
{
    public interface IGoogleTaskService
    {
        string CriarTask(InformacoesProcessoTask jsonInfo);
    }
}
