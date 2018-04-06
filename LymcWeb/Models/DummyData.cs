using LymcWeb.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LymcWeb.Models
{
    public class DummyData
    {
        private ApplicationDbContext _context;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;

        public DummyData(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task Seed()
        {
            if (!_roleManager.RoleExistsAsync("Admin").Result)
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!_roleManager.RoleExistsAsync("Member").Result)
            {
                await _roleManager.CreateAsync(new IdentityRole("Member"));
            }

            if (!_roleManager.RoleExistsAsync("Guest").Result)
            {
                await _roleManager.CreateAsync(new IdentityRole("Guest"));
            }

            if (_userManager.FindByNameAsync("a").Result == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "a",
                    Email = "a@a.a",
                    FirstName = "Administrator",
                    LastName = "@Infosys.bcit.ca",
                    StreetAddress = "100 Smith Ave",
                    MobileNumber = "12134553",
                    SailingExp = "100 Years"
                };

                var result = _userManager.CreateAsync(user, "P@$$w0rd").Result;

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(_userManager.FindByNameAsync("a").Result, "Admin");
                    await _userManager.AddToRoleAsync(_userManager.FindByNameAsync("a").Result, "Member");
                }

              
            }

            if (_userManager.FindByNameAsync("m").Result == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "m",
                    Email = "m@m.m",
                    FirstName = "Mike",
                    LastName = "Lee",
                    StreetAddress = "999 Summer Ave",
                    MobileNumber = "7784553333",
                    SailingExp = "1 Year"
                };
                var result = await _userManager.CreateAsync(user, "P@$$w0rd");
                if (result.Succeeded)
                    await _userManager.AddToRoleAsync(_userManager.FindByNameAsync("m").Result, "Member");
            }

            _context.SaveChanges();

            if (!_context.Boats.Any())
            {
                _context.Boats.AddRange(getBoats(_context, _userManager));
            }
            _context.SaveChanges();
        }
        
        public static List<Boat> getBoats(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            List<Boat> list = new List<Boat>()
            {
                new Boat()
                {
                    BoatName = "Wind Dancer",
                    LengthInFeet = 33,
                    Make = "Boston Whaler",
                    BoatPicUrl = "https://images.pexels.com/photos/163236/luxury-yacht-boat-speed-water-163236.jpeg?h=350&auto=compress&cs=tinysrgb",
                    Year = 2005,
                    RecordCreationDate = DateTime.Now,
                    //CreatedBy ="a"
                    CreatedBy = userManager.FindByNameAsync("a").Result
                },
                new Boat()
                {
                    BoatName = "Windsong",
                    LengthInFeet = 20,
                    Make="American Skier",
                    BoatPicUrl="http://l.rgbimg.com/cache1oIH38/users/t/ta/tacluda/600/myISknQ.jpg",
                    //BoatPicUrl="http://newimages.yachtworld.com/resize/1/19/86/4681986_20140410083142702_1_XLARGE.jpg?f=/1/19/86/4681986_20140410083142702_1_XLARGE.jpg&w=924&h=693&t=1397147573000",
                    Year = 2006,
                    RecordCreationDate = DateTime.Now,
                    CreatedBy =  userManager.FindByNameAsync("m").Result

                },
                new Boat()
                {
                    BoatName = "Whisper",
                    LengthInFeet = 15,
                    Make = "Gulf Craft",
                    BoatPicUrl="https://image.yachtcharterfleet.com/charter-REHAB/REHAB-2.jpg?image_id=122397&k=2c4a&w=1200&h=779&q=75",
                    Year = 2015,
                    RecordCreationDate = DateTime.Now,
                    CreatedBy =  userManager.FindByNameAsync("m").Result

                }
            };
            return list;
        }
    }
}
