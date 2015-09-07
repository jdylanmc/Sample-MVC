using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample.Domain.Model.Business
{
    public class Todo
    {
        public Todo()
        { }

        public Todo(string description, string title)
        {
            this.Title = title;
            this.Description = description;
        }

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Column(TypeName = "NVARCHAR(MAX)")]
        [MaxLength]
        public string Description { get; set; }
    }
}
