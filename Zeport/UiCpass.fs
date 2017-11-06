namespace Zeport

open UiCommon

module UiCpass =

    let CUR_FIELD = "cur"
    let NEW_FIELD = "new"
    let NEW2_FIELD = "new2"

    type Model = {
        Title : string
        Message : Message option
        CurPassField : string
        NewPassField : string
        New2PassField : string }

    let private render navi user message =
        async {
            let TITLE = "Change password"
            let model = {
                Title = TITLE
                Message = message
                CurPassField = CUR_FIELD
                NewPassField = NEW_FIELD
                New2PassField = NEW2_FIELD }
            let! html =
                renderMainLayout
                    TITLE
                    (renderBanner user Cpass)
                    (UiNavi.render navi)
                    (renderTemplate "Cpass.liquid" model)
            return Text html }

    let renderView navi user = function
        | Error AccessDenied ->
            UiError.renderAccessDenied navi user
        | _ ->
            render navi user None

    let renderDo navi user = function
        | Error AccessDenied ->
            UiError.renderAccessDenied navi user
        | Error (Other str) ->
            render navi user (Some { Success = false; Content = str })
        | Ok _ ->
            render navi user (Some { Success = true; Content = "Saved" })
