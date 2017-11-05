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
        | Ok _ -> ()
        | Error _ -> Assert.IsTrue false

    [<Test>]
    let ``when do cpass, if not login yet, then access denied`` () =
        let result = doCpass None (fun _ _ -> true) "" "" ""
        assertAccessDenied result

    [<Test>]
    let ``when do cpass, if current password is wrong, then error`` () =
        let result = doCpass (Some 1) (fun _ _ -> false) "" "" ""
        match result with
        | Error (Other _) -> ()
        | _ -> Assert.IsTrue false

    [<Test>]
    let ``when do cpass, if the new password is empty, then error`` () =
        let result = doCpass (Some 1) (fun _ _ -> true) "current" "" "bbb"
        match result with
        | Error (Other _) -> ()
        | _ -> Assert.IsTrue false

    [<Test>]
    let ``when do cpass, if the 2 new passwords do not match, then error`` () =
        let result = doCpass (Some 1) (fun _ _ -> true) "current" "aaa" "bbb"
        match result with
        | Error (Other _) -> ()
        | _ -> Assert.IsTrue false

    [<Test>]
    let ``when do cpass, if new password is longer than 32, then error`` () =
        let result = doCpass (Some 1) (fun _ _ -> true)
                        "current"
                        "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
                        "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
        match result with
        | Error (Other _) -> ()
        | _ -> Assert.IsTrue false

    [<Test>]
    let ``when do cpass, if everything is ok, then ok of the user ID and the new password`` () =
        let result = doCpass (Some 1) (fun _ _ -> true) "" "aaa" "aaa"
        match result with
        | Ok tuple -> Assert.AreEqual ((1, "aaa"), tuple)
        | _ -> Assert.IsTrue false
