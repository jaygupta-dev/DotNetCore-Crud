namespace DotNetCore_CRUD.Models
{
    public class FormModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public long MobileNumber { get; set; }
        public IFormFile? ProfilePath { get; set; }
        public string? ProfilePathStr { get; set; }
        public string? Gender { get; set; }
        public string? Qualification { get; set; }
        public string? Message { get; set; }
    }

}
