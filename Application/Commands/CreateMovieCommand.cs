using DataAccess;
using MediatR;
using WebApi.Models;

namespace Application.Commands
{
    public class CreateMovieCommand : IRequest<CreateMovieCmdResponse>
    {

        public string Title { get; set; }
        public string Director { get; set; }
        public int Year { get; set; }

        public class Handler : IRequestHandler<CreateMovieCommand, CreateMovieCmdResponse>
        {

            private readonly ApplicationDBContext _applicationDBContext;

            public Handler(ApplicationDBContext applicationDBContext)
            {
                _applicationDBContext = applicationDBContext;
            }

            public async Task<CreateMovieCmdResponse> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
            {

                var existingMovieRecord = _applicationDBContext.Movies.FirstOrDefault(m => m.Title == request.Title);
                if (existingMovieRecord is not null)
                {
                    return new CreateMovieCmdResponse { AddedMovie = null, MovieExists = true };
                }

                var movie = new Movie
                {

                    Title = request.Title,
                    Director = request.Director,
                    Year = request.Year
                };

                var result = await _applicationDBContext.Movies.AddAsync(movie);
                _applicationDBContext.SaveChanges();

                return new CreateMovieCmdResponse { AddedMovie = result.Entity, MovieExists = false };

            }
        }


    }

    public class CreateMovieCmdResponse
    {

        public bool MovieExists { get; set; }
        public Movie AddedMovie { get; set; }


    }
}
