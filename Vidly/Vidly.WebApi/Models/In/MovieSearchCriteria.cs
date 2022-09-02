using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vidly.WebApi.Domain;

namespace Vidly.WebApi.Models.In
{
    public class MovieSearchCriteria
    {
        public string? Title { get; set; }
        public string? Description { get; set; }

        public bool Criteria(Movie movie)
        {
            return FilterByTitle(movie) && FilterByDescription(movie);
        }

        private bool FilterByTitle(Movie movie)
        {
            if (string.IsNullOrEmpty(Title))
            {
                return true;
            }
            else
            {
                return movie.Title == Title;
            }
        }

        private bool FilterByDescription(Movie movie)
        {
            if (string.IsNullOrEmpty(Description))
            {
                return true;
            }
            else
            {
                return movie.Description == Description;
            }
        }
    }
}
