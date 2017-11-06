namespace Zeport

open Suave
open Suave.Successful
open Suave.Operators
open Result
open UiCommon
open Controller

module Uc =

    let private makeWebPart output ctx =
        async {
            let! o = output
            match o with
            | Text text ->
                return! OK text ctx
            | Redirect path ->
                return! Redirection.FOUND path ctx }

    let private navi () = buildNavi Db.findTeamsByParent Db.findProjectsByParent

    let view404 =
        context (
            Session.getUser
            >> UiError.render404 (navi ())
            >> makeWebPart )

    let viewHome =
        context (
            Session.getUser
            >> UiHome.render (navi ())
            >> makeWebPart )
        
    let logout =
        Session.clearUser ()
        >=> Redirection.FOUND Path.Home

    let viewLogin =
        context (
            Session.hasUser
            >> viewLogin
            >> UiLogin.renderView (navi ())
            >> makeWebPart )

    let doLogin =
        request (fun request ->
            let result =
                request
                |> parseDoLogin
                ||> doLogin Db.checkLogin
            (result |> Session.handleDoLoginResult)
            >=> (result |> UiLogin.renderDo (navi ()) |> makeWebPart))

    let viewCpass =
        context (fun ctx ->
            let user = Session.getUser ctx
            user.IsSome
            |> viewCpass
            |> UiCpass.renderView (navi ()) user
            |> makeWebPart )

    let doCpass =
        context (fun ctx ->
            let user = Session.getUser ctx
            let userId = user |> Option.map (fun u -> u.Id)
            parseDoCpass ctx.request
            |||> doCpass userId Db.checkPassword
            |> bind (fun (userId, password) ->
                Db.updatePassword userId password |> accessDeniedResult)
            |> UiCpass.renderDo (navi ()) user
            |> makeWebPart)
