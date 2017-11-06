namespace Zeport.UnitTests

open NUnit.Framework
open Zeport

module CpassTest =

    [<Test>]
    let ``when view cpass, if not login yet, then access denied`` () =
        let alreadyLogin = false
        let result = viewCpass alreadyLogin
        assertAccessDenied result

    [<Test>]
    let ``when view cpass, if already login, then ok`` () =
        let alreadyLogin = true
        let result = viewCpass alreadyLogin
        match result with
        | Ok _ -> good ()
        | Error _ -> bad ()

    [<Test>]
    let ``when do cpass, if not login yet, then access denied`` () =
        let result = doCpass None (fun _ _ -> true) "" "" ""
        assertAccessDenied result

    let sampleUser = { Id = 1; Username = Username "nghia.buivan"; IsAdmin = true }

    [<Test>]
    let ``when do cpass, if current password is wrong, then error`` () =
        let result = doCpass (Some sampleUser) (fun _ _ -> false) "" "" ""
        match result with
        | Error (Other _) -> good ()
        | _ -> bad ()

    [<Test>]
    let ``when do cpass, if the new password is empty, then error`` () =
        let result = doCpass (Some sampleUser) (fun _ _ -> true) "current" "" "bbb"
        match result with
        | Error (Other _) -> good ()
        | _ -> bad ()

    [<Test>]
    let ``when do cpass, if the 2 new passwords do not match, then error`` () =
        let result = doCpass (Some sampleUser) (fun _ _ -> true) "current" "aaa" "bbb"
        match result with
        | Error (Other _) -> good ()
        | _ -> bad ()

    [<Test>]
    let ``when do cpass, if new password is longer than 32, then error`` () =
        let result = doCpass (Some sampleUser) (fun _ _ -> true)
                        "current"
                        "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
                        "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
        match result with
        | Error (Other _) -> good ()
        | _ -> bad ()

    [<Test>]
    let ``when do cpass, if everything is ok, then ok of the user and the new password`` () =
        let result = doCpass (Some sampleUser) (fun _ _ -> true) "" "aaa" "aaa"
        match result with
        | Ok tuple -> Assert.AreEqual ((sampleUser, "aaa"), tuple)
        | _ -> bad ()
