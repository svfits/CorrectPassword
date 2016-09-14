using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorrectPassword
{
  public  class User
    {
        [Key]
        public int id { get; set; }

        /// <summary>
        ///  логин пользователя
        /// </summary>
        public string loginUser { get; set; }

        /// <summary>
        ////описание пользователя
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// пароль 
        /// </summary>
        public string password { get; set; }

        /// <summary>
        //// сложность пароля 1 есть спец символы
        //// 0 нет их
        /// </summary>
        public int passwordСomplexity { get; set; }

        /// <summary>
        //// длина пароля 
        /// </summary>
        public int passwordLength { get; set; }

        /// <summary>
        /// время жизни пароля
        /// </summary>
        public int passwordLifeTime { get; set; }

        /// <summary>
        /// имя пк пользователя
        /// </summary>
        public string namePc { get; set; }

        /// <summary>
        ////ip адресс пк 
        /// </summary>
        public string ipPC { get; set; }
          
        /// <summary>
        /// отметка времени 
        /// </summary>
        public DateTime stampDateTimeLoadPc { get; set; }
    }
}
