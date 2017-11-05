﻿namespace Zeport

[<AutoOpen>]
module DomainCpass =

    let viewCpass alreadyLogin =
        if alreadyLogin then
            Ok ()
        else
            Error AccessDenied

    let doCpass (username : Username option) checkPass currentPass (newPass : string) newPass2 =
        match username with
        | None ->
            AccessDenied |> Error
        | Some username ->
            if checkPass username currentPass = false then
                Other "Wrong current password" |> Error
            elif newPass <> newPass2 then
                Other "The two new passwords do not match" |> Error
            elif newPass.Length = 0 then
                Other "The new password is empty" |> Error
            elif newPass.Length > 32 then
                Other "The length of new password is greater than 32" |> Error
            else
                (username, newPass) |> Ok