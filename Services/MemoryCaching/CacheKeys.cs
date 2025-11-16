namespace QLPhongKham.API.Services.MemoryCaching
{
    public static class CacheKeys
    {
        private const string BenhNhanPrefix = "benhnhan";

        // Cache time constants
        public static class Expiration
        {
            public static readonly TimeSpan Short = TimeSpan.FromMinutes(5);
            public static readonly TimeSpan Medium = TimeSpan.FromMinutes(30);
            public static readonly TimeSpan Long = TimeSpan.FromHours(2);
            public static readonly TimeSpan VeryLong = TimeSpan.FromHours(24);
        }

        // Bệnh nhân cache keys
        public static class BenhNhan
        {
            public static string GetAll() => $"{BenhNhanPrefix}:all";
            public static string GetById(int id) => $"{BenhNhanPrefix}:id:{id}";
            public static string GetByMaBenhNhan(string maBenhNhan) => $"{BenhNhanPrefix}:ma:{maBenhNhan}";
            public static string Pattern() => $"{BenhNhanPrefix}:*";
        }
    }
}
