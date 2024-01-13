namespace Backend.DTOs
{
    public class PostDto
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public string? Description { get; set; }

        public string? Body { get; set; }
    }
}
