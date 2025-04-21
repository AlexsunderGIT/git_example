using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cslight.ДЗ
{
    internal class TaskReceipt
    {
        static void Main(string[] args)
        {
        AuthorReciept author = new AuthorReciept("Вася", DateTime.Now);
        Receipt gavno = new Receipt("Борщ", "всё в воду и помешать", "борщевые ингридиенты", author, "сварить и съесть", 0.2, ReceiptCategory.soup);
        Receipt protein = new Receipt("Протеин", "молоко + протик", "протик и мовочко", author, "миксером смешать", 9.8, ReceiptCategory.cocktail);
        }
        


        class Receipt
        {

            readonly string receiptName;
            readonly string description;
            readonly string ingredients;
            readonly string algorithmCooking;
            public AuthorReciept author;
            public ReceiptCategory category;
            public double rating;
            public Receipt(string name, string desc, string ingred, AuthorReciept authorConstructor, string algCook, double rate, ReceiptCategory recCat)
            {
                receiptName = name;
                description = desc;
                ingredients = ingred;
                author = authorConstructor;
                algorithmCooking = algCook;
                rating = rate;
                recCat = category;

            }
        }


        public enum ReceiptCategory
        {

            soup,
            dessert,
            cocktail,
            mainDish,
            snack,
            alсoholCocktail
        }
        class AuthorReciept
        {
            readonly string authorName;
            public DateTime createReceipt = DateTime.Now;
            public double averageRatingOfAuthor;
            public List<Receipt> receiptCategorie = new List<Receipt>();

            public AuthorReciept(string name, DateTime date)
            {
                authorName = name;
                date = createReceipt;
            }

        }
    }
}
