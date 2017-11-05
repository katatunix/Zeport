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

    let private render user message =
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
                    (renderProjectTree ())
                    (renderTemplate "Cpass.liquid" model)
            return Text html }

    let renderView user = function
        | Error AccessDenied ->
            renderAccessDeniedFull user
        | _ ->
            render user None

    let renderDo user = function
        | Error AccessDenied ->
            renderAccessDeniedFull user
        | Error (Other str) ->
            render user (Some { Success = false; Content = str })
        | Ok _ ->
            render user (Some { Success = true; Content = "Saved" })
