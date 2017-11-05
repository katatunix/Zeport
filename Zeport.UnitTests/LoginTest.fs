namespace Zeport.UnitTests

open NUnit.Framework
open Zeport

module LoginTest =

    [<Test>]
    let ``when view login, if already login, then go home`` () =
        let alreadyLogin = true
        let result = viewLogin alreadyLogin
        Assert.AreEqual (GoHome, result)

    [<Test>]
    let ``when view login, if not login yet, then stay`` () =
        let alreadyLogin = false
        let result = viewLogin alreadyLogin
        Assert.AreEqual (Stay, result)

    [<Test>]
    let ``when do login, if username and password are correct, then Ok of that user`` () =
        let username = Username "nghia.buivan"
        let user = { Username = username; IsAdmin = true }
        let check = fun _ _ -> Some user
        let result = doLogin check username "12345678"
        match result with
        | Ok actualUser -> Assert.AreEqual (user, actualUser)
        | Error _ -> Assert.IsTrue false

    [<Test>]
    let ``when do login, if username and password are incorrect, then Error of that username`` () =
        let username = Username "nghia.buivan"
        let check = fun _ _ -> None
        let result = doLogin check username "12345678"
        match result with
        | Ok _ -> Assert.IsTrue false
        | Error actualUsername -> Assert.AreEqual (username, actualUsername)
