using System;
using eZeroOne.Domain;

namespace eZeroOne.Service.Repository
{
    /// <summary>
    /// Default implementation of the IUnitOfWork
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly eResortsEntities context;
        public UnitOfWork(eResortsEntities context)
        {
            this.context = context;
        }

        protected eResortsEntities Context
        {
            get
            {
                return this.context;
            }
        }

        /// <summary>
        /// Commit the In-memory changes of the DbContext to the backend database.
        /// </summary>
        public void Commit()
        {
            if (this.context == null) throw new NullReferenceException("context");

            this.context.SaveChanges();
        }
    }
}