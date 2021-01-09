using Konveyor.Common.Utilities;
using System;
using System.Collections.Generic;
using Xunit;

namespace Konveyor.Common.Tests
{
    public class CodeGeneratorTests
    {
        public static IEnumerable<object[]> DemoCustomerCodes()
        {
            yield return new object[] 
            { 
                CodeGenerator.GenerateCode("Customer"), 
                CodeGenerator.GenerateCode("Customer") 
            
            };
        }

        public static IEnumerable<object[]> DemoEmployeeCodes()
        {
            yield return new object[]
            {
                CodeGenerator.GenerateCode("Employee"),
                CodeGenerator.GenerateCode("Employee")
            };
        }

        public static IEnumerable<object[]> DemoOrderCodes()
        {
            yield return new object[]
            {
                CodeGenerator.GenerateCode("Order"),
                CodeGenerator.GenerateCode("Order")

            };
        }

        public static IEnumerable<object[]> DemoEmptyCodes()
        {
            yield return new object[]
            {
                CodeGenerator.GenerateCode(""),
                CodeGenerator.GenerateCode("BadInput")

            };
        }


        [Theory]
        [MemberData(nameof(DemoCustomerCodes))]
        public void TestCustomerCode(string customerCode1, string customerCode2)
        {
            Assert.NotNull(customerCode1);
            Assert.NotEqual(string.Empty, customerCode1);
            Assert.StartsWith("C", customerCode1);
            Assert.InRange(customerCode1.Length, 11, 11);

            Assert.NotNull(customerCode2);
            Assert.NotEqual(string.Empty, customerCode2);
            Assert.StartsWith("C", customerCode2);
            Assert.InRange(customerCode2.Length, 11, 11);

            Assert.NotEqual(customerCode1, customerCode2);
        }


        [Theory]
        [MemberData(nameof(DemoEmployeeCodes))]
        public void TestEmployeeCode(string employeeCode1, string employeeCode2)
        {
            Assert.NotNull(employeeCode1);
            Assert.NotEqual(string.Empty, employeeCode1);
            Assert.StartsWith("E", employeeCode1);
            Assert.InRange(employeeCode1.Length, 11, 11);

            Assert.NotNull(employeeCode2);
            Assert.NotEqual(string.Empty, employeeCode2);
            Assert.StartsWith("E", employeeCode2);
            Assert.InRange(employeeCode2.Length, 11, 11);

            Assert.NotEqual(employeeCode1, employeeCode2);
        }


        [Theory]
        [MemberData(nameof(DemoOrderCodes))]
        public void TestOrderCode(string orderCode1, string orderCode2)
        {
            Assert.NotNull(orderCode1);
            Assert.NotEqual(string.Empty, orderCode1);
            Assert.StartsWith(DateTime.Today.Year.ToString(), orderCode1);
            Assert.InRange(orderCode1.Length, 14, 14);

            Assert.NotNull(orderCode2);
            Assert.NotEqual(string.Empty, orderCode2);
            Assert.StartsWith(DateTime.Today.Year.ToString(), orderCode2);
            Assert.InRange(orderCode2.Length, 14, 14);

            Assert.NotEqual(orderCode1, orderCode2);
        }


        [Theory]
        [MemberData(nameof(DemoEmptyCodes))]
        public void TestInvalidInput(string emptyCode1, string emptyCode2)
        {
            Assert.NotNull(emptyCode1);
            Assert.Equal(string.Empty, emptyCode1);

            Assert.NotNull(emptyCode2);
            Assert.Equal(string.Empty, emptyCode2);
        }
    }
}
