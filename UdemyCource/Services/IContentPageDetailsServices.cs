using System.Collections.Generic;
using UdemyCource.Models;

namespace UdemyCource.Services
{
    public interface IContentPageDetailsServices
    {
        void AddContent(PostContent postContent);
        void RemoveContent(int Id);
        IEnumerable<PostContent> PostContents { get; }
    }
}
