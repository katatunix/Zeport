namespace Zeport

[<AutoOpen>]
module DomainCpass =

    let viewCpass alreadyLogin =
        if alreadyLogin then
            Ok ()
        else
            Error AccessDenied

    let doCpass (user : User option) checkPass
                (currentPass : string) (newPass : string) newPass2 =
        match user with
        | None ->
            AccessDenied |> Error
        | Some user ->
            if checkPass user.Id currentPass = false then
                Other "Wrong current password" |> Error
            elif newPass <> newPass2 then
                Other "The two new passwords do not match" |> Error
            elif newPass.Length = 0 then
                Other "The new password is empty" |> Error
            elif newPass.Length > 32 then
                Other "The length of the new password is greater than 32" |> Error
            else
                (user, newPass) |> Ok
