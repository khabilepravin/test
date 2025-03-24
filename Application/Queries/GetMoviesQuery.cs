using DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace Application.Queries;

public  class GetMoviesQuery  : IRequest<List<Movie>>
{

    public class Handler : IRequestHandler<GetMoviesQuery, List<Movie>>
    {
        private readonly ApplicationDBContext _applicationDBContext;
        public Handler(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        public async Task<List<Movie>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
        {
            return await _applicationDBContext.Movies.ToListAsync();
        }
    }
}
