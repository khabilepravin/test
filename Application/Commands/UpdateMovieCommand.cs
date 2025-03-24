using DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands;

public class UpdateMovieCommand : IRequest<bool>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Director { get; set; }
    public int Year { get; set; }

    public class Handler : IRequestHandler<UpdateMovieCommand, bool>
    {
        private readonly ApplicationDBContext _applicationDBContext;
        public Handler(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        public async Task<bool> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {

       var movie = await _applicationDBContext.Movies.FirstOrDefaultAsync(m => m.Id == request.Id);

            if (movie == null)
            {
                return false;
            }

            movie.Title = request.Title;
            movie.Director = request.Director;
            movie.Year = request.Year;;

            await _applicationDBContext.SaveChangesAsync();
            return true;

        }


    }
}
