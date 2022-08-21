using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.HomeServices.Dtos
{
    public class HomeNewsDto
    {
        public int Id { get; set; }

        public string Slug { get; set; } = null!;

        public string Image { get; set; } = null!;

        public string MetaDescription { get; set; } = null!;
        public string Title { get; set; } = null!;

    }
}
