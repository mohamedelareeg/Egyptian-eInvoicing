using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingBlocks.Results;
using MediatR;

namespace BuildingBlocks.Messaging;
public interface IListQuery<TItem> : IRequest<Result<CustomList<TItem>>>
{

}
