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
    let private keyIsAdmin () = keyPrefix + "IsAdmin"

    let init = initSession

    let setUser (user : User) : WebPart =
        setSessionValue (keyUsername ()) user.Username
        >=> setSessionValue (keyIsAdmin ()) user.IsAdmin

    let getUser ctx =
        maybe {
            let! username = getSessionValue ctx (keyUsername ())
            let! isAdmin = getSessionValue ctx (keyIsAdmin ())
            return { Username = username; IsAdmin = isAdmin } }

    let hasUser ctx =
        (getUser ctx).IsSome

    let clearUser = resetSession

    let handleDoLoginResult = function
        | Success user ->
            setUser user
        | Failed _ ->
            fun ctx -> async { return Some ctx }
