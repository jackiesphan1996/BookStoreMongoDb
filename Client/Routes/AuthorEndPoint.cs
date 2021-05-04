namespace BookStoreMongoDb.Client.Routes
{
    public static class AuthorEndPoint
    {
        public static string GetAll(int page, int pageSize, string search) => $"api/author?page={page}&pageSize={pageSize}&search={search}";
        public static string Create = "api/author";
        public static string Update = "api/author";
        public static string Delete(int id) => $"api/author/{id}";
    }
}
