namespace Zeport

open UiCommon

module UiCpass =

    let CUR_FIELD = "cur"
    let NEW_FIELD = "new"
    let NEW2_FIELD = "new2"

    type Model = {
        Title       : string
        Message     : Message option
        CurField    : string
        NewField    : string
        New2Field   : string }

    let private render message =
        async {
            let TITLE = "Change password"
            let model = {
                Title       = TITLE
                Message     = message
                CurField    = CUR_FIELD
                NewField    = NEW_FIELD
                New2Field   = NEW2_FIELD }
            let! html = renderTemplate "Cpass.liquid" model
            return Content (TITLE, html) }

    let renderView = function
        | Error AccessDenied ->
            UiError.renderAccessDenied ()
        | _ ->
            render None

    let renderDo = function
        | Error AccessDenied ->
            UiError.renderAccessDenied ()
        | Error (Other str) ->
            render (Some { Success = false; Content = str })
        | Ok _ ->
            render (Some { Success = true; Content = "Saved" })
