using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project_Management_System.Data;
using System.ComponentModel.DataAnnotations;

namespace Project_Management_System.Pages.AccountProfile
{
    {
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);

            if (_context.StaffDivision != null)
            {
                StaffDivisions = await _context.StaffDivision.ToListAsync();
            }

            if (_context.Division != null)
            {
                Divisions = await _context.Division.ToListAsync();
            }

            /*if (_context.StaffInterest!= null)
            {
                StaffInterest = await _context.StaffInterest.ToListAsync();
            }*/

            Topic = _db.Topic //select data from database
                .FromSqlRaw("SELECT * FROM Topic WHERE SupervisorID = {0}", user.Id)
                .ToList();

            StaffInterest = _db.StaffInterest //select data from database
                .FromSqlRaw("SELECT * FROM StaffInterest WHERE StaffID = {0}", user.Id)
                .ToList();

            StaffExpertise = _db.StaffExpertise //select data from database
                .FromSqlRaw("SELECT * FROM StaffExpertise WHERE StaffID = {0}", user.Id)
                .ToList();



            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile uploadedImage)
        {
            if (uploadedImage != null && uploadedImage.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await uploadedImage.CopyToAsync(memoryStream);
                    byte[] imageBytes = memoryStream.ToArray();

                    // You can now use the byte array to store the image in your database

                    var user = await _userManager.GetUserAsync(User);
                    if (user != null)
                    {
                        user.ProfilePicture = imageBytes;
                        await _userManager.UpdateAsync(user);
                        await _signInManager.RefreshSignInAsync(user);
                    }
                }
            }

            return RedirectToPage();
        }
    }
}
