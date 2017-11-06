namespace Zeport

open UiCommon

module UiHome =

    type Model = {
        Title : string
        ResPath : string }

    let render () =
        async {
            let TITLE = "Welcome to Zeport"
            let model = {
                Title = TITLE
                ResPath = Path.Res }
            let! html = renderTemplate "Home.liquid" model
            return Content (TITLE, html) }
