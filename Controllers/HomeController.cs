using System.Data;
using System.Diagnostics;
using DotNetCore_CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace DotNetCore_CRUD.Controllers
{
    public class HomeController : Controller
    {
        private readonly SqlConnection _connection;

        public HomeController(IConfiguration config)
        {
            string? _connectionString = config.GetConnectionString("DBConnection");
            _connection = new SqlConnection(_connectionString);
        }

        public IActionResult Index()
        {
            ViewBag.message = TempData["message"];
            return View();
        }

        [HttpPost]
        public IActionResult Index(FormModel model)
        {
            SqlCommand command = new SqlCommand("sp_InsertFormData", _connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            //if image field need
            string ProfileImage = "";
            if (model.ProfilePath != null)
            {
                ProfileImage = Path.GetFileName(model.ProfilePath.FileName);
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", ProfileImage);

                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    model.ProfilePath.CopyTo(stream);
                }
            }

            command.Parameters.AddWithValue("@Name", model.Name);
            command.Parameters.AddWithValue("@Email", model.Email);
            command.Parameters.AddWithValue("@ProfilePath", ProfileImage);
            command.Parameters.AddWithValue("@Gender", model.Gender);
            command.Parameters.AddWithValue("@Qualification", model.Qualification);
            command.Parameters.AddWithValue("@Message", model.Message);
            command.Parameters.AddWithValue("@MobileNumber", model.MobileNumber);

            _connection.Open();
            int result = command.ExecuteNonQuery();
            _connection.Close();

            if (result > 0)
            {
                TempData["message"] = "added successfully";
            }
            else
            {
                TempData["message"] = "Try again.";
            }
            return RedirectToAction("Index");
        }

        public IActionResult About(int Id)
        {
            if (Id != 0)
            {
                SqlCommand com = new SqlCommand("sp_DeleteFormData", _connection);
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", Id);
                _connection.Open();
                com.ExecuteNonQuery();
                _connection.Close();
            }
            SqlCommand command = new SqlCommand("sp_GetAllFormData", _connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(command);

            DataTable dataTable = new DataTable();
            sda.Fill(dataTable);

            List<FormModel> formList = new List<FormModel>();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    FormModel model = new FormModel();

                    model.Id = row["Id"] != DBNull.Value ? Convert.ToInt32(row["Id"]) : 0;
                    model.Name = row["Name"] != DBNull.Value ? Convert.ToString(row["Name"]) : "";
                    model.Email = row["Email"] != DBNull.Value ? Convert.ToString(row["Email"]) : "";
                    model.MobileNumber = row["MobileNumber"] != DBNull.Value ? Convert.ToInt64(row["MobileNumber"]) : 0;
                    model.Gender = row["Gender"] != DBNull.Value ? Convert.ToString(row["Gender"]) : "";
                    model.Qualification = row["Qualification"] != DBNull.Value ? Convert.ToString(row["Qualification"]) : "";
                    model.Message = row["Message"] != DBNull.Value ? Convert.ToString(row["Message"]) : "";
                    model.ProfilePathStr = row["ProfilePath"] != DBNull.Value ? Convert.ToString(row["ProfilePath"]) : "";

                    formList.Add(model);
                }
            }

            ViewBag.message = TempData["message"];
            return View(formList);
        }

        public IActionResult EditPage(int Id)
        {
            if (Id > 0)
            {
                SqlCommand command = new SqlCommand("sp_GetDataByID", _connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", Id);

                SqlDataAdapter sda = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();
                sda.Fill(dataTable);

                FormModel formModel = new FormModel();
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        formModel.Id = row["Id"] != DBNull.Value ? Convert.ToInt32(row["Id"]) : 0;
                        formModel.Name = row["Name"] != DBNull.Value ? Convert.ToString(row["Name"]) : "";
                        formModel.Email = row["Email"] != DBNull.Value ? Convert.ToString(row["Email"]) : "";
                        formModel.MobileNumber = row["MobileNumber"] != DBNull.Value ? Convert.ToInt64(row["MobileNumber"]) : 0;
                        formModel.Gender = row["Gender"] != DBNull.Value ? Convert.ToString(row["Gender"]) : "";
                        formModel.Qualification = row["Qualification"] != DBNull.Value ? Convert.ToString(row["Qualification"]) : "";
                        formModel.Message = row["Message"] != DBNull.Value ? Convert.ToString(row["Message"]) : "";
                        formModel.ProfilePathStr = row["ProfilePath"] != DBNull.Value ? Convert.ToString(row["ProfilePath"]) : "";
                    }
                }

                return View(formModel);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult UpdateForm(FormModel model)
        {
            SqlCommand command = new SqlCommand("sp_UpdateFormData", _connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            string ProfileImage = "";
            if (model.ProfilePath != null)
            {
                ProfileImage = Path.GetFileName(model.ProfilePath.FileName);
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", ProfileImage);

                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    model.ProfilePath.CopyTo(stream);
                }
            }
            else if (model.ProfilePathStr != null)
            {
                ProfileImage = model.ProfilePathStr;
            }

            command.Parameters.AddWithValue("@Id", model.Id);
            command.Parameters.AddWithValue("@Name", model.Name);
            command.Parameters.AddWithValue("@Email", model.Email);
            command.Parameters.AddWithValue("@ProfilePath", ProfileImage);
            command.Parameters.AddWithValue("@Gender", model.Gender);
            command.Parameters.AddWithValue("@Qualification", model.Qualification);
            command.Parameters.AddWithValue("@Message", model.Message);
            command.Parameters.AddWithValue("@MobileNumber", model.MobileNumber);

            _connection.Open();
            int result = command.ExecuteNonQuery();
            _connection.Close();

            if (result > 0)
            {
                TempData["message"] = "updated successfully";
            }
            else
            {
                TempData["message"] = "Try again.";
            }
            return RedirectToAction("About");
        }
    }
}
