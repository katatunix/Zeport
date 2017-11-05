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

    let private render username message =
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
                    (renderBanner username Cpass)
                    (renderProjectTree ())
                    (renderTemplate "Cpass.liquid" model)
            return Text html }

    let renderView username = function
        | Error AccessDenied ->
            renderAccessDeniedFull username
        | _ ->
            render username None

    let renderDo username = function
        | Error AccessDenied ->
            renderAccessDeniedFull username
        | Error (Other str) ->
            render username (Some { Success = false; Content = str })
        | Ok _ ->
            render username (Some { Success = true; Content = "Saved" })
