using Common.Extensions;
using fileserverapp.Models;
using fileserverapp.Services;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace fileserverapp.Controllers
{
        public class DefaultController : ApiController
    {
        private IFileServerService service;

        public DefaultController(IFileServerService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Get root
        /// </summary>
        /// <returns>Gets the root resource</returns>
        [HttpGet()]
        [Route("resources")]
        [ResponseType(typeof(IEnumerable<ResourceDto>))]
        public IHttpActionResult Get()
        {
            var resource = service.Get(null);

            return Ok(new List<ResourceDto> { new ResourceDto(resource) });
        }

        /// <summary>
        /// Get resource by id
        /// </summary>
        /// <param name="id">Unique identifier of the resource</param>
        /// <returns>Returns drive/folder/file resource</returns>
        [HttpGet()]
        [Route("resources/{id}")]
        [ResponseType(typeof(ResourceDto))]
        public IHttpActionResult Get(string id)
        {
            var resource = service.Get(id.FromBase64String());

            return Ok(new ResourceDto(resource));
        }

        /// <summary>
        /// Get children resources for the given resource id
        /// </summary>
        /// <param name="id">Unique identifier of the resource</param>
        /// <returns>Children resources underneath the given resource</returns>
        [HttpGet()]
        [Route("resources/{id}/children")]
        [ResponseType(typeof(IEnumerable<ResourceDto>))]
        public IHttpActionResult GetChildren(string id)
        {
            var children = service.GetChildren(id.FromBase64String());
            var result = new List<ResourceDto>();

            foreach (var item in children)
            {
                result.Add(new ResourceDto(item));
            }
            return Ok(result);
        }

        /// <summary>
        /// Copies the resource
        /// </summary>
        /// <param name="id">Unique identifier of the resource</param>
        /// <returns>Copied resource</returns>
        [HttpGet()]
        [Route("resources/{id}/copy")]
        [ResponseType(typeof(ResourceDto))]
        public IHttpActionResult Copy(string id)
        {
            var copiedResource = service.Copy(id.FromBase64String());           
            return Ok(new ResourceDto(copiedResource));
        }

        /// <summary>
        /// Renames the resource
        /// </summary>
        /// <param name="id">Unique identifier of the resource</param>
        /// <param name="name">new name of the resource</param>
        /// <returns>Updated resource</returns>
        [HttpPut()]
        [Route("resources/{id}/rename")]
        public ResourceDto Rename(string id, [FromBody] string name)
        {
            var renamedResource = service.Rename(id.FromBase64String(), name);
            return new ResourceDto(renamedResource);
        }

        /// <summary>
        /// Deletes the resource
        /// </summary>
        /// <param name="id">Unique identifier of the resource</param>
        [HttpDelete()]
        [Route("resources")]
        public IHttpActionResult Delete(string id)
        {
            service.Delete(id.FromBase64String());
            return Ok();
        }

        /// <summary>
        /// Downloads the resource
        /// </summary>
        /// <param name="id">Unique identifier of the resource</param>
        [HttpGet()]
        [Route("resources/{id}/download")]
        public HttpResponseMessage Download(string id)
        {
            var url = id.FromBase64String();
            var resource = service.Get(url);
            var result = new HttpResponseMessage(HttpStatusCode.OK);

            result.Content = new StreamContent(service.Download(url));
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = resource.Name
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return result;
        }

        /// <summary>
        /// Creates the resource
        /// </summary>
        /// <param name="id">Unique identifier of the resource</param>
        /// <param name="name">Name of new resource to create</param>
        [HttpPut()]
        [Route("resources/{id}/create")]
        [ResponseType(typeof(ResourceDto))]
        public IHttpActionResult Create(string id, [FromBody] string name)
        {
            var resource = service.Create(id.FromBase64String(), name);

            return Ok(new ResourceDto(resource));
        }

        /// <summary>
        /// Uploads the resource
        /// </summary>
        /// <param name="id">Unique identifier of the resource</param>
        /// <param name="name">Name of new resource to create</param>
        /// <param name="data">binary content of the resource</param>
        [HttpPost()]
        [Route("resources/{id}/upload")]
        [ResponseType(typeof(ResourceDto))]
        public async Task<IHttpActionResult> Upload()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest();
            }

            var provider = new MultipartMemoryStreamProvider();
            // путь к папке на сервере
            var id = Request.GetRouteData().Values["id"].ToString();
            await Request.Content.ReadAsMultipartAsync(provider);

            var file = provider.Contents[0];
            var name = file.Headers.ContentDisposition.FileName.Trim('\"');
            var data = await file.ReadAsByteArrayAsync();

            var resource = await service.UploadAsync(id.FromBase64String(), name, data);

            return Ok(new ResourceDto(resource));
        }

        /// <summary>
        /// Gets the metadata of the resource
        /// </summary>
        /// <param name="id">Unique identifier of the resource</param>
        [HttpGet()]
        [Route("resources/{id}/metadata")]
        [ResponseType(typeof(MetadataDto))]
        public IHttpActionResult Metadata(string id)
        {
            var metadata = service.GetMetadata(id.FromBase64String());

            return Ok(new MetadataDto(metadata));
        }
    }
}
