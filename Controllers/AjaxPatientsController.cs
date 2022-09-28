using CrystalDecisions.CrystalReports.Engine;
using HospitalMvcProject.Models;
using HospitalMvcProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace HospitalMvcProject.Controllers
{
    public class AjaxPatientsController : Controller
    {
        #region Single Page Application with MVC Razor Code
        [HttpGet]
        public ActionResult Single(int? id)
        {
            var db = new Model1();
            var opatient = (from o in db.Patients where o.PatientID == id select o).FirstOrDefault();
            opatient = opatient == null ? new Patient() : opatient;
            ViewData["List"] = db.Patients.ToList();
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName");
            ViewBag.ReferDoctor = new SelectList(db.Doctors, "DoctorID", "DoctorName");
            return View(opatient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Single(Patient model, HttpPostedFileBase img)
        {
            string fileName = "";
            if (img != null)
            {
                fileName = img.FileName;
                // To save a image to a folder
                string picture = System.IO.Path.GetFileName(img.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Imgs"), picture);
                img.SaveAs(path);
            }
            var db = new Model1();
            var opatient = db.Patients.Find(model.PatientID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName");
            if (opatient == null)
            {
                #region insert
                model.Picture = "/Imgs/" + fileName;
                db.Patients.Add(model);
                #endregion
                ViewBag.Message = "inserted successfully.";
            }
            else
            {
                #region update
                opatient.PatientName = model.PatientName;
                opatient.Gender = model.Gender;
                opatient.Age = model.Age;
                opatient.BirthDate = model.BirthDate;
                opatient.Address = model.Address;
                opatient.DepartmentID = model.DepartmentID;
                opatient.ReferDoctor = model.ReferDoctor;
                opatient.Picture = "/Imgs/" + fileName;
                #endregion
                ViewBag.Message = "updated successfully.";
            }
            db.SaveChanges();
            ViewData["List"] = db.Patients.ToList();
            return RedirectToAction("Single");
        }
        #endregion
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var db = new Model1();
            var opatient = (from o in db.Patients where o.PatientID == id select o).FirstOrDefault();
            if (opatient != null)
            {
                db.Patients.Remove(opatient);
                db.SaveChanges();
            }
            return RedirectToAction("Single");
        }
        public ActionResult ExportPatient()
        {
            var db = new Model1();
            var ListPatient = (from x in db.Patients
                               join y in db.Departments on x.DepartmentID equals
                               y.DepartmentID join z  in db.Doctors on x.ReferDoctor equals z.DoctorID
                               select new VmPatient
                               {
                                   PatientID = x.PatientID,
                                   PatientName = x.PatientName,
                                   Gender = x.Gender,
                                   Age = x.Age,
                                   BirthDate = x.BirthDate,
                                   Address = x.Address,
                                   DepartmentName = y.DepartmentName,
                                   DoctorName = z.DoctorName,
                                   Picture = "http://localhost:44306" + x.Picture
                               }).ToList();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "CrystalReport4.rpt"));
            rd.SetDataSource(ListToDataTable(ListPatient));
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "Patients.pdf");
            }
            catch
            {
                throw;
            }
        }
        public ActionResult ExportPatientDetails(int id)
        {
            var db = new Model1();
            var ListPatient = (from x in db.Patients
                               join y in db.Departments on x.DepartmentID equals y.DepartmentID
                               join z in db.Doctors on x.ReferDoctor equals z.DoctorID
                               where x.PatientID == id
                               select new VmPatient
                               {
                                   PatientID = x.PatientID,
                                   PatientName = x.PatientName,
                                   Gender = x.Gender,
                                   Age = x.Age,
                                   BirthDate = x.BirthDate,
                                   Address = x.Address,
                                   DepartmentName = y.DepartmentName,
                                   DoctorName = z.DoctorName,
                                   Picture = "http://localhost:44306" + x.Picture
                               }).ToList();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "CrystalReport3.rpt"));
            rd.SetDataSource(ListToDataTable(ListPatient));
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "Patients.pdf");
            }
            catch
            {
                throw;
            }
        }
        private DataTable ListToDataTable<T>(List<T> items)
        {
            DataTable datatable = new DataTable(typeof(T).Name);
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                datatable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                datatable.Rows.Add(values);
            }
            return datatable;
        }
    }
}