using System.Threading.Tasks;

namespace SmartHome.Areas.Device.Controllers
{
    public interface IScenarioRunService
    {
        Task IterateScenarios();
    }
}