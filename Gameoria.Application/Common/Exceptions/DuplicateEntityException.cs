
//DuplicateEntityException: عند محاولة إنشاء كيان موجود مسبقاً
namespace GameOria.Application.Common.Exceptions
{
    public class DuplicateEntityException : Exception
    {
        public DuplicateEntityException()
            : base("Entity already exists.")
        {
        }

        public DuplicateEntityException(string name, object key)
            : base($"Entity \"{name}\" ({key}) already exists.")
        {
        }

        public DuplicateEntityException(string message)
            : base(message)
        {
        }

        public DuplicateEntityException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
