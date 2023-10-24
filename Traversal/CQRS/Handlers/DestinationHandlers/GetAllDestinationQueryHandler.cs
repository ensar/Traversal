using DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Traversal.CQRS.Queries.DestinationQueries;
using Traversal.CQRS.Results.DestinationResults;

namespace Traversal.CQRS.Handlers.DestinationHandlers
{
    public class GetAllDestinationQueryHandler
    {
        private readonly Context _context;

        public GetAllDestinationQueryHandler(Context context)
        {
            _context = context;
        }
        public List<GetAllDestinationQueryResult> Handle()
        {
            var values = _context.Destinations.Select(d => new GetAllDestinationQueryResult
            {
                id = d.DestinationID,
                capacity = d.Capacity,
                city = d.City,
                daynight = d.DayNight,
                price = d.Price
            }).AsNoTracking().ToList();
            return values;
        }
    }
}
