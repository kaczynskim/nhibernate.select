using System;

namespace Nhibernate.Select
{
    public class PostRecord
    {
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserName { get; set; }

        public PostRecord(string title, DateTime creationDate, string userName)
        {
            Title = title;
            CreationDate = creationDate;
            UserName = userName;
        }
    }

    public struct PostRecordStruct
    {
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserName { get; set; }

        public PostRecordStruct(string title, DateTime creationDate, string userName)
        {
            Title = title;
            CreationDate = creationDate;
            UserName = userName;
        }
    }
}
