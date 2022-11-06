﻿using OPN.Domain.Interfaces;
using OPN.Domain.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPN.Domain.Login
{
    public class LoggedUser
    {
        public int ID { get; set; }
        public string IDN { get; set; }
        public string TaskGoal { get; set; }
        public int? TaskId { get; set; }
        public IUserDataCommiter _userDataCommiter { get; set; }

        public LoggedUser() { }
        public LoggedUser(IUserDataCommiter userDataCommiter)
        {
            _userDataCommiter = userDataCommiter;
        }

        public void AddTask(OPNTask task)
        {
            TaskId = task.Id;
            TaskGoal = task.Goal;
            _userDataCommiter.AddTaskToUser(ID, task.Goal, task.Id);
        }

        public void CompleteTask()
        {
            if (TaskGoal == null)
                throw new Exception("Este IDN não possui uma task ativa!");

            _userDataCommiter.CompleteTaskFromUser(ID, TaskId);

            TaskId = null;
            TaskGoal = null;
        }

        public void CancelTask()
        {
            if (TaskGoal == null)
                throw new Exception("Este IDN não possui uma task ativa!");

            _userDataCommiter.CancelTaskFromUser(ID, TaskId);

            TaskId = null;
            TaskGoal = null;

        }
    }
}
