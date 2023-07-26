using Graphql_example_code.Application.Core;
using Graphql_example_code.Application.Core.Commands;
using Graphql_example_code.Application.Core.Results;
using Graphql_example_code.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphql_example_code.Application.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : CommandHandler<UpdateProductCommand, ResultT<Guid>>
    {
        private readonly IProduct _productRepository;
        public UpdateProductCommandHandler(IProduct productRepository)
            => this._productRepository = productRepository;

        public async override Task<ResultT<Guid>> ExecuteCommand(
            CommandHandlerResult<ResultT<Guid>> commandResult,
            UpdateProductCommand command, 
            CancellationToken cancellationToken = default)
        {
            if (commandResult.ValidationResult.IsValid)
            {
                try
                {
                    var product =
                    await _productRepository.GetProductByIdAsync(command._product.Id, cancellationToken);

                    if (product is null)
                        return Result.Fail<Guid>(Error.NotFound);

                    product.SetUpdate(
                        command._product.Title,
                        command._product.Description
                        );

                    var result =
                        await _productRepository.UpdateProductAsync(product, cancellationToken);

                    return Result.Ok(value: result);
                }
                catch(Exception ex)
                {
                    return Result.Fail<Guid>(Error.GetDatabaseError(ex.Message));
                }

            }
            else
            {
                var validationErrors = commandResult.ValidationResult.Errors.ToResultError();
                return Result.Fail<Guid>(validationErrors);
            }
        }
    }
}
