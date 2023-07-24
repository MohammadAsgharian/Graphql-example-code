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
    public class CreateProductCommandHandler : CommandHandler<CreateProductCommand, ResultT<bool>>
    {
        private readonly IProduct _productRepository;
        public CreateProductCommandHandler(IProduct productRepository)
            => this._productRepository = productRepository;

        public async override Task<ResultT<bool>> ExecuteCommand(
            CommandHandlerResult<ResultT<bool>> commandResult,
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

                    return new ResultT<bool>(
                        value: result,
                        isSuccess: true,
                        errors: Error.None);
                }
                catch (Exception ex)
                {
                    return new ResultT<bool>(
                        value: false,
                        isSuccess: false,
                        errors: Error.GetDatabaseError(ex.Message));
                }
            }
            else
            {
                var result = commandResult.ValidationResult.Errors.ToResultError();
                return new ResultT<bool>(
                    value: false,
                    isSuccess: false,
                    errors: result);
            }
        }
    }
}
