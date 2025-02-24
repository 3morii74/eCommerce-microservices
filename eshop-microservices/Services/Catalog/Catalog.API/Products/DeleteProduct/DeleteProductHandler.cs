namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccess);

    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product ID is required");
        }
    }

    internal class DeleteProductCommandHandler
        (IDocumentSession session, ILogger<DeleteProductCommandHandler> logger)
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            // Log the incoming command
            logger.LogInformation("DeleteProductCommandHandler.Handle called with {@Command}", command);

            // Validate the command
            if (command.Id == Guid.Empty)
            {
                logger.LogError("Invalid product ID provided.");
                return new DeleteProductResult(false); // Return false as the deletion cannot proceed
            }

            // Check if the product exists in the session
            var product = await session.Query<Product>().FirstOrDefaultAsync(p => p.Id == command.Id, cancellationToken);
            if (product == null)
            {
                logger.LogError("Product with ID {ProductId} not found.", command.Id);
                return new DeleteProductResult(false); // Return false as the product doesn't exist
            }

            // Proceed with deletion
            session.Delete<Product>(command.Id);
            await session.SaveChangesAsync(cancellationToken);

            // Return success
            return new DeleteProductResult(true);
        }
    }
}