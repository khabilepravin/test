using DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace Application.Queries
{
    public class GetMovieByIdQuery : IRequest<Movie>
    {

        public int Id { get; set; }

        public class Handler : IRequestHandler<GetMovieByIdQuery, Movie>
        {
            private readonly ApplicationDBContext _applicationDBContext;
            public Handler(ApplicationDBContext applicationDBContext)
            {
                _applicationDBContext = applicationDBContext;
            }
            async Task<Movie> IRequestHandler<GetMovieByIdQuery, Movie>.Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
            {
                return await _applicationDBContext.Movies.FirstOrDefaultAsync(m => m.Id == request.Id);
            }
        }


    }
}
