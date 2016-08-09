using DocuSign.BusinessObject.Enums;
using DocuSign.BusinessObject.Errors;

namespace DocuSign.BusinessObject
{
    public class Person
    {
        // Fields - Clothing slots
        private string footwear;
        private string headwear;
        private string socks;
        private string shirt;
        private string jacket;
        private string pants;
        
        // This value determines wheter the Person has or not his/her pajamas on.
        private bool hasPajamas;

        // Determines the outside subjective temperature for this person.
        private Temperature temperature;


        // Properties for Clothing slots
        public string Footwear
        {
            get { return footwear; }
        }
        public string Headwear
        {
            get { return headwear; }
        }
        public string Socks
        {
            get { return socks; }
        }
        public string Shirt
        {
            get { return shirt; }
        }
        public string Jacket
        {
            get { return jacket; }
        }
        public string Pants
        {
            get { return pants; }
        }


        /**
        * Initializes a Person, with the given temperature (from the Temperature Enum). All clothing actions will depend on this value.
        */
        public Person(Temperature temperature)
        {
            this.temperature = temperature;

            // Initial state is in house with pajamas on
            hasPajamas = true;
        }
        
        /**
        * Validates if person is ready to leave house
        */
        private bool IsReadyToLeave()
        {
            // You cannot leave the house until all items of clothing are on (except socks and a jacket when it’s hot)
            return !hasPajamas && // No Pajamas
                   IsEquipped(footwear) && // Footwear On
                   IsEquipped(headwear) && // Headwear On
                   (IsEquipped(socks) || temperature == Temperature.HOT) && // Socks On, or too Hot
                   IsEquipped(shirt) && // Shirt On
                   (IsEquipped(jacket) || temperature == Temperature.HOT) && // Jacket On, or too Hot
                   IsEquipped(pants); // Pants On
        }

        // Command 7 - Leave house
        public string LeaveHouse()
        {
            if (!IsReadyToLeave())
                throw new ClothingException("Not ready to leave yet!");

            return "leaving house";
        }

        // Clothing slots methods --------------------------------------------------------

        /**
        * Checks if an item slot is already equipped.
        */
        private bool IsEquipped(string clothingSlot)
        {
            if (clothingSlot != null && clothingSlot.Length > 0)
                return true;

            return false;
        }

        /**
        * Checks if a clothing slot is valid to be equipped. Throws exceptions if it's not.
        */
        private void CheckClothingSlot(string clothingSlot)
        {
            // Pajamas must be taken off before anything else can be put on
            if (hasPajamas)
                throw new PajamasException("Pajamas still on!");

            // Only 1 piece of each type of clothing may be put on
            if (IsEquipped(clothingSlot))
                throw new ClothingException("Clothing slot overload!");
        }

        // Command 8 - Take off pajamas
        public string RemovePajamas()
        {
            hasPajamas = false;
            return "Removing PJs";
        }

        // Command 1 - Footwear
        public void EquipFootwear()
        {
            CheckClothingSlot(footwear);

            // Socks must be put on before shoes (Only applies if COLD)
            if (!IsEquipped(socks) && temperature == Temperature.COLD)
                throw new ClothingException("No socks yet!");

            // Pants must be put on before shoes
            if (!IsEquipped(pants))
                throw new ClothingException("No pants yet!");

            if (temperature == Temperature.HOT)
            {
                footwear = "sandals";
            }
            else
            {
                footwear = "boots";
            }
        }

        // Command 2 - Headwear
        public void EquipHeadwear()
        {
            CheckClothingSlot(headwear);

            // The shirt must be put on before the headwear or jacket
            if (!IsEquipped(shirt))
                throw new ClothingException("No shirt yet!");

            if (temperature == Temperature.HOT)
            {
                headwear = "sun visor";
            }
            else
            {
                headwear = "hat";
            }
        }

        // Command 3 - Socks
        public void EquipSocks()
        {
            CheckClothingSlot(socks);

            if (temperature == Temperature.HOT)
            {
                // You cannot put on socks when it is hot
                throw new ClothingException("Too hot for socks!");
            }
            else
            {
                socks = "socks";
            }
        }

        // Command 4 - Shirt
        public void EquipShirt()
        {
            CheckClothingSlot(shirt);

            if (temperature == Temperature.HOT)
            {
                shirt = "t-shirt";
            }
            else
            {
                shirt = "shirt";
            }
        }

        // Command 5 - Jacket
        public void EquipJacket()
        {
            CheckClothingSlot(jacket);

            // The shirt must be put on before the headwear or jacket
            if (!IsEquipped(shirt))
                throw new ClothingException("No shirt yet!");

            if (temperature == Temperature.HOT)
            {
                // You cannot put on a jacket when it is hot
                throw new ClothingException("Too hot for a jacket!");
            }
            else
            {
                jacket = "jacket";
            }
        }

        // Command 6 - Pants
        public void EquipPants()
        {
            CheckClothingSlot(pants);

            if (temperature == Temperature.HOT)
            {
                pants = "shorts";
            }
            else
            {
                pants = "pants";
            }
        }

    }
}
