namespace Zeport.UnitTests

open NUnit.Framework
open Zeport

module CpassTest =

    [<Test>]
    let ``when view cpass, if not login yet, then Error of AccessDenied`` () =
        let alreadyLogin = false
        let result = viewCpass alreadyLogin
        assertAccessDenied result

    [<Test>]
    let ``when view cpass, if already login, then Ok`` () =
        let alreadyLogin = true
        let result = viewCpass alreadyLogin
        match result with
        | Ok _ -> ()
        | Error _ -> Assert.IsTrue false

    [<Test>]
    let ``when do cpass, if not login yet, then Error of AccessDenied`` () =
        let result = doCpass None (fun _ _ -> true) "" "" ""
        assertAccessDenied result

    [<Test>]
    let ``when do cpass, if current password is wrong, then Error of the Other type`` () =
        let result = doCpass (Username "" |> Some) (fun _ _ -> false) "" "" ""
        match result with
        | Error (Other _) -> ()
        | _ -> Assert.IsTrue false

    [<Test>]
    let ``when do cpass, if the new password is empty, then Error of the Other type`` () =
        let result = doCpass (Username "" |> Some) (fun _ _ -> true) "current" "" "bbb"
        match result with
        | Error (Other _) -> ()
        | _ -> Assert.IsTrue false

    [<Test>]
    let ``when do cpass, if the 2 new passwords do not match, then Error of the Other type`` () =
        let result = doCpass (Username "" |> Some) (fun _ _ -> true) "current" "aaa" "bbb"
        match result with
        | Error (Other _) -> ()
        | _ -> Assert.IsTrue false

    [<Test>]
    let ``when do cpass, if new password is longer than 32, then Error of the Other type`` () =
        let result = doCpass (Username "" |> Some) (fun _ _ -> true) "current"
                        "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
                        "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
        match result with
        | Error (Other _) -> ()
        | _ -> Assert.IsTrue false

    [<Test>]
    let ``when do cpass, if everything is ok, then Ok of username and the new password`` () =
        let username = Username "nghia.buivan"
        let result = doCpass (username |> Some) (fun _ _ -> true) "" "aaa" "aaa"
        match result with
        | Ok tuple -> Assert.AreEqual ((username, "aaa"), tuple)
        | _ -> Assert.IsTrue false
