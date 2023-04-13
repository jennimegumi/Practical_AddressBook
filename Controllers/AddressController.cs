using Practical_AddressBook1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Practical_AddressBook1.Controllers
{
    public class AddressController : Controller
    {
        private readonly AddressBookDAL _dal;

        public AddressController(AddressBookDAL dal)
        {
            _dal = dal;
        }

        [HttpGet]

        public IActionResult Index()
        {
            List<AddressBookLogs> logs = new List<AddressBookLogs>();
            try
            {
                logs = _dal.GetAll();
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            return View(logs);
        }

        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Insert(AddressBookLogs ab)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "Invalid Object!";
                }
                bool result = _dal.Insert(ab);

                if (!result)
                {
                    TempData["errorMessage"] = "Unable to Insert Person's Details";
                    return View();
                }

                TempData["successMessage"] = "Inserting Person's Details success!";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                AddressBookLogs logs = _dal.GetPersonById(id);
                if (logs.PersonID == 0)
                {
                    TempData["errorMessage"] = $"Unable to find ID: {id}";
                    return RedirectToAction("Index");
                }
                return View(logs);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult Edit(AddressBookLogs logs)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "Invalid Data";
                    return View();
                }
                bool result = _dal.Update(logs);
                if (!result)
                {
                    TempData["errorMessage"] = "Updating Person's Data Failed!";
                    return View();
                }
                TempData["successMessage"] = "Updated Person's Data successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                AddressBookLogs logs = _dal.GetPersonById(id);
                if (logs.PersonID == 0)
                {
                    TempData["errorMessage"] = $"Unable to find ID: {id}";
                    return RedirectToAction("Index");
                }
                return View(logs);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(AddressBookLogs logs)
        {
            try
            {
                bool result = _dal.Delete(logs.PersonID);

                if (!result)
                {
                    TempData["errorMessage"] = "Unable to delete Person's Data";
                    return View();
                }
                TempData["successMessage"] = "Person's Data has been deleted successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
    }
}
