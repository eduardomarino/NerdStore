namespace NerdStore.Core.Data;

public interface IUnitOfWork
{
    // Returns true if database operations are successful
    Task<bool> Commit();
}
