namespace IntravisionTest.Interface
{
    public interface IImportService
    {
        Task<int> ImportDrinksAsync(IFormFile file, CancellationToken cancellationToken);
    }
}
