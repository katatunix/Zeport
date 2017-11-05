namespace Zeport.UnitTests

open NUnit.Framework
open Zeport

[<AutoOpen>]
module Common =

    let assertAccessDenied result =
        match result with
        | Error AccessDenied -> ()
        | _ -> Assert.IsTrue false
