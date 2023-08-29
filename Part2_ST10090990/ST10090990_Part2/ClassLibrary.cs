using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ST10090990_Part2
{
    public class ClassLibrary
    {

        public string Name { get; set; }

        public string StudentNum { get; set; }

        public string University { get; set; }

        public string modCode { get; set; }

        public string modName { get; set; }

        public int credits { get; set; }

        public double hours { get; set; }

        public int numWeeks { get; set; }

        public DateTime startDate { get; set; }

        public double SelfStudy { get; set; }

        public double Remainder { get; set; }

        public static List<ClassLibrary> moduleInfo = new List<ClassLibrary>(); //Creating my list

        public ClassLibrary() //Default constructor
        {

        }

        public ClassLibrary(string ModCode, string ModName, int Credits, double Hours, int NumWeeks, DateTime StartDate, double selfStudy, double remainder) //Constructor used to populate the list
        {
            this.modCode = ModCode;
            this.modName = ModName;
            this.credits = Credits;
            this.hours = Hours;
            this.numWeeks = NumWeeks;
            this.startDate = StartDate;
            SelfStudy = selfStudy;
            this.Remainder = remainder;
        }


        public void Calculation() //Method to calculate the number of self study hours required per week
        {
            SelfStudy = ((credits * 10) / numWeeks) - hours;
        }

        public void Calculation2() //Method used to calculate how many hours of self study remain in a week after getting user input on the number of hours the have self studied in that week
        {
            Remainder = SelfStudy - Remainder;
        }

        public override string ToString() //An override method to display the items in the list
        {
            return $"Module Code: {modCode} | Module Name: {modName} | Credits: {credits} | Hours: {hours} | Number of Weeks: {numWeeks} | Start Date: {startDate} | Required Self-Study Hours: {SelfStudy}";
        }
        public string hashPassword(string password)
        {
            // SHA classes are disposable, use using to ensure any managed resources are properly disposed of by the runtime
            var sha = SHA256.Create();
            var asByteArray = Encoding.Default.GetBytes(password);
            var hashedPassword = sha.ComputeHash(asByteArray);
            return Convert.ToBase64String(hashedPassword);
        }
    }
}
