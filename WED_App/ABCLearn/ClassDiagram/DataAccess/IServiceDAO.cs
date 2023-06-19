namespace ABCLearn.DataAccess
{
    public interface IServiceDAO<T, P>
    {
        private static P instance;
        public static P Instance;
        public void GetAll();
        public bool AddNew(T obj);
        public bool Update(T obj);
        public void SaveChange();
        public bool Delete(int obj);
    }
}
