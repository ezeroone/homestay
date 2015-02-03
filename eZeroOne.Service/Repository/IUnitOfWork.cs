namespace eZeroOne.Service.Repository
{
    /// <summary>
    /// Provides an implementation of the UnitOfWork Pattern.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Commit the In-memory changes of the DbContext to the backend database.
        /// </summary>
        void Commit();
    }
}