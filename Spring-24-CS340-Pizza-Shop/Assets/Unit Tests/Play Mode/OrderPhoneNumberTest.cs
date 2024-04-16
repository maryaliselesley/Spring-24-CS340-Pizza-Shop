using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class OrderPhoneNumberTest
{
    private string _validPhoneNumber = "1234567890";
    private string _phoneNumberTooLong = "12345678900";
    private string _phoneNumberInvalidChars = "@$%2193082";
    private string _phoneNumberContainsLetter = "a234567890";

    [UnityTest]
    public IEnumerator OrderPhoneNumberTestWithEnumeratorPasses()
    {
        GameObject gameObject = new GameObject();
        FinishPayment finishPayment = gameObject.AddComponent<FinishPayment>();

        // Use reflection to access the private method
        MethodInfo methodInfo = typeof(FinishPayment).GetMethod("IsPhoneNumberValidHelper", BindingFlags.NonPublic | BindingFlags.Instance);

        // Type cast and invoke the finishPayment object with parameters of the method
        Assert.IsTrue((bool)methodInfo.Invoke(finishPayment, new object[] { _validPhoneNumber }));
        Assert.IsFalse((bool)methodInfo.Invoke(finishPayment, new object[] { _phoneNumberTooLong }));
        Assert.IsFalse((bool)methodInfo.Invoke(finishPayment, new object[] { _phoneNumberInvalidChars }));
        Assert.IsFalse((bool)methodInfo.Invoke(finishPayment, new object[] { _phoneNumberContainsLetter }));

        yield return null;
    }
}