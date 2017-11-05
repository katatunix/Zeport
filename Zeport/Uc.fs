namespace Zeport

open Suave
open Suave.Successful
open Suave.Operators
open Result
open NghiaBui.Common
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

    let view404 =
        context (
            Session.getUsername
            >> UiCommon.render404Full
            >> makeWebPart )

    let viewHome =
        context (
            Session.getUsername
            >> UiHome.render
            >> makeWebPart )
        
    let logout =
        Session.clearUser ()
        >=> Redirection.FOUND Path.Home

    let viewLogin =
        context (
            Session.hasUser
            >> viewLogin
            >> UiLogin.renderView
            >> makeWebPart )

    let doLogin =
        request (fun request ->
            let result =
                request
                |> parseDoLogin
                ||> doLogin Db.checkLogin
            (result |> Session.handleDoLoginResult)
            >=> (result |> UiLogin.renderDo |> makeWebPart))

    let viewCpass =
        context (fun ctx ->
            let username = Session.getUsername ctx
            username.IsSome
            |> viewCpass
            |> UiCpass.renderView username
            |> makeWebPart )

    let doCpass =
        let checkPwd username password =
            Db.checkLogin username password |> option2bool

        context (fun ctx ->
            let username = Session.getUsername ctx
            parseDoCpass ctx.request
            |||> doCpass username checkPwd
            |> bind (fun (username, password) ->
                Db.updatePassword username password |> accessDeniedResult)
            |> UiCpass.renderDo username
            |> makeWebPart)
