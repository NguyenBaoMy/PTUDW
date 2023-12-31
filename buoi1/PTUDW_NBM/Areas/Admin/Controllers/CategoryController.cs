﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.Model;
using MyClass.DAO;
using PTUDW_NBM.Library;

namespace PTUDW_NBM.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        CategoriesDAO categoriesDAO = new CategoriesDAO();
        ////////////////////////////////////////////////////////////////////
        // GET: Admin/Category/Index
        public ActionResult Index()
        {
            return View(categoriesDAO.getList("Index"));
        }

        ////////////////////////////////////////////////////////////////////
        // GET: Admin/Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = categoriesDAO.getRow(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        ////////////////////////////////////////////////////////////////////
        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            ViewBag.CatList = new SelectList(categoriesDAO.getList("Index"), "Id", "Name");
            ViewBag.OrderList = new SelectList(categoriesDAO.getList("Index"), "Order", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categories categories)
        {
            if (ModelState.IsValid)
            {
                //xu ly tu dong cho cac truong sau:
                //-----CreateAt
                categories.CreateAt = DateTime.Now;
                //-----CreateBy
                categories.CreateBy = Convert.ToInt32(Session["UserID"]);
                //slug
                categories.Slug = XString.Str_Slug(categories.Name);
                //ParentID
                if (categories.ParentID == null)
                {
                    categories.ParentID = 0;
                }
                //Order
                if (categories.Order == null)
                {
                    categories.Order = 1;
                }
                else
                {
                    categories.Order += 1;
                }
                //UpdateAt
                categories.UpdateAt = DateTime.Now;
                //UpdateBy
                categories.UpdateBy = Convert.ToInt32(Session["UserID"]);

                categoriesDAO.Insert(categories);
                //hien thi thong bao thanh cong
                TempData["message"] = new XMessage("success", "Tạo mới loại sản phẩm thành công");

                return RedirectToAction("Index");
            }
            ViewBag.CatList = new SelectList(categoriesDAO.getList("Index"), "Id", "Name");
            ViewBag.OrderList = new SelectList(categoriesDAO.getList("Index"), "Order", "Name");

            return View(categories);
        }

        ////////////////////////////////////////////////////////////////////
        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.CatList = new SelectList(categoriesDAO.getList("Index"), "Id", "Name");
            ViewBag.OrderList = new SelectList(categoriesDAO.getList("Index"), "Order", "Name");
            if (id == null)
            {
                //hien thi thong bao
                TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
                return RedirectToAction("Index");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            Categories categories = categoriesDAO.getRow(id);
            if (categories == null)
            {
                //hien thi thong bao
                TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
                return RedirectToAction("Index");
                //return HttpNotFound();
            }
            return View(categories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categories categories)
        {
            if (ModelState.IsValid)
            {
                //xu ly tu dong cho cac truong sau:
                //slug
                categories.Slug = XString.Str_Slug(categories.Name);
                //ParentID
                if (categories.ParentID == null)
                {
                    categories.ParentID = 0;
                }
                //Order
                if (categories.Order == null)
                {
                    categories.Order = 1;
                }
                else
                {
                    categories.Order += 1;
                }
                //UpdateAt
                categories.UpdateAt = DateTime.Now;
                //UpdateBy
                categories.UpdateBy = Convert.ToInt32(Session["UserID"]);
                //cap nhat DB
                categoriesDAO.Update(categories);
                //hien thi thong bao thanh cong
                TempData["message"] = new XMessage("success", "Cập nhật thông tin thành công");
                return RedirectToAction("Index");
            }
            ViewBag.CatList = new SelectList(categoriesDAO.getList("Index"), "Id", "Name");
            ViewBag.OrderList = new SelectList(categoriesDAO.getList("Index"), "Order", "Name");
            return View(categories);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //hien thi thong bao
                TempData["message"] = new XMessage("danger", "Xóa mẩu tin thất bại");
                return RedirectToAction("Trash");
            }
            Categories categories = categoriesDAO.getRow(id);
            if (categories == null)
            {
                //hien thi thong bao
                TempData["message"] = new XMessage("danger", "Xóa mẩu tin thất bại");
                return RedirectToAction("Trash");
            }
            return View(categories);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Categories categories = categoriesDAO.getRow(id);
            //tim thay mau tin, xoa
            categoriesDAO.Delete(categories);
            //hien thi thong bao
            TempData["message"] = new XMessage("success", "Xóa mẩu tin thành công");
            return RedirectToAction("Trash");
        }

        ////////////////////////////////////////////////////////////////////
        // GET: Admin/Category/Status/5
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                //hien thi thong bao
                TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
                return RedirectToAction("Index");
            }
            Categories categories = categoriesDAO.getRow(id);
            if (categories == null)
            {
                //hien thi thong bao
                TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
                return RedirectToAction("Index");
            }
            //cap nhat trang thai
            categories.Status = (categories.Status == 1) ? 2 : 1;
            //cap nhạt Update At
            categories.UpdateAt = DateTime.Now;
            //cap nhat Update By
            categories.UpdateBy = Convert.ToInt32(Session["UserID"]);
            //Update DB
            categoriesDAO.Update(categories);
            //hien thi thong bao
            TempData["message"] = new XMessage("success", "Cập nhật trạng thái thành công");
            //tro ve trang Index
            return RedirectToAction("Index");
        }
        // GET: Admin/Category/DelTrash/5
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                //hien thi thong bao
                TempData["message"] = new XMessage("danger", "Xóa mẩu tin thất bại");
                return RedirectToAction("Index");
            }
            Categories categories = categoriesDAO.getRow(id);
            if (categories == null)
            {
                //hien thi thong bao
                TempData["message"] = new XMessage("danger", "Xóa mẩu tin thất bại");
                return RedirectToAction("Index");
            }
            else
            {
                //cap nhat trang thai
                categories.Status = 0;
                //cap nhạt Update At
                categories.UpdateAt = DateTime.Now;
                //cap nhat Update By
                categories.UpdateBy = Convert.ToInt32(Session["UserID"]);
                //Update DB
                categoriesDAO.Update(categories);
                //hien thi thong bao
                TempData["message"] = new XMessage("success", "Xóa mẩu tin thành công");
                //tro ve trang Index
                return RedirectToAction("Index");
            }
        }

        ////////////////////////////////////////////////////////////////////
        // GET: Admin/Category/Trash = luc thung rac
        public ActionResult Trash()
        {
            return View(categoriesDAO.getList("Trash"));
        }

        //////////////////////////////////////////////////////////////////////////////////////
        //RECOVER
        // GET: Admin/Category/Recover/5
        public ActionResult Recover(int? id)
        {
            if (id == null)
            {
                //thong bao that bai
                TempData["message"] = new XMessage("danger", "Phục hồi mẫu tin thất bại");
                return RedirectToAction("Index");
            }
            //truy van dong co id = id yeu cau
            Categories categories = categoriesDAO.getRow(id);
            if (id == null)
            {
                //thong bao that bai
                TempData["message"] = new XMessage("danger", "Phục hồi mẫu tin thất bại");
                return RedirectToAction("Index");
            }
            if (categories == null)
            {
                //thong bao that bai
                TempData["message"] = new XMessage("danger", "Phục hồi mẫu tin thất bại");
                return RedirectToAction("Index");
            }
            //chuyen doi trang thai cua Satus tu 0 -> 2: Ko xuất bản
            categories.Status = 2;

            //cap nhat gia tri UpdateAt
            categories.UpdateAt = DateTime.Now;

            //cap nhat lai DB
            categoriesDAO.Update(categories);

            //thong bao phuc hoi du lieu thanh cong
            TempData["message"] = TempData["message"] = new XMessage("success", "Phục hồi mẫu tin thành công");

            return RedirectToAction("Index");
        }
    }
}
