

namespace Filmstudion.API.Models.DTO
{
    public class CreateFilm:ICreateFilm
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string Director { get; set; }
        public int NumberOfCopies { get; set; }
    }
}
