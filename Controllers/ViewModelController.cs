using HospitalMvcProject.Models;
using HospitalMvcProject.Models.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalMvcProject.Controllers
{
    public class ViewModelController : Controller
    {
        // GET: ViewModel
        private Model1 db = new Model1();

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var doctor = from d in db.Doctors
                         select d;
            if (!String.IsNullOrEmpty(searchString))
            {
                doctor = doctor.Where(d => d.DoctorName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    doctor = doctor.OrderByDescending(d => d.DoctorName);
                    break;

                default:
                    doctor = doctor.OrderBy(d => d.DoctorName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(doctor.ToPagedList(pageNumber, pageSize));

        }

        [HttpGet]

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult AddOrEdit(DoctorCreateViewModel viewObj)
        {
            var result = false;
            string fileName = Path.GetFileNameWithoutExtension(viewObj.ImageFile.FileName);
            string extension = Path.GetExtension(viewObj.ImageFile.FileName);
            string fileWithExtension = fileName + extension;
            Doctor trObj = new Doctor();
            trObj.DoctorName = viewObj.DoctorName;
            trObj.PhoneNo = viewObj.PhoneNo;
            trObj.Email = viewObj.Email;
            trObj.JoiningDate = viewObj.JoiningDate;
            trObj.ImageName = fileWithExtension;
            trObj.ImageUrl = "~/Images/" + fileName + extension;
            string serverPath = Path.Combine(Server.MapPath("~/Images/" + fileName + extension));
            viewObj.ImageFile.SaveAs(serverPath);
            if (ModelState.IsValid)
            {
                if (viewObj.DoctorID == 0)
                {
                    db.Doctors.Add(trObj);
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    trObj.DoctorID = viewObj.DoctorID;
                    db.Entry(trObj).State = EntityState.Modified;
                    db.SaveChanges();
                    result = true;
                }
            }
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                if (viewObj.DoctorID == 0)
                {
                    return View("Create");
                }
                else
                {
                    return View("Edit");
                }
            }
        }

        public ActionResult Edit(int id)
        {
            Doctor trObj = db.Doctors.SingleOrDefault(t => t.DoctorID == id);
            DoctorCreateViewModel viewObj = new DoctorCreateViewModel();
            viewObj.DoctorID = trObj.DoctorID;
            viewObj.DoctorName = trObj.DoctorName;
            viewObj.PhoneNo = trObj.PhoneNo;
            viewObj.Email = trObj.Email;
            viewObj.JoiningDate = trObj.JoiningDate;
            viewObj.ImageUrl = trObj.ImageUrl;
            viewObj.ImageName = trObj.ImageName;
            return View(viewObj);
        }
        //[Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult Delete(int id)
        {
            Doctor trObj = db.Doctors.SingleOrDefault(t => t.DoctorID == id);
            {
                db.Doctors.Remove(trObj);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}