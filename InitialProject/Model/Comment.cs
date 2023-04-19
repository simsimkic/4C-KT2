using System;

namespace InitialProject.Model
{
    public class Comment
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public string Text { get; set; }

        public Comment() { }

        public Comment(int id, DateTime creationTime, string text, User user)
        {
            Id = id;
            CreationTime = creationTime;
            Text = text;
        }

        public Comment(DateTime creationTime, string text, User user)

        {
            Id = commentId;
            CreationTime = creationTime;
            Text = commentText;
        }

        public override string ToString()
        {
            return $"CommentID: {Id}\n, CreationTime: {CreationTime}\n, Text: {Text}\n";
        }


    }
}
