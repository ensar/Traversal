using MediatR;
using Traversal.CQRS.Results.GuideResults;

namespace Traversal.CQRS.Queries.GuideQueries
{
    public class GetGuideByIDQuery : IRequest<GetGuideByIDQueryResult>
    {
        public GetGuideByIDQuery(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
