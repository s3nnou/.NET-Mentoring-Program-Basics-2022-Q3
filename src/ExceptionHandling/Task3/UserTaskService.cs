using System;
using Task3.DoNotChange;

namespace Task3
{
    public class UserTaskService
    {
        private const int InvalidUserIdError = -1;
        private const int UserNotFoundError = -2;
        private const int DuplicateTaskError = -3;
        private readonly IUserDao _userDao;

        public UserTaskService(IUserDao userDao)
        {
            _userDao = userDao;
        }

        public void AddTaskForUser(int userId, UserTask task)
        {
            if (userId < 0)
                MapError(InvalidUserIdError);

            var user = _userDao.GetUser(userId);

            if (user == null)
                MapError(UserNotFoundError);

            var tasks = user.Tasks;
            foreach (var t in tasks)
            {
                if (string.Equals(task.Description, t.Description, StringComparison.OrdinalIgnoreCase))
                    MapError(DuplicateTaskError);
            }

            tasks.Add(task);
        }

        private void MapError(int statusCode)
        {
            throw statusCode switch
            {
                -1 => new UserTaskServiceException("Invalid userId"),
                -2 => new UserTaskServiceException("User not found"),
                -3 => new UserTaskServiceException("The task already exists"),
                _ => new UserTaskServiceException("Unknown error"),
            };
        }
    }
}