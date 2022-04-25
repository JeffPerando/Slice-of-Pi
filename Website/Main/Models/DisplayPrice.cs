
namespace Main.Models
{
    public class DisplayPrice
    {
        private decimal _price;

        public DisplayPrice(int init)
        {
            _price = Convert.ToDecimal(init);
        }

        public DisplayPrice(float init)
        {
            _price = Convert.ToDecimal(Math.Round(init, 2));
        }

        public DisplayPrice(double init)
        {
            _price = Convert.ToDecimal(Math.Round(init, 2));
        }

        public override string ToString()
        {
            return _price.ToString("C");
        }
        /*
        DO NOT USE THESE
        I have no idea why, but they cause access violation exceptions.
        and since VS 2022 has no stack trace*/
        public static bool operator==(DisplayPrice? a, DisplayPrice? b)
        {
            return a?._price == b?._price;
        }

        public static bool operator!=(DisplayPrice? a, DisplayPrice? b)
        {
            return a?._price != b?._price;
        }

        public static bool operator>(DisplayPrice? a, DisplayPrice? b)
        {
            return a?._price > b?._price;
        }

        public static bool operator<(DisplayPrice? a, DisplayPrice? b)
        {
            return a?._price < b?._price;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            if (obj is DisplayPrice)
            {
                return _price == ((DisplayPrice)obj)._price;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return _price.GetHashCode();
        }
        
    }

}
