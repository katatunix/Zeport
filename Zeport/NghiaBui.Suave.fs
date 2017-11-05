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

    let resource convertToDisk : WebPart =
        pathRegex "(.*)\.(css|map|eot|svg|ttf|woff|woff2|js|png|jpg|jpeg|gif|ico)"
        >=> request (fun r ->
            match convertToDisk r.path with
            | Some p ->
                Files.browseFile System.Environment.CurrentDirectory p
            | None ->
                never)

    let idWebPart = fun ctx -> async { return Some ctx }

    [<AllowNullLiteral>]
    type WebPath (homeUrl, homeDisk) =
        let trimSlash (str : string) =
            if str.EndsWith "/" then
                str.Substring (0, str.Length - 1)
            else
                str
        let homeUrl = trimSlash homeUrl
        let homeUrlWithSlash = homeUrl + "/"
        let homeDisk = trimSlash homeDisk

        member x.Home = homeUrl
        member x.HomeWithSlash = homeUrlWithSlash

        member x.GetTail (url : string) =
            if url.StartsWith homeUrlWithSlash then
                System.IO.Path.Combine (homeDisk, url.Substring homeUrlWithSlash.Length) |> Some
            else
                None
