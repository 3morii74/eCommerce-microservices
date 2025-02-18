﻿using buildingBlocks.CQRS;
using Catalog.API.Models;

namespace Catalog.API.Products.CreateProducts
{
    public class CreateProductHandler
    {
        public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<CreateProductResult>;
        public record CreateProductResult(Guid Id);

        internal class CreateProductCommandHandler
            : ICommandHandler<CreateProductCommand, CreateProductResult>
        {
            public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
            {
                //create Product entity from command object
                //save to database
                //return CreateProductResult result

                var product = new Product
                {
                    Name = command.Name,
                    Category = command.Category,
                    Description = command.Description,
                    ImageFile = command.ImageFile,
                    Price = command.Price
                };

                // TODO
                //save to database
                //return result

                return new CreateProductResult(Guid.NewGuid());
            }
        }
    }
}