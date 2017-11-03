namespace Zeport

open Ui

module UiHome =

    let render currentUser =
        async {
            let! html =
                UiMainLayout.render
                    "Welcome to Zeport"
                    (UiBanner.render currentUser UiBanner.Item.Home)
                    (UiSidebar.renderProjectTree ())
                    (renderTemplate "Home.liquid" Path.Home)
            return Text html }
