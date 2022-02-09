﻿

namespace Filmstudion.API.Models.CRUD
{
    public interface ICreateFilm
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string Director { get; set; }
        public int NumberOfCopies { get; set; }
    }
}
