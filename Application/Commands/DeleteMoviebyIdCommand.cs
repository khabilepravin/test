using System.Threading;
using Application.Queries;
using DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace Application.Commands;

public class DeleteMoviebyIdCommand : IRequest<bool>
{
    public int Id { get; set; }

    public class Handler : IRequestHandler<DeleteMoviebyIdCommand, bool>
    {
        private readonly ApplicationDBContext _applicationDBContext;
        public Handler(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        async Task<bool> IRequestHandler<DeleteMoviebyIdCommand, bool>.Handle(DeleteMoviebyIdCommand request, CancellationToken cancellationToken)
        {
            var movie = await _applicationDBContext.Movies.FirstOrDefaultAsync(m => m.Id == request.Id);
            if (movie == null)
            { 
                return false;
            }
            _applicationDBContext.Movies.Remove(movie);
            await _applicationDBContext.SaveChangesAsync();
            return true;
        }
    }
}
