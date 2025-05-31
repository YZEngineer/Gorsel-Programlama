using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Pazar
{
    public class UserManager
    {
        private static List<User> users = new List<User>();

        static UserManager()
        {
            // Initialize with default users
            users.Add(new User
            {
                Id = 1,
                Name = "cafer",
                Email = "cafer@gmail.com",
                Password = "123456",
                IsAdmin = true,
                RegisterDate = DateTime.Now
            });
            users.Add(new User
            {
                Id = 2,
                Name = "emirhanhazretleri",
                Email = "emirhan@gmail.com",
                Password = "123456",
                IsAdmin = true,
                RegisterDate = DateTime.Now
            });
            users.Add(new User
            {
                Id = 3,
                Name = "kullanici",
                Email = "kullanici@gmail.com",
                Password = "123456",
                IsAdmin = false,
                RegisterDate = DateTime.Now
            });
        }

        public static (bool success, User user) Authenticate(string email, string password, bool isAdmin, bool isUser)
        {
            var user = users.FirstOrDefault(u => 
                (u.Email == email || u.Name == email) && 
                u.Password == password && 
                ((isAdmin && u.IsAdmin) || (isUser && !u.IsAdmin)));

            return (user != null, user);
        }

        public static bool RegisterUser(User newUser)
        {
            if (users.Any(u => u.Email == newUser.Email))
            {
                return false; // Email already in use
            }

            newUser.Id = users.Count + 1;
            newUser.RegisterDate = DateTime.Now;
            users.Add(newUser);
            return true;
        }

        public static bool UpdateUser(User updatedUser)
        {
            var user = users.FirstOrDefault(u => u.Id == updatedUser.Id);
            if (user != null)
            {
                user.Name = updatedUser.Name;
                user.Email = updatedUser.Email;
                user.Password = updatedUser.Password;
                user.Address = updatedUser.Address;
                user.Phone = updatedUser.Phone;
                return true;
            }
            return false;
        }

        public static bool UpdatePassword(int userId, string newPassword)
        {
            var user = users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                user.Password = newPassword;
                return true;
            }
            return false;
        }

        public static bool DeleteUser(int userId)
        {
            var user = users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                users.Remove(user);
                return true;
            }
            return false;
        }

        public static List<User> GetAllUsers()
        {
            return users;
        }

        public static User GetUserById(int userId)
        {
            return users.FirstOrDefault(u => u.Id == userId);
        }

        public static User GetUserByEmail(string email)
        {
            return users.FirstOrDefault(u => u.Email == email);
        }

        public static void AddToFavorites(int userId, int productId)
        {
            var user = GetUserById(userId);
            if (user != null)
            {
                user.AddToFavorites(productId);
            }
        }

        public static void RemoveFromFavorites(int userId, int productId)
        {
            var user = GetUserById(userId);
            if (user != null)
            {
                user.RemoveFromFavorites(productId);
            }
        }

        public static List<int> GetFavoriteProducts(int userId)
        {
            var user = GetUserById(userId);
            return user?.GetFavoriteProductIds() ?? new List<int>();
        }

        public static bool IsProductFavorite(int userId, int productId)
        {
            var user = GetUserById(userId);
            return user?.IsProductFavorite(productId) ?? false;
        }

        // İkinci favori listesi için metodlar
        public static void AddToFavorites2(int userId, int productId)
        {
            var user = GetUserById(userId);
            if (user != null)
            {
                user.AddToFavorites2(productId);
            }
        }

        public static void RemoveFromFavorites2(int userId, int productId)
        {
            var user = GetUserById(userId);
            if (user != null)
            {
                user.RemoveFromFavorites2(productId);
            }
        }

        public static List<int> GetFavorite2Products(int userId)
        {
            var user = GetUserById(userId);
            return user?.GetFavorite2ProductIds() ?? new List<int>();
        }

        public static bool IsProductFavorite2(int userId, int productId)
        {
            var user = GetUserById(userId);
            return user?.IsProductFavorite2(productId) ?? false;
        }

        public int GetUsersCount()
        {
            //
            return users.Count;
        }

    }
} 