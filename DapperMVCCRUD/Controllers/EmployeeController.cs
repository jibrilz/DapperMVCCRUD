﻿using Dapper;
using DapperMVCCRUD.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperMVCCRUD.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: /Employee/
        public IActionResult Index()
        {
            return View(DapperORM.ReturnList<EmployeeModel>("EmployeeViewAll"));
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View();
            else
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@EmployeeID", id);
                return View(DapperORM.ReturnList<EmployeeModel>("EmployeeViewByID", param).FirstOrDefault<EmployeeModel>());
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(EmployeeModel emp)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@EmployeeID", emp.EmployeeID);
            param.Add("@Name", emp.Name);
            param.Add("@Position", emp.Position);
            param.Add("@Age", emp.Age);
            param.Add("@Salary", emp.Salary);
            DapperORM.ExecuteWithoutReturn("EmployeeAddOrEdit", param);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@EmployeeID", id);
            DapperORM.ExecuteWithoutReturn("EmployeeDeleteByID", param);
            return RedirectToAction("Index");
        }
    }
}
