namespace Zeport

module UiSidebar =

    let renderProjectTree () =
        async {
            return! Ui.renderTemplate "ProjectTree.liquid" 0 }
