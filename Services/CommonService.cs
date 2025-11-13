using Microsoft.EntityFrameworkCore;
using QLPhongKham.API.Models.Entities;
using System.Text;

namespace QLPhongKham.API.Services
{
    public class CommonService
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }

        //Tạo mã OTP
        public static string GenerateOTP(int length = 6)
        {
            var random = new Random();
            var otp = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                otp.Append(random.Next(0, 10));
            }
            return otp.ToString();
        }

        // Tạo mã bảng
        public static string LuuMaQL(string prefix, string tableName, PhongKhamDBContext context)
        {
            if (string.IsNullOrEmpty(prefix))
                throw new ArgumentException("Tiền tố mã không được để trống.", nameof(prefix));
            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentException("Tên bảng không được để trống.", nameof(tableName));

            // Lấy entity type tương ứng với tên bảng
            var entityType = context.Model.GetEntityTypes()
                .FirstOrDefault(e => e.GetTableName()?.Equals(tableName, StringComparison.OrdinalIgnoreCase) == true);

            if (entityType == null)
                throw new ArgumentException($"Không tìm thấy bảng '{tableName}' trong DbContext.");

            // Tìm thuộc tính có tên bắt đầu bằng "Ma"
            var propMa = entityType.ClrType.GetProperties()
                .FirstOrDefault(p => p.Name.StartsWith("Ma", StringComparison.OrdinalIgnoreCase));

            if (propMa == null)
                throw new Exception($"Không tìm thấy cột có tên bắt đầu bằng 'Ma' trong bảng '{tableName}'.");

            int? maxLength = entityType.FindProperty(propMa.Name)?.GetMaxLength();

            // Lấy dữ liệu bảng
            var setMethod = typeof(DbContext).GetMethod("Set", Type.EmptyTypes);
            var genericSetMethod = setMethod.MakeGenericMethod(entityType.ClrType);
            var dbSet = genericSetMethod.Invoke(context, null) as IQueryable<object>;

            var dataList = dbSet.AsEnumerable();
            string? lastCode = dataList
                .Select(x => propMa.GetValue(x)?.ToString())
                .Where(x => !string.IsNullOrEmpty(x) && x.StartsWith(prefix))
                .OrderByDescending(x => x)
                .FirstOrDefault();

            int number = 0;
            if (!string.IsNullOrEmpty(lastCode))
            {
                var numericPart = lastCode.Substring(prefix.Length);
                int.TryParse(numericPart, out number);
            }

            number++;

            int numericLength = 4;
            if (maxLength.HasValue)
            {
                int available = maxLength.Value - prefix.Length;
                numericLength = Math.Max(1, Math.Min(available, 6));
            }

            string newCode = $"{prefix}{number.ToString($"D{numericLength}")}";

            if (maxLength.HasValue && newCode.Length > maxLength.Value)
            {
                newCode = newCode.Substring(0, maxLength.Value);
            }

            return newCode;
        }

    }
}
