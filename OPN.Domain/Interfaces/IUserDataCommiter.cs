using OPN.Domain.Login;

namespace OPN.Domain.Interfaces
{
    public interface IUserDataCommiter
    {
        LoggedUser RegisterNewUser(string iDN, string id);
        public void AddTaskToUser(int userId, string taskGoal, int taskId);
        void CompleteTaskFromUser(int userId, int? taskId, int completedTasksFromUser);
        void CancelTaskFromUser(int iD, int? taskId);
    }
}
