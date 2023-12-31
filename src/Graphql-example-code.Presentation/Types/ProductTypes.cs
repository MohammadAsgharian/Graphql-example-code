﻿using MediatR;
using Graphql_example_code.Domain;
using Graphql_example_code.Application.Queries;
using Graphql_example_code.Application.Core.Results;


namespace Graphql_example_code.Presentation.Types;
public class ProductTypes
{
    public async Task<ResultT<List<Product>>> GetProductListAsync(
        [Service] IMediator mediator)
        => await mediator.Send(new GetProductQuery());
    public async Task<ResultT<Product>> GetProductByIdListAsync(
        [Service] IMediator mediator,
        Guid Id)
        => await mediator.Send(new GetProductByIdQuery(Id));
}
