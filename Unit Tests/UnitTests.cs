using Microsoft.VisualStudio.TestTools.UnitTesting;
using DocuSign.BusinessObject;
using DocuSign.BusinessObject.Enums;
using DocuSign.BusinessObject.Errors;

/**
* This unit tests are done on the business objects of the exercise
*/
namespace DocuSign.UnitTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        [ExpectedException(typeof(ClothingException))]
        public void Person_TriesToLeaveWithPajamasOn_ShouldThrowClothingException()
        {
            Person person = new Person(Temperature.HOT);

            person.LeaveHouse();
        }

        [TestMethod]
        [ExpectedException(typeof(ClothingException))]
        public void Person_LeavesHouseWithoutAllItemSlots_ShouldThrowClothingException()
        {
            Person person = new Person(Temperature.HOT);

            // 8 - Remove Pajamas
            person.RemovePajamas();

            // 6 - Pants
            person.EquipPants();

            // 4 - Shirt
            person.EquipShirt();

            // 2 - Headwear
            person.EquipHeadwear();

            // 7 - Leave House
            person.LeaveHouse();
        }

        [TestMethod]
        [ExpectedException(typeof(PajamasException))]
        public void Person_TriesToEquipSlotWithoutRemovingPajamas_ShouldThrowPajamasException()
        {
            Person person = new Person(Temperature.HOT);

            // 6 - Pants
            person.EquipPants();

            // 4 - Shirt
            person.EquipShirt();
        }

        [TestMethod]
        [ExpectedException(typeof(ClothingException))]
        public void Person_TriesToEquipSocksWhenHot_ShouldThrowClothingException()
        {
            Person person = new Person(Temperature.HOT);

            // 8 - Remove Pajamas
            person.RemovePajamas();

            // 3 - Socks
            person.EquipSocks();
        }

        [TestMethod]
        [ExpectedException(typeof(ClothingException))]
        public void Person_TriesToEquipJacketBeforeShirtWhenCold_ShouldThrowClothingException()
        {
            Person person = new Person(Temperature.COLD);

            // 8 - Remove Pajamas
            person.RemovePajamas();

            // 5 - Jacket
            person.EquipJacket();

            // 4 - Shirt
            person.EquipShirt();
        }


        // Succesful tests -------------------------------------------------
        [TestMethod]
        public void Person_HotTemperature_ShouldEquipProperItems()
        {
            Person person = new Person(Temperature.HOT);

            // 8 - Remove Pajamas
            person.RemovePajamas();

            // 6 - Pants
            person.EquipPants();

            // 4 - Shirt
            person.EquipShirt();

            // 2 - Headwear
            person.EquipHeadwear();

            // 1 - Footwear
            person.EquipFootwear();
            
            Assert.AreEqual(person.Footwear, "sandals");
            Assert.AreEqual(person.Headwear, "sun visor");
            Assert.IsNull(person.Socks);
            Assert.AreEqual(person.Shirt, "t-shirt");
            Assert.IsNull(person.Jacket);
            Assert.AreEqual(person.Pants, "shorts");
        }

        [TestMethod]
        public void Person_ColdTemperature_ShouldEquipProperItems()
        {
            Person person = new Person(Temperature.COLD);

            // 8 - Remove Pajamas
            person.RemovePajamas();

            // 6 - Pants
            person.EquipPants();

            // 3 - Socks
            person.EquipSocks();

            // 4 - Shirt
            person.EquipShirt();

            // 2 - Headwear
            person.EquipHeadwear();

            // 5 - Jacket
            person.EquipJacket();

            // 1 - Footwear
            person.EquipFootwear();

            Assert.AreEqual(person.Footwear, "boots");
            Assert.AreEqual(person.Headwear, "hat");
            Assert.AreEqual(person.Socks, "socks");
            Assert.AreEqual(person.Shirt, "shirt");
            Assert.AreEqual(person.Jacket, "jacket");
            Assert.AreEqual(person.Pants, "pants");
        }

        // Input: HOT 8, 6, 4, 2, 1, 7
        [TestMethod]
        public void Person_ExcersiceTest1_ShouldNotThrowAnyException()
        {
            Person person = new Person(Temperature.HOT);

            // 8 - Remove Pajamas
            person.RemovePajamas();

            // 6 - Pants
            person.EquipPants();

            // 4 - Shirt
            person.EquipShirt();

            // 2 - Headwear
            person.EquipHeadwear();

            // 1 - Footwear
            person.EquipFootwear();

            // 7 - Leave House
            string response = person.LeaveHouse();

            Assert.IsNotNull(response);
            Assert.IsTrue(response.Length > 0);
        }

        // Input: COLD 8, 6, 3, 4, 2, 5, 1, 7
        [TestMethod]
        public void Person_ExcersiceTest2_ShouldNotThrowAnyException()
        {
            Person person = new Person(Temperature.COLD);

            // 8 - Remove Pajamas
            person.RemovePajamas();

            // 6 - Pants
            person.EquipPants();

            // 3 - Socks
            person.EquipSocks();

            // 4 - Shirt
            person.EquipShirt();

            // 2 - Headwear
            person.EquipHeadwear();

            // 5 - Jacket
            person.EquipJacket();

            // 1 - Footwear
            person.EquipFootwear();

            // 7 - Leave House
            string response = person.LeaveHouse();

            Assert.IsNotNull(response);
            Assert.IsTrue(response.Length > 0);
        }

    }
}
