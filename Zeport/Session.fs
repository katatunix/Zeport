namespace Zeport

open Suave
open Suave.Operators

open NghiaBui.Common.Maybe
open NghiaBui.Suave

open Domain

module Session =

    let mutable private keyPrefix = ""

    let setKeyPrefix prefix =
        keyPrefix <- prefix

    let private keyUsername () = keyPrefix + "Username"
    let private keyRole () = keyPrefix + "Role"

    let init = initSession

    let setUser (user : User) : WebPart =
        setSessionValue (keyUsername ()) user.Username
        >=> setSessionValue (keyRole ()) user.Role

    let getUser ctx =
        maybe {
            let! username = getSessionValue ctx (keyUsername ())
            let! role = getSessionValue ctx (keyRole ())
            return { Username = username; Role = role } }

    let hasUser ctx =
        (getUser ctx).IsSome

    let clearUser = resetSession

    let handleDoLoginResult = function
        | Success user ->
            setUser user
        | Failed _ ->
            fun ctx -> async { return Some ctx }
