using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using UdemyCource.Models;

namespace UdemyCource.Services
{
    public class MemoryContentPageDetailsRepository : IContentPageDetailsServices
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        public MemoryContentPageDetailsRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        #region inti Data For Post Content

        public List<PostContent> _contentPageDetailsData = new List<PostContent>
        {

                new PostContent()
                {
                    Id = 1,
                    Title= "Learning ASP.NET Core Identity",
                    Details = "This Cource Explain For you An advanced topics about ASP.NET Core Identity",
                    AddedDate = new DateTime(2023, 1, 1)
                },

                new PostContent()
                {
                    Id = 2,
                    Title= "Claims in ASP.NET Core Identity",
                    Details = "This Cource Explain For you An advanced topics about ASP.NET Core Identity",
                    AddedDate = new DateTime(2023, 1, 1)
                },

                new PostContent()
                {
                    Id = 3,
                    Title= "2FA ASP.NET Core Identity",
                    Details = "This Cource Explain For you An advanced topics about ASP.NET Core Identity",
                    AddedDate = new DateTime(2023, 1, 1)
                },

                new PostContent()
                {
                    Id = 4,
                    Title= "Authenticate By Google Authenticator in ASP.NET Core Identity",
                    Details = "This Cource Explain For you An advanced topics about ASP.NET Core Identity",
                    AddedDate = new DateTime(2023, 1, 1)
                },

                new PostContent()
                {
                    Id = 5,
                    Title= "Securing Web APIs By using ASP.NET Core Identity",
                    Details = "This Cource Explain For you An advanced topics about ASP.NET Core Identity",
                    AddedDate = new DateTime(2023, 1, 1)
                },

        };
        #endregion

        public IEnumerable<PostContent> PostContents => _contentPageDetailsData;

        public void AddContent(PostContent postContent)
        {
            postContent.Id = _contentPageDetailsData.Max(x => x.Id) + 1;
            postContent.AddedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            postContent.AddedDate = DateTime.Now;
            _contentPageDetailsData.Add(postContent);
        }

        public void RemoveContent(int Id)
        {
            var postContent = _contentPageDetailsData.Where(x => x.Id == Id).FirstOrDefault();
            _contentPageDetailsData.Remove(postContent);
        }
    }
}
