//ForbiddenAccessException: عندما لا يملك المستخدم الصلاحيات المطلوبة

namespace GameOria.Application.Common.Exceptions
{
    public class ForbiddenAccessException : Exception
    {
        public ForbiddenAccessException() : base("Access to this resource is forbidden.") { }
    }
}
