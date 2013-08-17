using CodeJewels.DataLayer;
using CodeJewels.Services.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CodeJewels.Services.Controllers
{
    public class CodeJewelsController : ApiController
    {
        private DbContext context;
        private DbSet<CodeJewel> entitySet;

        public CodeJewelsController()
        {
            this.context = new CodeJewelsDBEntities();
            this.entitySet = context.Set<CodeJewel>();
        }

        // GET api/codejewels
        public IEnumerable<CodeJewelsModel> Get()
        {
            var codeJewels = from code in this.entitySet
                             select new CodeJewelsModel()
                             {
                                 Id = code.Id,
                                 SourceCode = code.SourceCode,
                                 AuthorEmail = code.AuthorEmail,
                                 Rating = code.Rating,
                                 CodeLanguage = code.Category.CodeLanguage
                             };
            return codeJewels;
        }

        // GET api/codejewels/5
        [ActionName("get")]
        public IEnumerable<CodeJewelsModel> GetBySourceAndCategory(string source, string category)
        {
            var codeJewels = entitySet.Where(x => x.SourceCode.Contains(source) &&
                x.Category.CodeLanguage.ToLower() == category.ToLower())
                    .Select(new Func<CodeJewel, CodeJewelsModel>(x => new CodeJewelsModel()
                    {
                        Id = x.Id,
                        SourceCode = x.SourceCode,
                        AuthorEmail = x.AuthorEmail,
                        Rating = x.Rating,
                        CodeLanguage = x.Category.CodeLanguage
                    })).ToList();

            return codeJewels;
        }

        // POST api/codejewels
        public HttpResponseMessage Post([FromBody]CodeJewelsModel value)
        {
            var codeJewel = new CodeJewel()
            {
                SourceCode = value.SourceCode,
                AuthorEmail = value.AuthorEmail,
                Rating = value.Rating
            };

            if (value.CodeLanguage != null)
            {
                var category = new Category();
                category.CodeLanguage = value.CodeLanguage;
                category.CodeJewels.Add(codeJewel);
                var entytiCategories = context.Set<Category>();
                entytiCategories.Add(category);
            }
            else
            {
                this.entitySet.Add(codeJewel);
            }
            this.context.SaveChanges();
            CodeJewelsModel result = new CodeJewelsModel()
            {
                Id = codeJewel.Id,
                SourceCode = codeJewel.SourceCode,
                AuthorEmail = codeJewel.AuthorEmail,
                Rating = codeJewel.Rating,
                CodeLanguage = value.CodeLanguage
            };

            var response = Request.CreateResponse(HttpStatusCode.Created, result);
            return response;
        }

        // PUT api/codejewels/5
        public void Put(int id, string vote)
        {
            var codeJewel = entitySet.FirstOrDefault(x => x.Id == id);
            if (vote == "plus")
            {
                codeJewel.Rating++;
            }
            else if (vote == "minus")
            {
                codeJewel.Rating--;
            }
            
            context.SaveChanges();
        }

        // DELETE api/codejewels/5
        [ActionName("delete")]
        public void DeleteLowScores()
        {
            var minRating = entitySet.Min(x => x.Rating);
            var codeJewelsMinRating = entitySet.Where(c => c.Rating == minRating);
            foreach (var min in codeJewelsMinRating)
            {
                entitySet.Remove(min);
            }

            context.SaveChanges();
        }
    }
}
