using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingSystemMobile.Models
{
    // Этот класс точно описывает, что мы получаем от API после успешного входа
    public class LoginResponseDto
    {
        public int Id { get; set; }
        public string Role { get; set; }
    }
}
