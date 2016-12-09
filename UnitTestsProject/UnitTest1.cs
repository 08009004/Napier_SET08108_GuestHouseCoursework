using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Program
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestBookingConstrutor_NormalCase()
        {
            // arrange
            PersonComponent c = PersonFactory.Instance
                                             .GetNewCustomer("name","address");
           
            BookingComponent b1 = new Booking(1,
                                              c,
                                              new DateTime(2016, 12, 09),
                                              new DateTime(2016, 12, 13));

            BookingComponent b2 = new Booking(1,
                                              c,
                                              new DateTime(2016, 12, 09),
                                              new DateTime(2016, 12, 13));

            // assert
            Assert.IsTrue(b1.Equals(b2), "Booking.Equals method problem");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestBookingConstructor_CustomerException()
        {
            // arrange
            PersonComponent p = new Person("name");

            // act
            BookingComponent b = new Booking(98,
                                             p,
                                             new DateTime(2016, 02, 18),
                                             new DateTime(2016, 03, 2));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestBookingConstructor_BookingNbException()
        {
            // arrange
            PersonComponent c = PersonFactory.Instance
                                             .GetNewCustomer("name", "address");
            // act
            BookingComponent b = new Booking(-10,
                                             c,
                                             new DateTime(1998, 02, 18),
                                             new DateTime(1998, 03, 2));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestBookingConstructor_DatesException()
        {
            // arrange
            PersonComponent c = PersonFactory.Instance
                                             .GetNewCustomer("name", "address");
            // act
            BookingComponent b = new Booking(3,
                                             c,
                                             new DateTime(2016, 04, 18),
                                             new DateTime(2016, 02, 15));
        }

        [TestMethod]
        public void TestBookingAddGuest_NormalCase()
        {
            // arrange
            PersonComponent expected = PersonFactory.Instance
                                                    .GetNewGuest("myself",
                                                                 "08855N8",
                                                                 31);
            BookingComponent b1 = new Booking(
                                        1,
                                        PersonFactory.Instance
                                                     .GetNewCustomer("name", 
                                                                     "address"),
                                        new DateTime(1024, 12, 09),
                                        new DateTime(2016, 9, 3));
            // act
            b1.AddGuest(expected);

            // assert
            Assert.IsTrue(b1.GetGuests().Contains(expected), 
                          "Problem with Booking.AddGuest");
        }

        [TestMethod]
        public void TestBookingGetNbGuests_NormalCase()
        {
            // arrange
            int expected = 0;
            BookingComponent b1 = new Booking(
                                        1,
                                        PersonFactory.Instance
                                                     .GetNewCustomer("name",
                                                                     "address"),
                                        new DateTime(1024, 12, 09),
                                        new DateTime(2016, 9, 3));
            // act
            int result = b1.GetNbGuests();

            // assert
            Assert.AreEqual(expected, result, 
                            "Problem with Booking.GetNbGuests");
        }

        [TestMethod]
        public void TestBookingGetNbNights_NormalCase()
        {
            // arrange
            int expected = 4;
            BookingComponent b1 = new Booking(
                                        1,
                                        PersonFactory.Instance
                                                     .GetNewCustomer("name",
                                                                     "address"),
                                        new DateTime(2010, 01, 07),
                                        new DateTime(2010, 01, 11));
            // act
            int result = b1.GetNbNights();

            // assert
            Assert.AreEqual(expected, result,
                            "Problem with Booking.GetNbNights");
        }

        [TestMethod]
        public void TestBookingGetCostPerNight_NormalCase()
        {
            // arrange
            float expected = 80;
            BookingComponent b1 = new Booking(
                                        1,
                                        PersonFactory.Instance
                                                     .GetNewCustomer("name",
                                                                     "address"),
                                        new DateTime(2010, 01, 07),
                                        new DateTime(2010, 01, 11));
            b1.AddGuest(PersonFactory.Instance.GetNewGuest("g1", "Pass", 57));
            b1.AddGuest(PersonFactory.Instance.GetNewGuest("g2", "Port", 14));

            // act
            float result = b1.GetCostPerNight();

            // assert
            Assert.AreEqual(expected, result,
                            "Problem with Booking.GetCostPerNight");
        }
    }
}
