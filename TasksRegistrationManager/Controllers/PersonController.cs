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

namespace TasksRegistrationManager.Controllers
{
    public class PersonController : Controller
    {
        private readonly IDbConnectionManager _manager;

        public PersonController(IDbConnectionManager dbConnectionManager)
        {
            _manager = dbConnectionManager;
            _manager.Initialaize();
        }

        //
        // GET: /Person/

        public ActionResult Index()
        {
            var res = new List<Person>();


            try
            {
                var sqlCmd = _manager.CreateCommand();
                sqlCmd.CommandText = SqlQueryBuilder.PrepareSqlQuery(EntityQueryType.Select, new Person(), null);
                _manager.OpenConnection();
                using (DbDataReader dr = sqlCmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        res.Add(new Person
                        {
                            PersonId = (int)dr["PersonId"],
                            FirstName = (string)dr["FirstName"],
                            LastName = (string)dr["LastName"],
                            MiddleName = (string)dr["MiddleName"]
                        });
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                _manager.CloseConnection();
            }
            


            return View(res);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Person person)
        {
            try
            {
                var sqlCmd = _manager.CreateCommand();
                sqlCmd.CommandText = SqlQueryBuilder.PrepareSqlQuery(EntityQueryType.Insert, person, null);
                _manager.OpenConnection();
                if (sqlCmd.ExecuteNonQuery() > 0)
                {
                    _manager.CloseConnection();
                    return RedirectToAction("Index", "Person");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                _manager.CloseConnection();
            }

            return View();
        }

        
        public ActionResult Update(int id)
        {
            Person person;

            try
            {
                var sqlCmd = _manager.CreateCommand();
                sqlCmd.CommandText = SqlQueryBuilder.PrepareSqlQuery(EntityQueryType.Select, new Person(), " WHERE PersonId=" + id);
                _manager.OpenConnection();
                using (DbDataReader dr = sqlCmd.ExecuteReader())
                {
                    dr.Read();

                    person = new Person
                    {
                        PersonId = (int)dr["PersonId"],
                        FirstName = (string)dr["FirstName"],
                        LastName = (string)dr["LastName"],
                        MiddleName = (string)dr["MiddleName"]
                    };
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                _manager.CloseConnection();
            }
            


            return View(person);
        }

        [HttpPost]
        public ActionResult Update(Person person)
        {
            try
            {
                var sqlCmd = _manager.CreateCommand();
                sqlCmd.CommandText = SqlQueryBuilder.PrepareSqlQuery(EntityQueryType.Update, person, null);
                _manager.OpenConnection();
                if (sqlCmd.ExecuteNonQuery() > 0)
                {
                    _manager.CloseConnection();
                    return RedirectToAction("Index", "Person");
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _manager.CloseConnection();
            }

            return View(person);
        }

        
        public ActionResult Delete(int id)
        {
            Person person;

            try
            {
                var sqlCmd = _manager.CreateCommand();
                sqlCmd.CommandText = SqlQueryBuilder.PrepareSqlQuery(EntityQueryType.Select, new Person(), " WHERE PersonId=" + id);
                _manager.OpenConnection();
                using (DbDataReader dr = sqlCmd.ExecuteReader())
                {
                    dr.Read();

                    person = new Person
                    {
                        PersonId = (int)dr["PersonId"],
                        FirstName = (string)dr["FirstName"],
                        LastName = (string)dr["LastName"],
                        MiddleName = (string)dr["MiddleName"]
                    };
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _manager.CloseConnection();
            }
            

            return View(person);
        }

        [HttpPost]
        public ActionResult Delete(Person person)
        {
            try
            {
                var sqlCmd = _manager.CreateCommand();
                _manager.OpenConnection();
                //sqlCmd.CommandText = "DELETE FROM Tasks WHERE Tasks.PersonId = " + person.PersonId;
                //sqlCmd.ExecuteNonQuery();

                sqlCmd.CommandText = SqlQueryBuilder.PrepareSqlQuery(EntityQueryType.Delete, person, null);
                if (sqlCmd.ExecuteNonQuery() > 0)
                {
                    _manager.CloseConnection();
                    return RedirectToAction("Index", "Person");
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _manager.CloseConnection();
            }
            return View(person);
        }

    }
}
