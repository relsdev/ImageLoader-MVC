using ImageLoader.Data;
using ImageLoader.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace ImageLoader
{
    public class ImageController : ApiController
    {
        //Add dependency injection (constructor)
        private readonly IUnitOfWork uow = null;
        private readonly IRepository<Domain.Image> repository = null;

        public ImageController()
        {
            uow = new UnitOfWork();
            repository = new Repository<Domain.Image>(uow);
        }
        // GET api/<controller>
        public IEnumerable<Image> Get()
        {          
            return repository.All.ToList();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string urlValue)
        {
            
        }

        // PUT api/<controller>/5
        [HttpPut]
        public long Put([FromBody] Image image)
        {
            //TODO: Add exception handling here
           var newImage = new Image { Domain = (new Uri(image.Url)).Host, Url = image.Url };
           repository.Insert(newImage);
           uow.Save();

            return newImage.Id;          
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            //TODO: Add exception handling here
            repository.Delete(id);
            uow.Save();
        }
    }
}