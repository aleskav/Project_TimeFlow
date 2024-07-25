using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TimeFlow.Data;
using TimeFlow.Models;

namespace TimeFlow.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public UserService(ApplicationDbContext context , UserManager<User> userManager) 
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<Boolean> toAdmin(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return false;
            }

            var hasRole = await _userManager.IsInRoleAsync(user, "Admin");
            if (hasRole)
            {
                return false;
            }

            var newUser = new Admin
            {
                AccessFailedCount = user.AccessFailedCount,
                ConcurrencyStamp = user.ConcurrencyStamp,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                Id = user.Id,
                LockoutEnabled = user.LockoutEnabled,
                LockoutEnd = user.LockoutEnd,
                NomFamille = user.NomFamille,
                NormalizedEmail = user.NormalizedEmail,
                NormalizedUserName = user.NormalizedUserName,
                PasswordHash = user.PasswordHash,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                SecurityStamp = user.SecurityStamp,
                TwoFactorEnabled = user.TwoFactorEnabled,
                UserName = user.UserName,
                Prenom = user.Prenom,
                DateEmbauche = user.DateEmbauche
            };

            _context.Users.Remove(user);
            _context.Users.Add(newUser);
            _context.SaveChanges();

            //delete old roles
            var roles = await _userManager.GetRolesAsync(newUser);
            _ = await _userManager.RemoveFromRolesAsync(newUser, roles);

            _ = await  _userManager.AddToRoleAsync(newUser, "Admin");

            return true;
        }

        public async Task<Boolean> toSuperviseur(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            var hasRole = await _userManager.IsInRoleAsync(user, "Superviseur");

            if ( user == null || hasRole)
            {
                return false;
            }
            
            var newUser = new Superviseur
            {
                AccessFailedCount = user.AccessFailedCount,
                ConcurrencyStamp = user.ConcurrencyStamp,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                Id = user.Id,
                LockoutEnabled = user.LockoutEnabled,
                LockoutEnd = user.LockoutEnd,
                NomFamille = user.NomFamille,
                NormalizedEmail = user.NormalizedEmail,
                NormalizedUserName = user.NormalizedUserName,
                PasswordHash = user.PasswordHash,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                SecurityStamp = user.SecurityStamp,
                TwoFactorEnabled = user.TwoFactorEnabled,
                UserName = user.UserName,
                Prenom = user.Prenom,
                DateEmbauche = user.DateEmbauche
            };

            _context.Users.Remove(user);
            _context.Users.Add(newUser);
            _context.SaveChanges();

            //delete old roles
            var roles = await _userManager.GetRolesAsync(newUser);
            _ = await _userManager.RemoveFromRolesAsync(newUser, roles);

            _ = await _userManager.AddToRoleAsync(newUser, "Superviseur");

            return true;
        }

        public async Task<Boolean> toEmploye(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            var hasRole = await _userManager.IsInRoleAsync(user, "Employe");

            if (user == null || hasRole)
            {
                return false;
            }

            var newUser = new Employe
            {
                AccessFailedCount = user.AccessFailedCount,
                ConcurrencyStamp = user.ConcurrencyStamp,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                Id = user.Id,
                LockoutEnabled = user.LockoutEnabled,
                LockoutEnd = user.LockoutEnd,
                NomFamille = user.NomFamille,
                NormalizedEmail = user.NormalizedEmail,
                NormalizedUserName = user.NormalizedUserName,
                PasswordHash = user.PasswordHash,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                SecurityStamp = user.SecurityStamp,
                TwoFactorEnabled = user.TwoFactorEnabled,
                UserName = user.UserName,
                Prenom = user.Prenom,
                DateEmbauche = user.DateEmbauche
            };

            _context.Users.Remove(user);
            _context.Users.Add(newUser);
            _context.SaveChanges();

            //delete old roles
            var roles = await _userManager.GetRolesAsync(newUser);
            _ = await _userManager.RemoveFromRolesAsync(newUser, roles);

            _ = await _userManager.AddToRoleAsync(newUser, "Employe");

            return true;
        }

    }
}
