namespace DotnetAPI.Data
{
    public interface IUserRepository
    {
        public bool SaveChanges();
        public void AddEntity<T>(T entityAdd);
        public void RemoveEntity<T>(T entityToRemove);
    }
}