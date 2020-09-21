using MyShop.Core.Entities.Base;

namespace MyShop.Core.Entities
{
    public class Currency : BaseManualEntity<int>
    {
        public string Code { get; set; } // UAH, USD, EUR
        public string SymbolLeft { get; set; } // $, €
        public string SymbolRight { get; set; } // грн.
        public double Value { get; set; } // Курс валют относительно базовой. Пример: доллар - 1.0; гривна - 25.0
        public bool IsBase { get; set; } // true - base, false - usual
    }
}