namespace Zeport.IntegrationTests

open NUnit.Framework
open Zeport

module DbTest =

    [<Test>]
    let ``test openConn`` () =
        use conn = Db.openConn ()
        ()

    [<Test>]
    let ``test checkLogin ok`` () =
        use conn = Db.openConn ()
        let result =
            Db.checkLogin conn (Username "nghia.buivan") "12345678"
        Assert.AreEqual (
            Some { Id = 1; Username = Username "nghia.buivan"; IsAdmin = true },
            result )

    [<Test>]
    let ``test checkLogin error`` () =
        use conn = Db.openConn ()
        let result =
            Db.checkLogin conn (Username "nghia.buivan") "1234"
        Assert.AreEqual (None, result )
