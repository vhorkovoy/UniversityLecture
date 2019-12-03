using UniversityLecture.Core;

namespace UniversityLecture.WEB.Interfaces
{
    public interface IAuthenticate
    {
        string GetToken(string login, string pwd);
    }
}
