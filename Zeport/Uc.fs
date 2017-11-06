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
            | Output.Text text ->
                return! OK text ctx
            | Output.Redirect path ->
                return! Redirection.FOUND path ctx }

    let private user (f : User option -> WebPart) =
        context (Session.getUser >> f)

    let private userequest (f : User option -> HttpRequest -> WebPart) =
        context (fun ctx -> f (Session.getUser ctx) ctx.request)

    let private navi () = buildNavi Db.findTeamsByParent Db.findProjectsByParent

    let view404 =
        user (fun user ->
            UiError.render404 ()
            |> UiMainLayout.render user UiBanner.Nope (navi ())
            |> makeWebPart)

    let viewHome =
        user (fun user ->
            UiHome.render ()
            |> UiMainLayout.render user UiBanner.Home (navi ())
            |> makeWebPart)
        
    let logout =
        Session.clearUser ()
        >=> Redirection.FOUND Path.Home

    let viewLogin =
        user (fun user ->
            user.IsSome
            |> viewLogin
            |> UiLogin.renderView
            |> UiMainLayout.render None UiBanner.Login (navi ())
            |> makeWebPart)

    let doLogin =
        request (fun request ->
            let result = request |> parseDoLogin ||> doLogin Db.checkLogin
            (result |> Session.handleDoLoginResult)
            >=> (   result
                    |> UiLogin.renderDo
                    |> UiMainLayout.render None UiBanner.Login (navi ())
                    |> makeWebPart ))

    let viewCpass =
        user (fun user ->
            user.IsSome
            |> viewCpass
            |> UiCpass.renderView
            |> UiMainLayout.render user UiBanner.Cpass (navi ())
            |> makeWebPart)

    let doCpass =
        userequest (fun user request ->
            parseDoCpass request
            |||> doCpass user Db.checkPassword
            |> bind (fun (user, password) ->
                Db.updatePassword user.Id password |> accessDeniedResult)
            |> UiCpass.renderDo
            |> UiMainLayout.render user UiBanner.Cpass (navi ())
            |> makeWebPart)
