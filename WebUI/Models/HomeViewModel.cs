using Application.Services.HomeServices.Dtos;

namespace WebUI.Models
{
    public class HomeViewModel
    {
        public List<HomeNewsDto> HotNews { get; set; } = null!;
        public List<HomeNewsDto> LastNews { get; set; } = null!;
        public List<HomeNewsDto> MostVisited { get; set; } = null!;
        public List<HomeNewsDto> RandomNews { get; set; } = null!;
    }
}
