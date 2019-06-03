using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Automation_instruments.Model
{
    abstract class ToolProduct
    {
        public ToolProduct() { }

        public ToolProduct(string name)
        {
            Name = name;
        }
        [Key]
        public int Id { get; set; }

        [MinLength(4), MaxLength(15), Required]
        public string Name { get; set; }
    }

    class Caliber : ToolProduct
    {
        public Caliber() { }
        /// <summary>
        /// Конструктор детали Калибр
        /// </summary>
        /// <param name="name">Наименование детали</param>
        /// <param name="type">Тип детали предельный,нормальный,регулируемый</param>
        public Caliber(string name, string type):base(name)
        {
            Type = type;
        }

        public string Type  { get; set; }
    }

    class Clamp : ToolProduct
    {
        public Clamp() { }
        /// <summary>
        /// Конструктор Скобы
        /// </summary>
        /// <param name="name">Наименование скобы</param>
        /// <param name="typesize">Тип размера 0,30,60,90,120,150</param>
        /// <param name="diametrcontrol">Контроль среднего диаметра</param>
        public Clamp(string name,TypeS typesize, bool diametrcontrol = false):base(name)
        {
            TypeSize = typesize;
            DiametrControl = diametrcontrol;
        }

        public bool DiametrControl { get; set; }

        public TypeS TypeSize { get; set; }
        internal enum TypeS
        {
            S_0 = 0,
            S_30 = 30,
            S_60 = 60,
            S_90 = 90,
            S_120 = 120,
            S_150 = 150
        }
    }

    class Plug : ToolProduct
    {
        public Plug() { }
        /// <summary>
        /// Конструктор Пробки
        /// </summary>
        /// <param name="name">Наименование пробка</param>
        /// <param name="type">Тип односторонний, двухсторонний </param>
        public Plug(string name,string type):base(name)
        {
            Type = type;
        }

        public string Type { get; set; }
    }

    class Ring : ToolProduct
    {
        public Ring() { }
        /// <summary>
        /// Конструктор Резьбового Кольца
        /// </summary>
        /// <param name="name">Наименование кольца</param>
        /// <param name="typeThread">Вид резьбы метрический,дюйм,трубный,специальный</param>
        /// <param name="profile">Профиль полный,укороченый,гладкие резьбовые и т.д.</param>
        public Ring(string name,string typeThread, string profile):base(name)
        {
            TypeThread = typeThread;
            Profile = profile;
        }

        public string TypeThread { get; set; }

        public string Profile { get; set; }
    }

    class Template : ToolProduct
    {
        public Template() { }
        /// <summary>
        /// Конструктор шаблонов
        /// </summary>
        /// <param name="name">ГОСТ наименование шаблона</param>
        public Template(string name) : base(name) { }
    }
}
