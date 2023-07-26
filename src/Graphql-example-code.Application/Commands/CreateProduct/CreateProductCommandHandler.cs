using Graphql_example_code.Application.Core;
using Graphql_example_code.Application.Core.Commands;
using Graphql_example_code.Application.Core.Results;
using Graphql_example_code.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphql_example_code.Application.Commands.CreateProduct
{
    public class CreateProductCommandHandler : 
        CommandHandler<CreateProductCommand, ResultT<Guid>>
    {
        private readonly IProduct _productRepository;
        public CreateProductCommandHandler(IProduct productRepository)
            => this._productRepository = productRepository;

        public async override Task<ResultT<Guid>> ExecuteCommand(
            CommandHandlerResult<ResultT<Guid>> commandResult,
            CreateProductCommand command, CancellationToken cancellationToken = default)
        {
            if (commandResult.ValidationResult.IsValid)
            {
                try
                {
                    var newProduct = Product.CreateNew(
                        command._product.Title,
                        command._product.Description);

                    var result =
                        await _productRepository.AddProductAsync(newProduct, cancellationToken);

                    return Result.Ok(result);
                }
                catch (Exception ex)
                {
                    return Result.Fail<Guid>(Error.GetDatabaseError(ex.Message));
                }
            }
            else
            {
                var validationErrors = 
                    commandResult.ValidationResult.Errors.ToResultError();

                return Result.Fail<Guid>(validationErrors);
            }
        }
    }
}
