using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Connection;
using DataAccess.Fabric;
using DataAccess.Models;
using TasksRegistrationManager.Models;

namespace TasksRegistrationManager.Controllers
{
    public class TaskController : Controller
    {
        private readonly IDbConnectionManager _manager;

        public TaskController(IDbConnectionManager dbConnectionManager)
        {
            _manager = dbConnectionManager;
            _manager.Initialaize();
        }

        public ActionResult Index()
        {

            var res = new List<TaskView>();


            var sqlCmd = _manager.CreateCommand();
            sqlCmd.CommandText = SqlQueryBuilder.PrepareSqlQuery(EntityQueryType.Select, new Task(), null);
            _manager.OpenConnection();
            using (DbDataReader dr = sqlCmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    res.Add(new TaskView
                    {
                        TaskId = (int)dr["TaskId"],
                        Name = (string)dr["Name"],
                        Workload = (string)dr["Workload"],
                        StartDate = (DateTime)dr["StartDate"],
                        EndDate = (DateTime)dr["EndDate"],
                        TaskStateId = (int)dr["TaskStateId"],
                        PersonId = (int)dr["PersonId"]
                    });
                }
            }
            foreach (var item in res)
            {
                sqlCmd.CommandText = SqlQueryBuilder.PrepareSqlQuery(EntityQueryType.Select, new TaskState(),
                    "WHERE TaskStateId=" + item.TaskStateId);
                using (DbDataReader dr = sqlCmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        item.State = new TaskState
                        {
                            TaskStateId = (int)dr["TaskStateId"],
                            Name = (string)dr["Name"]
                        };
                    }
                }

                sqlCmd.CommandText = SqlQueryBuilder.PrepareSqlQuery(EntityQueryType.Select, new Person(),
                    "WHERE PersonId=" + item.PersonId);
                using (DbDataReader dr = sqlCmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        item.Person = new Person
                        {
                            PersonId = (int)dr["PersonId"],
                            FirstName = (string)dr["FirstName"],
                            LastName = (string)dr["LastName"],
                            MiddleName = (string)dr["MiddleName"]
                        };
                    }
                }

            }
            _manager.CloseConnection();

            return View(res);
        }

        public ActionResult Create()
        {
            var allPersons = new List<Person>();
            var allTaskStates = new List<TaskState>();


            var sqlCmd = _manager.CreateCommand();
            sqlCmd.CommandText = SqlQueryBuilder.PrepareSqlQuery(EntityQueryType.Select, new TaskState(), null);
            _manager.OpenConnection();
            using (DbDataReader dr = sqlCmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    allTaskStates.Add(new TaskState
                    {
                        TaskStateId = (int)dr["TaskStateId"],
                        Name = (string)dr["Name"]
                    });
                }
            }

            sqlCmd.CommandText = SqlQueryBuilder.PrepareSqlQuery(EntityQueryType.Select, new Person(), null);
            using (DbDataReader dr = sqlCmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    allPersons.Add(new Person
                    {
                        PersonId = (int)dr["PersonId"],
                        FirstName = (string)dr["FirstName"],
                        LastName = (string)dr["LastName"],
                        MiddleName = (string)dr["MiddleName"]
                    });
                }
            }

            ViewBag.Persons = allPersons;
            ViewBag.AllTaskStates = allTaskStates;

            return View();
        }

        [HttpPost]
        public ActionResult Create(Task task)
        {
            var sqlCmd = _manager.CreateCommand();
            sqlCmd.CommandText = SqlQueryBuilder.PrepareSqlQuery(EntityQueryType.Insert, task, null);
            _manager.OpenConnection();
            if (sqlCmd.ExecuteNonQuery() > 0)
            {
                _manager.CloseConnection();
                return RedirectToAction("Index", "Task");
            }
            


            _manager.CloseConnection();

            return View(task);
        }

        public ActionResult Update(int id)
        {
            Task task;
            var allPersons = new List<Person>();
            var allTaskStates = new List<TaskState>();

            var sqlCmd = _manager.CreateCommand();
            sqlCmd.CommandText = SqlQueryBuilder.PrepareSqlQuery(EntityQueryType.Select, new Task(), " WHERE TaskId=" + id);
            _manager.OpenConnection();
            using (DbDataReader dr = sqlCmd.ExecuteReader())
            {
                dr.Read();

                task = new Task
                {
                    TaskId = (int)dr["TaskId"],
                    Name = (string)dr["Name"],
                    Workload = (string)dr["Workload"],
                    StartDate = (DateTime)dr["StartDate"],
                    EndDate = (DateTime)dr["EndDate"],
                    TaskStateId = (int)dr["TaskStateId"],
                    PersonId = (int)dr["PersonId"]
                };
            }
            sqlCmd.CommandText = SqlQueryBuilder.PrepareSqlQuery(EntityQueryType.Select, new TaskState(), null);
            using (DbDataReader dr = sqlCmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    allTaskStates.Add(new TaskState
                    {
                        TaskStateId = (int)dr["TaskStateId"],
                        Name = (string)dr["Name"]
                    });
                }
            }

            sqlCmd.CommandText = SqlQueryBuilder.PrepareSqlQuery(EntityQueryType.Select, new Person(), null);
            using (DbDataReader dr = sqlCmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    allPersons.Add(new Person
                    {
                        PersonId = (int)dr["PersonId"],
                        FirstName = (string)dr["FirstName"],
                        LastName = (string)dr["LastName"],
                        MiddleName = (string)dr["MiddleName"]
                    });
                }
            }
            _manager.CloseConnection();

            ViewBag.Persons = allPersons;
            ViewBag.AllTaskStates = allTaskStates;

            return View(task);
        }

        [HttpPost]
        public ActionResult Update(Task task)
        {
            var sqlCmd = _manager.CreateCommand();
            sqlCmd.CommandText = SqlQueryBuilder.PrepareSqlQuery(EntityQueryType.Update, task, "WHERE TaskId="+task.TaskId);
            _manager.OpenConnection();
            if (sqlCmd.ExecuteNonQuery() > 0)
            {
                _manager.CloseConnection();
                return RedirectToAction("Index", "Task");
            }

            _manager.CloseConnection();


            return View(task);
        }

        public ActionResult Delete(int id)
        {
            TaskView task;

            var sqlCmd = _manager.CreateCommand();
            sqlCmd.CommandText = SqlQueryBuilder.PrepareSqlQuery(EntityQueryType.Select, new Task(), " WHERE TaskId=" + id);
            _manager.OpenConnection();
            using (DbDataReader dr = sqlCmd.ExecuteReader())
            {
                dr.Read();

                task = new TaskView
                {
                    TaskId = (int)dr["TaskId"],
                    Name = (string)dr["Name"],
                    Workload = (string)dr["Workload"],
                    StartDate = (DateTime)dr["StartDate"],
                    EndDate = (DateTime)dr["EndDate"],
                    TaskStateId = (int)dr["TaskStateId"],
                    PersonId = (int)dr["PersonId"]
                };
            }
            sqlCmd.CommandText = SqlQueryBuilder.PrepareSqlQuery(EntityQueryType.Select, new TaskState(),
                    "WHERE TaskStateId=" + task.TaskStateId);
            using (DbDataReader dr = sqlCmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    task.State = new TaskState
                    {
                        TaskStateId = (int)dr["TaskStateId"],
                        Name = (string)dr["Name"]
                    };
                }
            }

            sqlCmd.CommandText = SqlQueryBuilder.PrepareSqlQuery(EntityQueryType.Select, new Person(),
                "WHERE PersonId=" + task.PersonId);
            using (DbDataReader dr = sqlCmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    task.Person = new Person
                    {
                        PersonId = (int)dr["PersonId"],
                        FirstName = (string)dr["FirstName"],
                        LastName = (string)dr["LastName"],
                        MiddleName = (string)dr["MiddleName"]
                    };
                }
            }
            _manager.CloseConnection();

            return View(task);
        }

        [HttpPost]
        public ActionResult Delete(Task task)
        {
            var sqlCmd = _manager.CreateCommand();
            sqlCmd.CommandText = SqlQueryBuilder.PrepareSqlQuery(EntityQueryType.Delete, task, null);
            _manager.OpenConnection();
            if (sqlCmd.ExecuteNonQuery() > 0)
            {
                _manager.CloseConnection();
                return RedirectToAction("Index", "Task");
            }
            _manager.CloseConnection();

            return View(task);
        }
    }
}
