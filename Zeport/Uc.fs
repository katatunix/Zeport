namespace Zeport

open Suave
open Suave.Successful
open Suave.Operators

module Uc =

    let private makeWebPart output ctx =
        async {
            let! o = output
            match o with
            | Ui.Output.Text text ->
                return! OK text ctx
            | Ui.Output.Redirect path ->
                return! Redirection.FOUND path ctx }

    let viewHome =
        context (
            Session.getUser
            >> UiHome.render
            >> makeWebPart )
        
    let logout =
        Session.clearUser ()
        >=> Redirection.FOUND Path.Home

    let viewLogin =
        context (
            Session.hasUser
            >> DomainFuns.viewLogin
            >> UiLogin.renderView
            >> makeWebPart )

    let doLogin =
        request (fun request ->
            let result =
                request
                |> Controller.parseDoLoginRequest
                |> DomainFuns.doLogin Db.checkLogin
            let wp1 = result |> Session.handleDoLoginResult
            let wp2 = result |> UiLogin.renderDo |> makeWebPart
            wp1 >=> wp2)
