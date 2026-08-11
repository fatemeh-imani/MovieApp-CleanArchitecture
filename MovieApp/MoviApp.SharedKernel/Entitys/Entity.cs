namespace MoviApp.SharedKernel.Entitys
{
    public  abstract class Entity
    {
        public Guid Id { get; protected set; }
        public bool IsDelete { get;  set; }
        
        protected Entity(Guid id)
        {
            Id = id;
        }

        public void Delete()
        {
            IsDelete = true;

        }

        public void Restore()
        {
            IsDelete = false;
        }
    }
}
