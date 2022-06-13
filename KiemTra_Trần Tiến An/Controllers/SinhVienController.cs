using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KiemTra_Trần_Tiến_An.Models;

namespace KiemTra_Trần_Tiến_An.Controllers
{
    public class SinhVienController : Controller
    {
        // GET: SinhVien
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult Index()
        {
            var all_sinhvien = from tt in data.SinhViens select tt;
            return View(all_sinhvien);
        }
        public ActionResult Detail(string id)
        {
            var A_sinhvien = data.SinhViens.Where(m => m.MaSV == id).First();
            return View(A_sinhvien);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, SinhVien s)
        {
            var E_Masv = collection["MaSV"];
            var E_Hoten = collection["HoTen"];
            var E_Gt = collection["GioiTinh"];
            var E_ngaycapnhat = Convert.ToDateTime(collection["NgaySinh"]);
            var E_Hinh = collection["Hinh"];
            var E_MaNghanh = collection["MaNghanh"];
            if (string.IsNullOrEmpty(E_Hoten))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                s.MaSV = E_Masv;
                s.HoTen = E_Hoten.ToString();
                s.GioiTinh = E_Gt.ToString();
                s.NgaySinh = E_ngaycapnhat;
                s.Hinh = E_Hinh.ToString();
                s.MaNganh = E_MaNghanh;
                data.SinhViens.InsertOnSubmit(s);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }
        public ActionResult Edit(string id)
        {
            var E_Sinhvien = data.SinhViens.First(m => m.MaSV == id);
            return View(E_Sinhvien);
        }
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            var E_Id = data.SinhViens.First(m => m.MaSV == id);
            var E_Hoten = collection["Hoten"];
            var E_GioiTinh = collection["gioitinh"];
            var E_NgaySinh = Convert.ToDateTime(collection["ngaysinh"]);
            var E_Hinh = collection["hinh"];
            var E_MaNganh = collection["manganh"];
            E_Id.MaSV = id;
            if (string.IsNullOrEmpty(E_Hoten))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                E_Id.HoTen = E_Hoten;
                E_Id.GioiTinh = E_GioiTinh;
                E_Id.NgaySinh = E_NgaySinh;
                E_Id.Hinh = E_Hinh;
                E_Id.MaNganh = E_MaNganh;
                UpdateModel(E_Id);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
        //-----------------------------------------
        public ActionResult Delete(String id)
        {
            var D_sach = data.SinhViens.First(m => m.MaSV == id);
            return View(D_sach);
        }
        [HttpPost]
        public ActionResult Delete(String id, FormCollection collection)
        {
            var D_sach = data.SinhViens.Where(m => m.MaSV == id).First();
            data.SinhViens.DeleteOnSubmit(D_sach);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/images/" + file.FileName));
            return "/Content/images/" + file.FileName;
        }
    }
}
