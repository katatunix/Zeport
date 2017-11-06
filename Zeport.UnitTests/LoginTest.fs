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
    let ``when do login, if username and password are correct, then ok of that user`` () =
        let username = Username "nghia.buivan"
        let user = { Id = 1; Username = username; IsAdmin = true }
        let check = fun _ _ -> Some user
        let result = doLogin check username "12345678"
        match result with
        | Ok actualUser -> Assert.AreEqual (user, actualUser)
        | Error _ -> bad ()

    [<Test>]
    let ``when do login, if username and password are incorrect, then error of that username`` () =
        let username = Username "nghia.buivan"
        let check = fun _ _ -> None
        let result = doLogin check username "12345678"
        match result with
        | Ok _ -> bad ()
        | Error actualUsername -> Assert.AreEqual (username, actualUsername)
