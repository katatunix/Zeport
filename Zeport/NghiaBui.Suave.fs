namespace NghiaBui

open Suave
open Suave.Authentication
open Suave.State
open Suave.State.CookieStateStore
open Suave.Cookie
open Suave.Operators
open Suave.Filters

module Suave =

    let initSession : WebPart = statefulForSession

    let getSessionValue ctx key =
        HttpContext.state ctx
        |> Option.bind (fun state -> state.get key)

    let setSessionValue key value : WebPart =
        context (fun ctx ->
            match HttpContext.state ctx with
            | Some state -> state.set key value
            | None -> never)

    let resetSession () : WebPart =
        unsetPair SessionAuthCookie
        >=> unsetPair StateCookie

    //===================================================================================

    let resource convertToDisk : WebPart =
        pathRegex "(.*)\.(css|map|eot|svg|ttf|woff|woff2|js|png|jpg|jpeg|gif|ico)"
        >=> request (fun r ->
            match convertToDisk r.path with
            | Some p -> Files.browseFileHome p
            | None -> never)
