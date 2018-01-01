namespace Zeport

open Ui

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
            return Text (TITLE, html) }
