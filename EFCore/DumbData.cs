using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore
{
    static class DumbData
    {
        public static string CreateName()
        {
            string[] nameList = { "Ayşe", "Fatma", "Hayriye", "Ali", "Veli", "İbrahim", "Osman", "Furkan", "Berkecan", "Ege", "Kazım", "Beyza", "Büşra", "Efekan", "Özge", "Öznur", "Barış", "Elif", "Nur", "Gökçe", "Burak", "Bahadır", "Yağız", "Zülal", "Kadri" }; //25
            string[] surnameList = { "Yılmaz", "Bıçak", "Balta", "Kul", "Yıldırım", "Kahraman", "Fırtına", "Eski", "Erden", "Yurt", "Cenk", "Tok", "Yavaş", "Turan", "Türk", "Mahir", "Usta", "Şahin", "Yalın", "Sarı", "Genç" }; //21
            Random rnd = new Random();

            int ad = rnd.Next(0, 24);
            int soyad = rnd.Next(0, 20);

            return nameList[ad] + " " + surnameList[soyad];
        }

    }
}
