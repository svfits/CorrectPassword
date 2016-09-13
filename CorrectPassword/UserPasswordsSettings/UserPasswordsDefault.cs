using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorrectPassword.UserPasswordsSettings
{
  public  class UserPasswordsDefault
    {
        [Key]
        public int id { get; set; }

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
        /// дефолтный логин 
        /// </summary>
        public string defaultLoginUser { get; set; }
    }
}
