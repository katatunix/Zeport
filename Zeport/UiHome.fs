namespace Zeport

open UiCommon

module UiHome =

    type HomeModel = {
        Title : string
        ResPath : string }

    let render username =
        async {
            let TITLE = "Welcome to Zeport"
            let model = {
                Title = TITLE
                ResPath = Path.Res }
            let! html =
                renderMainLayout
                    TITLE
                    (renderBanner username Home)
                    (renderProjectTree ())
                    (renderTemplate "Home.liquid" model)
            return Text html }
