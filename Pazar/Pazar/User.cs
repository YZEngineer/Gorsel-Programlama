using System;
using System.Collections.Generic;

namespace Pazar
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime RegisterDate { get; set; }
        private List<int> favoriteProductIds = new List<int>();
        private List<int> favorite2ProductIds = new List<int>();

        public User()
        {
            favoriteProductIds = new List<int>();
        }

        public bool IsProductFavorite(int productId)
        {
            return favoriteProductIds.Contains(productId);
        }

        public void AddToFavorites(int productId)
        {
            if (!favoriteProductIds.Contains(productId))
            {
                favoriteProductIds.Add(productId);
                // TODO: Veritabanına kaydet
            }
        }

        public void RemoveFromFavorites(int productId)
        {
            if (favoriteProductIds.Contains(productId))
            {
                favoriteProductIds.Remove(productId);
                // TODO: Veritabanından sil
            }
        }

        public List<int> GetFavoriteProductIds()
        {
            return new List<int>(favoriteProductIds);
        }

        public void AddToFavorites2(int productId)
        {
            if (!favorite2ProductIds.Contains(productId))
            {
                favorite2ProductIds.Add(productId);
            }
        }

        public void RemoveFromFavorites2(int productId)
        {
            favorite2ProductIds.Remove(productId);
        }

        public List<int> GetFavorite2ProductIds()
        {
            return new List<int>(favorite2ProductIds);
        }

        public bool IsProductFavorite2(int productId)
        {
            return favorite2ProductIds.Contains(productId);
        }
    }
} 