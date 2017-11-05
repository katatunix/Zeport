namespace Zeport

open Suave
open NghiaBui.Suave

module Session =

    let mutable private keyPrefix = ""
    let setKeyPrefix prefix = keyPrefix <- prefix

    let private keyCurrentUser () = keyPrefix + "CurrentUser"

    let init = initSession

    let setUser (user : User) : WebPart =
        setSessionValue (keyCurrentUser ()) user

    let getUser ctx : User option =
        getSessionValue ctx (keyCurrentUser ())

    let getUsername =
        getUser >> Option.map (fun user -> user.Username)

    let hasUser ctx = (getUser ctx).IsSome

    let clearUser = resetSession

    let handleDoLoginResult = function
        | Ok user -> setUser user
        | Error _ -> idWebPart
