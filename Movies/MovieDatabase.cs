using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Movies
{
    /// <summary>
    /// A class representing a database of movies
    /// </summary>
    public static class MovieDatabase
    {
        private static List<Movie> movies;

        /// <summary>
        /// Loads the movie database from the JSON file
        /// </summary>

        public static List<Movie> All { get {
                if (movies == null)
                {
                    using (StreamReader file = System.IO.File.OpenText("movies.json"))
                    {
                        string json = file.ReadToEnd();
                        movies = JsonConvert.DeserializeObject<List<Movie>>(json);
                    }
                }
                return movies;
            }
        }

        public static List<Movie> Search(string term, List<Movie> mov)
        {
            List<Movie> result = new List<Movie>();

            foreach (Movie movie in mov)
            {
                if (movie.Title.Contains(term, StringComparison.OrdinalIgnoreCase)
                    || (movie.Director != null && movie.Director.Contains(term, StringComparison.OrdinalIgnoreCase)))
                {
                    result.Add(movie);
                }
            }
            return result;
        }

        public static List<Movie> FilterByMPAA(List<Movie> movies, List<string> mpaa)
        {
            List<Movie> result = new List<Movie>();

            foreach(Movie movie in movies)
            {
                if (mpaa.Contains(movie.MPAA_Rating))
                {
                    result.Add(movie);
                }
            }

            return result;
        }

        public static List<Movie> FilterByMinIMDB(List<Movie> movies, float min)
        {
            List<Movie> result = new List<Movie>();

            foreach(Movie movie in movies)
            {
                if (movie.IMDB_Rating != null && movie.IMDB_Rating >= min)
                {
                    result.Add(movie);
                }
            }

            return result;
        }
    }
}
