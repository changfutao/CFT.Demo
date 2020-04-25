using CFT.Demo.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFT.Demo.Admin.Data
{
    public class LibraryMockData
    {
        public static LibraryMockData Current { get; } = new LibraryMockData();
        public List<AuthorDto> Authors { get; set; }
        public List<BookDto> Books { get; set; }
        public LibraryMockData()
        {
            Authors = new List<AuthorDto>
            {
            new AuthorDto{ Id = Guid.NewGuid(),Name="cft",Age=23,Email="981384763@qq.com"},
            new AuthorDto{ Id = Guid.NewGuid(),Name="cft1",Age=27,Email="981384763@qq.com"}
            };

            Books = new List<BookDto> 
            {
                new BookDto{ Id ="" }
            };
        }
    }
}
