using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infrastructure.InfrastructureServices.ImageService
{
    public interface IImageService
    {
        Task<ImageAddressDto> ExecuteAsync(IFormFileCollection Files);

        Task<string> CompserImage(string Source);
    }

    public class ImageService : IImageService
    {
        private readonly IConfiguration configuration;
        private RestClient client;
        public ImageService(IConfiguration configuration)
        {
            client = new RestClient();
            client.Options.Timeout = -1;
            client.Options.BaseUrl = new Uri(configuration["StaticFilesAddress:Images"]);
            this.configuration = configuration;
        }

        public Task<string> CompserImage(string Source)
        {
            var add = configuration["StaticFilesAddress:Images"];
            return Task.FromResult(add + Source);
        }

        public async Task<ImageAddressDto> ExecuteAsync(IFormFileCollection Files)
        {
            var resource = "api/ImageUploader/ImageUploader";

            var request = new RestRequest(resource, Method.Post);

            foreach (var file in Files)
            {
                byte[] bytes;
                using(var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    bytes = ms.ToArray();
                }

                request.AddFile(file.FileName, bytes, file.FileName);

            }

            var response = await client.ExecuteAsync(request);

            var result = JsonConvert.DeserializeObject<ImageAddressDto>(response.Content);

            return result;
        }
    }

    public class ImageAddressDto
    {
        public List<string> Address { get; set; } = null!;
    }


}
